using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ContentBasedImageRetrieval
{
    public partial class FormMatching : Form
    {
        public FormMatching()
        {
            InitializeComponent();
        }

        public PictureBox GetPictureBox()
        {
            return pictureBox1;
        }

        public ListView GetListView()
        {
            return listView1;
        }
    }
}
