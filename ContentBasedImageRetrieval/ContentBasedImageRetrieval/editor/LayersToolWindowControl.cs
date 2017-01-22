using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ContentBasedImageRetrieval
{
    public partial class LayersToolWindowControl : UserControl
    {
        private static LayersToolWindowControl _instance;
        private static Size _mouseOffset;
        private Rectangle _dragBoxFromMouseDown;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        private bool _draggable;
        private bool _dragging = false;
        private int _rowIndexFromMouseDown;
        private int _rowIndexOfItemUnderMouseToDrop;

        public LayersToolWindowControl()
        {
            InitializeComponent();

            dgvLayers.RowsAdded +=dgvLayers_RowsAdded;


            dgvLayers.DataSource = null;
            dgvLayers.Columns[1].Width = Settings.Instance.Canvas.Thumbnail.Width;
            dgvLayers.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLayers.Columns[3].Width = 50;

            dgvLayers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvLayers.RowHeadersVisible = false;
            dgvLayers.ColumnHeadersVisible = false;
            dgvLayers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvLayers.AllowUserToResizeRows = false;
            dgvLayers.AllowDrop = true;

            dgvLayers.BackgroundColor = Color.White;
            dgvLayers.RowTemplate.Height = 50;
            dgvLayers.RowTemplate.MinimumHeight = 50;

            // draggable events
            MouseDown += control_MouseDown;
            MouseUp += control_MouseUp;
            MouseMove += control_MouseMove;

            labelHeader.MouseDown += control_MouseDown;
            labelHeader.MouseUp += control_MouseUp;
            labelHeader.MouseMove += control_MouseMove;
            // end draggable
        }

        public void SetProjectEvents()
        {
            Project.Instance.ProjectLoaded += Instance_ProjectLoaded;
            Project.Instance.ProjectClosed += Instance_ProjectClosed;

            Project.Instance.Layers.CollectionChanged += Layers_CollectionChanged;
            Project.Instance.BitmapChanged += Instance_BitmapChanged;
            Project.Instance.ActiveLayerChanged += Instance_ActiveLayerChanged;

        }

        void Instance_ProjectClosed(object sender, EventArgs e)
        {
            dgvLayers.Rows.Clear();
        }

        public static LayersToolWindowControl Instance
        {
            set { _instance = value; }
            get { return _instance ?? (_instance = new LayersToolWindowControl()); }
        }

        public Guid PreviousSelectedValue { get; set; }

        void Instance_ProjectLoaded(object sender, EventArgs e)
        {
            RefreshRows();
            SortGrid();
        }

        private void SetThumbnail(LayerChangeEventArgs e)
        {
            Layer layer = e.GetState;
            DataGridViewRow row = GetRowById(layer.Id);
            row.Cells[1].Value = layer.Thumbnail;
        }

        private void Instance_BitmapChanged(object sender, LayerChangeEventArgs e)
        {
            SetThumbnail(e);
        }

        private void Instance_ActiveLayerChanged(object sender, EventArgs e)
        {
            if (dgvLayers.SelectedRows.Cast<DataGridViewRow>()
                .Any(x => Guid.Parse(x.Cells[0].Value.ToString())
                          == Project.Instance.ActiveLayerGuid))
                return;

            DataGridViewRow row = dgvLayers.Rows.Cast<DataGridViewRow>()
                .FirstOrDefault(x => Guid.Parse(x.Cells[0].Value.ToString())
                                     == Project.Instance.ActiveLayerGuid);

            if (row != null)
                row.Selected = true;
        }


        private void Instance_ProjectChanged(object sender, LayerChangeEventArgs e)
        {
            SetCaption(e);
        }

        /// <summary>
        ///     Update caption
        /// </summary>
        /// <param name="e"></param>
        private void SetCaption(LayerChangeEventArgs e)
        {
            Layer layer = e.GetState;
            DataGridViewRow row = GetRowById(layer.Id);
            row.Cells[2].Value = layer.Caption;
        }

        private void RefreshRows()
        {
            dgvLayers.Rows.Clear();

            foreach (Layer layer in 
                Project.Instance.Layers.OrderByDescending(x => x.OrderId))
            {
                dgvLayers.Rows.Add(
                    layer.Id,
                    layer.Thumbnail,
                    layer.Caption,
                    layer.Visible,
                    layer.OrderId);
            }


            DataGridViewRow row = dgvLayers.Rows.Cast<DataGridViewRow>()
                .FirstOrDefault(x => Guid.Parse(x.Cells[0].Value.ToString())
                                     == Project.Instance.ActiveLayerGuid);

            if (row != null)
                row.Selected = true;
        }


        private void Layers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                RefreshRows();
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                ClearRows();
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                RemoveRow(GetRowById(e.OldItems.Cast<Layer>().First().Id));
            }

            SortGrid();
        }

        private void RemoveRow(DataGridViewRow row)
        {
            dgvLayers.Rows.Remove(row);
        }

        private void ClearRows()
        {
            dgvLayers.Rows.Clear();
        }

        private DataGridViewRow GetRowById(Guid guid)
        {
            return dgvLayers.Rows.Cast<DataGridViewRow>()
                .First(x => Guid.Parse(x.Cells[0].Value.ToString()) == guid);
        }

        // draggable events

        private void control_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseOffset = new Size(e.Location);
            _draggable = true;
        }

        private void control_MouseUp(object sender, MouseEventArgs e)
        {
            _draggable = false;
        }

        private void control_MouseMove(object sender, MouseEventArgs e)
        {
            if (_draggable)
            {
                Point newLocationOffset =
                    e.Location - _mouseOffset;

                (this).Left += newLocationOffset.X;
                (this).Top += newLocationOffset.Y;
            }
        }

        // end draggable

        private void panelAddLayer_MouseEnter(object sender, EventArgs e)
        {
            ((Panel) sender).BorderStyle = BorderStyle.FixedSingle;
        }

        private void panelAddLayer_MouseLeave(object sender, EventArgs e)
        {
            ((Panel) sender).BorderStyle = BorderStyle.None;
        }

        private void panelAddLayer_Click(object sender, EventArgs e)
        {
            if (Project.Instance != null)
            {
                Project.Instance.Layers.AddLayer();
            }
            else
            {
                ((FormCanvas) FindForm()).ShowStartForm();
            }
        }

        private void SortGrid()
        {
            DataGridViewColumn sortCol = dgvLayers.Columns[4];
            dgvLayers.Sort(sortCol, ListSortDirection.Ascending);
        }

        private void dgvLayers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                var ch1 = new DataGridViewCheckBoxCell();
                ch1 = (DataGridViewCheckBoxCell) dgvLayers.Rows[e.RowIndex].Cells[3];

                if (ch1.Value == null)
                    ch1.Value = false;
                switch (ch1.Value.ToString())
                {
                    case "True":
                        ch1.Value = false;
                        break;
                    case "False":
                        ch1.Value = true;
                        break;
                }

                Layer layer = GetSeletedLayer();
                layer.Visible = (bool) ch1.Value;
                Project.Instance.HasOrderChanged();
            }
        }

        private Layer GetSeletedLayer()
        {
            DataGridViewRow row = dgvLayers.SelectedRows.Cast<DataGridViewRow>().First();
            Guid guid = Guid.Parse(row.Cells[0].Value.ToString());
            Layer layer = Project.Instance.Layers.First(x => x.Id == guid);
            return layer;
        }

        private void dgvLayers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Layer layer = GetSeletedLayer();
            Project.Instance.HasChanged(layer);
        }

        // drag n drop

        private void dgvLayers_DragDrop(object sender, DragEventArgs e)
        {
            Project.Instance.Layers.CollectionChanged -= Layers_CollectionChanged;
            dgvLayers.SelectionChanged -= dgvLayers_SelectionChanged;

            Point clientPoint = dgvLayers.PointToClient(new Point(e.X, e.Y));

            _rowIndexOfItemUnderMouseToDrop =
                dgvLayers.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (_rowIndexOfItemUnderMouseToDrop == -1)
                return;

            if (e.Effect == DragDropEffects.Move)
            {
                var rowToMove = e.Data.GetData(typeof (DataGridViewRow)) as DataGridViewRow;

                dgvLayers.Rows.RemoveAt(_rowIndexFromMouseDown);
                dgvLayers.Rows.Insert(_rowIndexOfItemUnderMouseToDrop, rowToMove);
                dgvLayers.Rows[_rowIndexOfItemUnderMouseToDrop].Selected = true;
            }

            List<Guid> list = dgvLayers.Rows.Cast<DataGridViewRow>()
                .Select(x => Guid.Parse(x.Cells[0].Value.ToString())).ToList();

            int i = 0;
            foreach (Guid guid in list)
            {
                Project.Instance.Layers.First(x => x.Id == guid).OrderId = i++;
            }

            Project.Instance.Layers.CollectionChanged += Layers_CollectionChanged;
            Project.Instance.ProjectChanged += Instance_ProjectChanged;
            dgvLayers.SelectionChanged += dgvLayers_SelectionChanged;

            Project.Instance.HasOrderChanged();
        }

        private void dgvLayers_MouseDown(object sender, MouseEventArgs e)
        {
            _rowIndexFromMouseDown = dgvLayers.HitTest(e.X, e.Y).RowIndex;
            if (_rowIndexFromMouseDown != -1)
            {
                Size dragSize = SystemInformation.DragSize;
                _dragBoxFromMouseDown = new Rectangle(
                    new Point(e.X - (dragSize.Width/2),
                        e.Y - (dragSize.Height/2)),
                    dragSize);
            }
            else
            {
                _dragBoxFromMouseDown = Rectangle.Empty;
            }
        }

        private void dgvLayers_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void panelSettings_Click(object sender, EventArgs e)
        {
            if (Project.Instance != null)
            {
                var form = new EditLayerForm();
                form.LayerItem = GetSeletedLayer();
                form.ShowDialog();
            }
            else
            {
                ((FormCanvas) FindForm()).ShowStartForm();
            }
        }

        private void panelRemoveLayer_Click(object sender, EventArgs e)
        {
            if (Project.Instance != null)
            {
                Layer layer = GetSeletedLayer();
                DialogResult result = MessageBox.Show(
                    string.Format(@"Удалить слой ""{0}""?",
                        (layer.Caption.Length > 15
                            ? layer.Caption.Substring(0, 15) + ".."
                            : layer.Caption)),
                    @"Удаление слоя",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Project.Instance.Layers.Remove(layer);
                }
            }
            else
            {
                ((FormCanvas) FindForm()).ShowStartForm();
            }
        }

        private void dgvLayers_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (_dragBoxFromMouseDown != Rectangle.Empty &&
                    !_dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffect = dgvLayers.DoDragDrop(
                        dgvLayers.Rows[_rowIndexFromMouseDown],
                        DragDropEffects.Move);
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            ((Control) this).Visible = false;

            var form = FindForm() as FormCanvas;
            if (form != null)
            {
                form.SetMnuLayersChecked(false);
            }
        }

        private void dgvLayers_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgvLayers.SelectionChanged -= dgvLayers_SelectionChanged;
            var row = ((DataGridView) sender).Rows[e.RowIndex] as DataGridViewRow;
            row.Selected = true;
            Project.Instance.ActiveLayerGuid = Guid.Parse(row.Cells[0].Value.ToString());;
            PreviousSelectedValue = Project.Instance.ActiveLayerGuid;
            dgvLayers.SelectionChanged += dgvLayers_SelectionChanged;
        }

        private void dgvLayers_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow item = ((DataGridView) sender).CurrentRow;
            if (item != null)
            {
                Guid selguid = Guid.Parse(item.Cells[0].Value.ToString());
                if (Project.Instance.ActiveLayerGuid == selguid)
                    return;

                if (PreviousSelectedValue != selguid)
                {
                    Project.Instance.ActiveLayerGuid = selguid;
                    PreviousSelectedValue = selguid;
                }
            }
        }
    }
}