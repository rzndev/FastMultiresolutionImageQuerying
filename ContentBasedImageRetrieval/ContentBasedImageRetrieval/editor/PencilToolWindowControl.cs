using System;
using System.Drawing;
using System.Windows.Forms;

namespace ContentBasedImageRetrieval
{
    public partial class PencilToolWindowControl : UserControl
    {
        private static PencilToolWindowControl _instance;

        // draggable events
        private static Size mouseOffset;
        private bool _draggable;

        public PencilToolWindowControl()
        {
            InitializeComponent();

            // draggable events
            MouseDown += control_MouseDown;
            MouseUp += control_MouseUp;
            MouseMove += control_MouseMove;

            labelHeader.MouseDown += control_MouseDown;
            labelHeader.MouseUp += control_MouseUp;
            labelHeader.MouseMove += control_MouseMove;
            // end draggable
        }

        public static PencilToolWindowControl Instance
        {
            set { _instance = value; }
            get { return _instance ?? (_instance = new PencilToolWindowControl()); }
        }

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


        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            var form = FindForm() as FormCanvas;
            if (form != null)
            {
                form.SetMnuPencilChecked(false);
            }
        }
    }
}