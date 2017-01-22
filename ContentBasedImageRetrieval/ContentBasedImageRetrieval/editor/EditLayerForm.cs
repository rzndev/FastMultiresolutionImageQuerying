using System;
using System.Windows.Forms;

namespace ContentBasedImageRetrieval
{
    public partial class EditLayerForm : Form
    {
        private Layer _layer;

        public EditLayerForm()
        {
            InitializeComponent();
        }

        public Layer LayerItem
        {
            set { _layer = value; }
        }

        public Layer GetLayer()
        {
            return _layer;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _layer.Caption = textBoxLayerName.Text;
            Project.Instance.HasChanged(_layer);
            Close();
        }

        private void EditLayerForm_Load(object sender, EventArgs e)
        {
            textBoxLayerName.Text = _layer.Caption;
            OrderIdValue.Text = _layer.OrderId.ToString();
        }
    }
}