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
    public partial class FormQuantityResult : Form
    {
        int _num = 0; 
        public FormQuantityResult()
        {
            InitializeComponent();
        }

        /// <summary>
        /// установить число, отображаемое по умолчанию
        /// </summary>
        /// <param name="Number"></param>
        public void SetNumber(int Number)
        {
            textBox1.Text = Number.ToString();
            _num = Number;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Получить число, введенное в поле
        /// </summary>
        /// <returns></returns>
        public int GetNumber()
        {
            return _num;
        }

        /// <summary>
        /// Возврат результата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Int32.TryParse(textBox1.Text, out _num);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

    }
}
