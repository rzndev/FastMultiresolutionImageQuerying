using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ContentBasedImageRetrieval.Properties;

namespace ContentBasedImageRetrieval
{
    public partial class WorkArea : UserControl
    {
        private static WorkArea _instance;

        private WorkArea()
        {
            InitializeComponent();

            Project.Instance.X = 0;
            Project.Instance.Y = 0;

            Project.Instance.Bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
        }

        public static WorkArea Instance
        {
            set { _instance = value; }
            get { return _instance ?? (_instance = new WorkArea()); }
        }

        #region events

        private void Instance_OrderChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Layers_CollectionChanged(object sender,
            NotifyCollectionChangedEventArgs e)
        {
            Invalidate();
        }

        private void WorkArea_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (Project.Instance.GPath != null)
                Project.Instance.GPath.Dispose();

            for (int i = Project.Instance.CurrentLayer.Strips.Count - 1; i >= 0; i--)
            {
                Project.Instance.CurrentLayer.Strips[i].Dispose();
            }

            Project.Instance.CurrentLayer.Strips.Clear();

            if (Project.Instance.CurrentLayer.Bmp != null)
                Project.Instance.CurrentLayer.Bmp.Dispose();
        }

        private void WorkArea_ClientSizeChanged(object sender, EventArgs e)
        {
            Bitmap b = Project.Instance.CurrentLayer.Bmp;
            Project.Instance.CurrentLayer.Bmp = new Bitmap(ClientSize.Width, ClientSize.Height);

            if (b != null)
                b.Dispose();
        }

        private void WorkArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (Project.Instance.CurrentLayer == null)
            {
                DialogResult result =
                    MessageBox.Show(@"Для рисования необходимо добавить новый слой. Добавить новый слой?"
                                    + Environment.NewLine + @"Подробности в описании. (F1)",
                        @"Ошибка", MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                    Project.Instance.Layers.AddLayer();
                return;
            }
            if (Project.Instance.CurrentLayer != null && Project.Instance.CurrentLayer.Visible == false)
            {
                DialogResult result = MessageBox.Show(@"Скрытый слой не доступен для изменений. "
                                                      +
                                                      @"Необходимо поменять видимость выделенного слоя, либо выбрать другой слой. "
                                                      + @"Сделать видимым выделенный слой?"
                                                      + Environment.NewLine + @"Подробности в описании. (F1)",
                    @"Ошибка", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                    Project.Instance.CurrentLayer.Visible = true;
                return;
            }

            if (!Project.Instance.IsChanged)
                Project.Instance.IsChanged = true;

            Project.Instance.IsDoErase = false;

            Project.Instance.X = (e.X - AutoScrollPosition.X);
            Project.Instance.Y = (e.Y - AutoScrollPosition.Y);

            if (Project.Instance.CurrentStrip != null)
            {
                Project.Instance.CurrentStrip.IsSelected = false;
                Project.Instance.CurrentStrip = null;
            }

            if (e.Button == MouseButtons.Left &&
                (Settings.Instance.Mode == EditModeEnum.Erase ||
                 Settings.Instance.Mode == EditModeEnum.Draw))
            {
                if (Project.Instance.GPath == null)
                {
                    Project.Instance.GPath = new GraphicsPath();
                }

                Project.Instance.IsTracking = true;
            }

            if ((e.Button == MouseButtons.Left)
                && Project.Instance.CurrentLayer.Strips != null
                && Settings.Instance.Mode == EditModeEnum.Move)
            {
                bool b = false;

                SetUnselected();

                Project.Instance.MovePoints = new PointF[Project.Instance.CurrentLayer.Strips.Count];

                for (int i = 0; i < Project.Instance.CurrentLayer.Strips.Count; i++)
                {
                    Project.Instance.MovePoints[i]
                        = Project.Instance.CurrentLayer.Strips[i].FLocation.Location;
                }
                Invalidate();
            }

            if (e.Button == MouseButtons.Left
                && Settings.Instance.Mode == EditModeEnum.Erase)
            {
                if (Project.Instance.GPath == null)
                {
                    Project.Instance.GPath = new GraphicsPath();
                }

                Project.Instance.IsTracking = true;
                Project.Instance.IsDoErase = true;
            }
        }

        private void WorkArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (Project.Instance.CurrentLayer == null || Project.Instance.CurrentLayer.Visible == false)
                return;

            if (Project.Instance.IsTracking)
            {
                Project.Instance.GPath.AddLine(
                    new Point(Project.Instance.X, 
                        Project.Instance.Y),
                    new Point((e.X - AutoScrollPosition.X),
                        (e.Y - AutoScrollPosition.Y)));

                Project.Instance.X = (e.X - AutoScrollPosition.X);
                Project.Instance.Y = (e.Y - AutoScrollPosition.Y);

                Invalidate();
                Project.Instance.IsMoving = true;
            }

            if (e.Button == MouseButtons.Left
                && Project.Instance.CurrentLayer.Strips != null
                && Settings.Instance.Mode == EditModeEnum.Move)
            {
                Point mousePos = MousePosition;
                mousePos.Offset(-Project.Instance.X, -Project.Instance.Y);
                Point tmp = PointToClient(mousePos);

                for (int i = 0; i < Project.Instance.CurrentLayer.Strips.Count; i++)
                {
                    Project.Instance.CurrentLayer.Strips[i].FLocation =
                        new RectangleF(new PointF(
                            Project.Instance.MovePoints[i].X + tmp.X,
                            Project.Instance.MovePoints[i].Y + tmp.Y),
                            Project.Instance.CurrentLayer.Strips[i].FLocation.Size);
                }

                Invalidate();
            }
        }

        private void WorkArea_MouseUp(object sender, MouseEventArgs e)
        {
            if (Project.Instance.CurrentLayer.Visible == false)
                return;

            if (Project.Instance.GPath != null)
            {
                if (Project.Instance.IsMoving)
                {
                    if (e.Button == MouseButtons.Left
                        && Settings.Instance.Mode == EditModeEnum.Erase)
                    {
                        RectangleF r = Project.Instance.GPath.GetBounds();

                        // ReSharper disable ForCanBeConvertedToForeach
                        for (int i = 0; i < Project.Instance.CurrentLayer.Strips.Count; i++)
                            // ReSharper restore ForCanBeConvertedToForeach
                        {
                            if (Project.Instance.CurrentLayer.Strips[i].FLocation.IntersectsWith(r))
                            {
                                using (var lPath = (GraphicsPath) Project.Instance.GPath.Clone())
                                {
                                    lPath.Transform(new Matrix(1, 0, 0, 1,
                                        -Project.Instance.CurrentLayer.Strips[i].FLocation.X,
                                        -Project.Instance.CurrentLayer.Strips[i].FLocation.Y));

                                    if (Project.Instance.CurrentLayer.Strips[i].ErasingObjects == null)
                                        Project.Instance.CurrentLayer.Strips[i].ErasingObjects = new List<EraseObject>();

                                    Project.Instance.CurrentLayer.Strips[i].ErasingObjects.Add(new EraseObject
                                    {
                                        ErasingPath = (GraphicsPath) lPath.Clone(),
                                        ErasingWidth = (float) Settings.Instance.Width,
                                        ErasingRot = Project.Instance.CurrentLayer.Strips[i].Rotation
                                    });
                                }
                            }
                        }

                        Project.Instance.HasBitmapChanged(Project.Instance.CurrentLayer);
                    }
                    else if (e.Button == MouseButtons.Left
                             && Settings.Instance.Mode == EditModeEnum.Draw)
                    {
                        var l = new Strip();
                        l.OrderId = Project.Instance.CurrentLayer.Strips.Count == 0
                            ? 0 : Project.Instance.CurrentLayer.Strips.Max(x => x.OrderId) + 1;
                        RectangleF r = Project.Instance.GPath.GetBounds();
                        Project.Instance.GPath.Transform(new Matrix(1, 0, 0, 1, -r.X, -r.Y));
                        l.FLocation = r;
                        l.GPath = (GraphicsPath) Project.Instance.GPath.Clone();
                        l.FColor = Settings.Instance.Color;
                        l.FWidth = (float) Settings.Instance.Width;
                        Project.Instance.CurrentLayer.Strips.Add(l);
                        Project.Instance.HasBitmapChanged(Project.Instance.CurrentLayer);
                    }
                }
                Project.Instance.GPath.Reset();
                Invalidate();
            }

            Project.Instance.IsTracking = false;
            Project.Instance.IsMoving = false;
            Project.Instance.CurrentStrip = null;
        }

        private void WorkArea_Paint(object sender, PaintEventArgs e)
        {
            DrawBmp();
            SplitAll();
            e.Graphics.DrawImage(Project.Instance.Bmp, 0, 0);
        }

        private void WorkArea_MouseEnter(object sender, EventArgs e)
        {
            if (Settings.Instance.Mode == EditModeEnum.Draw ||
                Settings.Instance.Mode == EditModeEnum.Erase)
            {
                if (Project.Instance.CurrentLayer == null)
                    return;
                if (Project.Instance.CurrentLayer.Visible == false)
                {
                    SetCursorForbiden();
                }
                else
                {
                    SetCursorRing();
                }
            }

            if (Settings.Instance.Mode == EditModeEnum.Move)
            {
                SetCursorHand();
            }
        }

        private void SetCursorForbiden()
        {
            Cursor = Cursors.No;
        }

        private void WorkArea_MouseLeave(object sender, EventArgs e)
        {
            if (Settings.Instance.Mode == EditModeEnum.Draw)
            {
                SetCursorArrow();
            }
        }

        #endregion

        #region cursors

        private void SetCursorArrow()
        {
            Cursor = Cursors.Arrow;
        }

        private void SetCursorHand()
        {
            Cursor = new Cursor(Resources.hand23x23.GetHicon());
        }

        private void SetCursorRing()
        {
            var cursorBitmap = new Bitmap(
                (int) Settings.Instance.Width + 2,
                (int) Settings.Instance.Width + 2);

            using (Graphics g = Graphics.FromImage(cursorBitmap))
            {
                g.DrawEllipse(new Pen(Brushes.DeepSkyBlue),
                    0, 0,
                    (float) Settings.Instance.Width,
                    (float) Settings.Instance.Width);
            }

            Cursor = CursorHelper.CreateCursor(cursorBitmap,
                ((int) Settings.Instance.Width)/2,
                ((int) Settings.Instance.Width)/2);
        }

        #endregion

        #region core methods

        public void ReDrawAll()
        {
            foreach (Layer layer in Project.Instance.Layers.OrderBy(x => x.OrderId))
            {
                using (Graphics g = Graphics.FromImage(layer.Bmp))
                {
                    g.Clear(Color.Transparent);

                    if (Project.Instance.CurrentLayer.BackImage != null)
                    {
                        {
                            g.Clear(Color.Transparent);
                            g.DrawImage(layer.BackImage,
                                    new Rectangle(
                                        0,
                                        0,
                                        layer.BackImage.Width,
                                        layer.BackImage.Height));
                        }
                    }


                    foreach (Strip strip in layer.Strips.OrderBy(x => x.OrderId))
                    {
                        using (var pen = new Pen(strip.FColor, strip.FWidth)
                        {
                            EndCap = LineCap.Round,
                            StartCap = LineCap.Round,
                            LineJoin = LineJoin.Round
                        })
                        {
                            using (var fPath = new GraphicsPath())
                            {
                                fPath.FillMode = FillMode.Winding;

                                using (var gP = (GraphicsPath)strip.GPath.Clone())
                                {
                                    gP.Transform(new Matrix(1, 0, 0, 1, 
                                        strip.FLocation.X, 
                                        strip.FLocation.Y));

                                    gP.Widen(pen);

                                    fPath.Transform(new Matrix(1, 0, 0, 1, 
                                        strip.FLocation.X, 
                                        strip.FLocation.Y));

                                    var con = g.BeginContainer();
                                    using (var reg = new Region(gP))
                                    {
                                        reg.Exclude(fPath);
                                        g.Clip = reg;
                                        g.SmoothingMode = SmoothingMode.AntiAlias;
                                        g.DrawPath(pen, gP);
                                    }
                                    g.EndContainer(con);
                                }
                            }                            
                        }
                    }
                    // Invalidate();
                }

                layer.Bmp.Save(
                    Path.Combine(Path.GetDirectoryName(Project.Instance.Path),
                        string.Format("{0}.png", layer.Caption)),
                    ImageFormat.Png);
            }

            
        }


        private void DrawBmp()
        {
            if (Project.Instance == null
                || Project.Instance.CurrentLayer == null
                || Project.Instance.CurrentLayer.Visible == false)
                return;

            using (Graphics g = Graphics.FromImage(Project.Instance.CurrentLayer.Bmp))
            {
                g.Clear(Color.Transparent);
                g.CompositingMode = CompositingMode.SourceCopy;

                if (Project.Instance.CurrentLayer.BackImage != null)
                {
                    {
                        g.Clear(Color.Transparent);
                        g.DrawImage(Project.Instance.CurrentLayer.BackImage,
                                new Rectangle(
                                    0,
                                    0,
                                    Project.Instance.CurrentLayer.BackImage.Width,
                                    Project.Instance.CurrentLayer.BackImage.Height));
                    }
                }
                for (int i = 0; i < Project.Instance.CurrentLayer.Strips.Count; i++)
                {
                    g.CompositingMode = CompositingMode.SourceOver;

                    using (var pen = new Pen(
                        Project.Instance.CurrentLayer.Strips[i].FColor,
                        Project.Instance.CurrentLayer.Strips[i].FWidth)
                    {
                        EndCap = LineCap.Round,
                        StartCap = LineCap.Round,
                        LineJoin = LineJoin.Round
                    })
                    {
                        Project.Instance.CurrentLayer.Strips[i].Render(g, pen);
                    }
                }

                if (Project.Instance.GPath != null)
                {
                    g.ResetClip();

                    Color color = Color.Transparent; ;
                    if (Project.Instance.IsDoErase)
                    {
                        g.CompositingMode = CompositingMode.SourceCopy;
                    }
                    else
                    {
                        g.CompositingMode = CompositingMode.SourceOver;
                        color = Settings.Instance.Color;
                    }

                    using (var pen = new Pen(color, (float) Settings.Instance.Width)
                    {
                        EndCap = LineCap.Round,
                        StartCap = LineCap.Round,
                        LineJoin = LineJoin.Round
                    })
                    {
                        g.DrawPath(pen, Project.Instance.GPath);
                    }
                }
            }
        }

        private void SetUnselected()
        {
            for (int i = 0; i < Project.Instance.CurrentLayer.Strips.Count; i++)
                Project.Instance.CurrentLayer.Strips[i].IsSelected = false;
        }

        private void SplitAll()
        {
            Project.Instance.Bmp =
                BitmapTools.CombineBitmap(
                    Settings.Instance.Canvas.Width,
                    Settings.Instance.Canvas.Height);
        }

        #endregion

        public void SetProjectEvents()
        {
            Project.Instance.Layers.CollectionChanged += Layers_CollectionChanged;
            Project.Instance.OrderChanged += Instance_OrderChanged;
        }
    }
}