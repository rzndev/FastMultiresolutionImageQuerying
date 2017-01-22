using System.Drawing;
using System.Windows.Forms;

namespace ContentBasedImageRetrieval
{
    public partial class RoundedButtonControl : UserControl
    {
        private readonly Color _borderColor = Color.Gray;
        private int _borderWidth = 1;
        private bool _selected;

        public RoundedButtonControl()
        {
            InitializeComponent();
        }

        public void SetSelected(bool selected)
        {
            _selected = selected;
            CreateGraphics();
            OnPaint(new PaintEventArgs(CreateGraphics(),
                new Rectangle(
                    Location.X,
                    Location.Y,
                    Width,
                    Height)));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_selected)
            {
                var myBrush = new SolidBrush(_borderColor);
                Graphics graphics = e.Graphics;
                graphics.FillEllipse(myBrush, new Rectangle(0, 0, Size.Width - 2, Size.Width - 2));
                myBrush.Dispose();
            }
            else
            {
                var myBrush = new SolidBrush(BackColor);
                Graphics graphics = e.Graphics;
                graphics.FillEllipse(myBrush, new Rectangle(0, 0, Size.Width - 2, Size.Width - 2));
                var myPen = new Pen(_borderColor, _borderWidth);
                graphics.DrawEllipse(myPen, 0, 0, Size.Width - 2, Size.Width - 2);
                myBrush.Dispose();
                myPen.Dispose();
            }
        }
    }
}