using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;

namespace ContentBasedImageRetrieval
{
    public partial class ActionToolWindowControl : UserControl
    {
        private static ActionToolWindowControl _instance;
        private static Size mouseOffset;
        private bool _draggable;
        private bool _preventReqInvoke;

        public ActionToolWindowControl()
        {
            InitializeComponent();

            Settings.Instance.EditModeChanged += Instance_EditModeChanged;

            panelPaint.Tag = EditModeEnum.Draw;
            panelErise.Tag = EditModeEnum.Erase;
            panelRotate.Tag = EditModeEnum.Rotate;
            panelMove.Tag = EditModeEnum.Move;

            // draggable events
            MouseDown += control_MouseDown;
            MouseUp += control_MouseUp;
            MouseMove += control_MouseMove;

            labelHeader.MouseDown += control_MouseDown;
            labelHeader.MouseUp += control_MouseUp;
            labelHeader.MouseMove += control_MouseMove;
            // end draggable

            SetSelected(panelPaint);
        }

        public static ActionToolWindowControl Instance
        {
            set { _instance = value; }
            get { return _instance ?? (_instance = new ActionToolWindowControl()); }
        }

        public void SetProjectEvents()
        {
            Project.Instance.Layers.CollectionChanged += Layers_CollectionChanged;
        }

        public EditModeEnum PreviousMode { get; set; }

        private void Layers_CollectionChanged(object sender,
            NotifyCollectionChangedEventArgs e)
        {
            if (PreviousMode == Settings.Instance.Mode)
                return;

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Settings.Instance.Mode = EditModeEnum.Draw;
                PreviousMode = EditModeEnum.Draw;
            }
        }

        private void Instance_EditModeChanged(object sender, EventArgs e)
        {
            if (_preventReqInvoke)
            {
                _preventReqInvoke = false;
                return;
            }

            switch (Settings.Instance.Mode)
            {
                case EditModeEnum.Draw:
                    panelPaint_Click(panelPaint, null);
                    break;
                case EditModeEnum.Erase:
                    panelPaint_Click(panelErise, null);
                    break;
                case EditModeEnum.Move:
                    panelPaint_Click(panelMove, null);
                    break;
                case EditModeEnum.Rotate:
                    panelPaint_Click(panelRotate, null);
                    break;
            }
        }

        // draggable events

        private void control_MouseDown(object sender, MouseEventArgs e)
        {
            mouseOffset = new Size(e.Location);
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
                Point newLocationOffset = e.Location - mouseOffset;
                (this).Left += newLocationOffset.X;
                (this).Top += newLocationOffset.Y;
            }
        }

        // end draggable

        private void panelPaint_Click(object sender, EventArgs e)
        {
            _preventReqInvoke = true;
            var panel = sender as Panel;
            SetSelected(panel);
            Settings.Instance.Mode = (EditModeEnum) panel.Tag;
        }

        private void SetSelected(Panel panel)
        {
            ClearAll();
            panel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void ClearAll()
        {
            panelPaint.BorderStyle = BorderStyle.None;
            panelErise.BorderStyle = BorderStyle.None;
            panelRotate.BorderStyle = BorderStyle.None;
            panelMove.BorderStyle = BorderStyle.None;
        }

        private void panelPaint_MouseEnter(object sender, EventArgs e)
        {
            ((Panel) sender).BorderStyle = BorderStyle.FixedSingle;
        }

        private void panelPaint_MouseLeave(object sender, EventArgs e)
        {
            var panel = ((Panel) sender);
            if (Settings.Instance.Mode != (EditModeEnum) panel.Tag)
                ((Panel) sender).BorderStyle = BorderStyle.None;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Visible = false;

            var form = FindForm() as FormCanvas;
            if (form != null)
            {
                form.SetMnuActionChecked(false);
            }
        }
    }
}