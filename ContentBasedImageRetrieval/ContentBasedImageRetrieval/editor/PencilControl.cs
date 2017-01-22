using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ContentBasedImageRetrieval
{
    public partial class PencilControl : UserControl
    {
        public PencilControl()
        {
            InitializeComponent();

            Settings.Instance.Width = 7;
            numericUpDownWidth.Value = 7;
            trackBarPencilSize.Value = 7;
            SetSelectedButton(7);
        }

        private void roundedButtonControl7_Click(object sender, EventArgs e)
        {
            var roundedButton = sender as RoundedButtonControl;
            if (roundedButton != null)
            {
                Settings.Instance.Width = roundedButton.Width;
                numericUpDownWidth.Value = roundedButton.Width;
                trackBarPencilSize.Value = roundedButton.Width;
            }
        }

        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            var control = sender as NumericUpDown;
            if (control != null)
            {
                Settings.Instance.Width = (int) control.Value;
                trackBarPencilSize.Value = (int) control.Value;
                SetSelectedButton((int) control.Value);
            }
        }

        private void SetSelectedButton(int width)
        {
            IEnumerable<Control> list = flowLayoutPanel1.Controls.Cast<Control>()
                .Union(flowLayoutPanel4.Controls.Cast<Control>())
                .Where(x => (x as RoundedButtonControl) != null);
            foreach (RoundedButtonControl item in list.Select(x => x as RoundedButtonControl))
                item.SetSelected(item.Size.Width == width);
        }

        private void trackBarPencilSize_MouseUp(object sender, MouseEventArgs e)
        {
            var tb = sender as TrackBar;
            if (tb != null)
            {
                Settings.Instance.Width = tb.Value;
                numericUpDownWidth.Value = tb.Value;
            }
        }
    }
}