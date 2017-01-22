using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlServerCe;

namespace ContentBasedImageRetrieval
{
    public partial class FormFastMultiresolutionImageQuerying : Form
    {
        public const int max_coefs = 60; // количество идентифицирующих коэффициентов изображения

        public FormFastMultiresolutionImageQuerying()
        {
            InitializeComponent();
        }

        public Bitmap current_image = null;

        public void SetImage(Bitmap img)
        {

            pictureBox1.Image = img;
        }

        private void OpenImage(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            current_image = new Bitmap(name);
            pictureBox1.Image = current_image;
        }

        private void buttonOpenFileDialog_Click(object sender, EventArgs e)
        {
            DialogResult Result;
            var dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.tif)|*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.tif"; ;
            if ((Result = dialog.ShowDialog()) == DialogResult.OK)
            {
                textBoxImagePath.Text = dialog.FileName;
                OpenImage(textBoxImagePath.Text);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            OpenImage(textBoxImagePath.Text);
        }

        private void buttonAddToDatabase_Click(object sender, EventArgs e)
        {
            if (null == current_image) return;
            cbir.ImageDatabase db = new cbir.ImageDatabase();
            db.AddImageToDatabase(current_image, textBoxName.Text);
            RefreshImageList();
        }

        private void RefreshImageList()
        {
            int i;
            cbir.ImageDatabase db = new cbir.ImageDatabase();
            SqlCeConnection conn = new SqlCeConnection(global::ContentBasedImageRetrieval.Properties.Settings.Default.cbirConnectionString);
            int[] list = db.getIdxList(conn);
            if (null != list)
                for (i = 0; i < list.Length; i++) list[i] = i; // для списка изображений индексация должна быть сквозной
            listView1.Items.Clear();
            db.FillListView(listView1, list);
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshImageList();
        }

        private void buttonRemoveImageFromDatabase_Click(object sender, EventArgs e)
        {
            cbir.ImageDatabase db = new cbir.ImageDatabase();
            var selectedItems = listView1.SelectedItems;
            foreach (ListViewItem selectedItem in selectedItems)
            {
                db.RemoveImageFromDatabase(selectedItem.ImageIndex);
            }
            RefreshImageList();
        }

        private void FormFastMultiresolutionImageQuerying_Load(object sender, EventArgs e)
        {
            RefreshImageList();
        }

        private void buttonMatch_Click(object sender, EventArgs e)
        {
            int i, j;
            Color c;
            cbir.ImageDatabase db = new cbir.ImageDatabase(); // база данных с изображениями, среди которых осуществляется поиск
            // определяем количество возвращаемых изображений
            int NumberOfRetrievingImages = 3;                 // наибольшее количество извлекаемых из базы данных изображений 
            FormQuantityResult frmq = new FormQuantityResult();
            frmq.SetNumber(NumberOfRetrievingImages);
            if (frmq.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                NumberOfRetrievingImages = frmq.GetNumber();
            }
            if (NumberOfRetrievingImages < 1) NumberOfRetrievingImages = 1; // количество изображений не должно быть меньше 1
            if (null == current_image) return;
            
            HaarWavelet dwt = new HaarWavelet(); // класс вейвлет преобразования
            int width, height;
            cbir.YIQ yiq = new cbir.YIQ(); // класс для преобразования цветового пространства из RGB в YIQ
            // вычисляем размеры изображения-запроса
            width = current_image.Width;
            height = current_image.Height;
            if (width < 2) return;
            if (height < 2) return;
            if ((width % 2) != 0) width -= 1;
            if ((height % 2) != 0) height -= 1;
            // формируем массив данных для изображения-запроса
            double[,] Red = new double[width, height];
            double[,] Green = new double[width, height];
            double[,] Blue = new double[width, height];
            for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                {
                    c = current_image.GetPixel(i, j);
                    Red[i, j] = (double)cbir.ImageDatabase.Scale(0, 255, -1, 1, c.R);
                    Green[i, j] = (double)cbir.ImageDatabase.Scale(0, 255, -1, 1, c.G);
                    Blue[i, j] = (double)cbir.ImageDatabase.Scale(0, 255, -1, 1, c.B);
                }
            yiq.Create_YIQ_from_RGB(Red, Green, Blue);
            double[,] w_Y; // вейвлет-преобразование составляющей Y
            double[,] w_I; // вейвлет-преобразование составляющей I
            double[,] w_Q; // вейвлет-преобразование составляющей Q

            // выполняем вейвлет-декомпозицию
            w_Y = dwt.standardDecomposition(yiq.image_Y);
            w_I = dwt.standardDecomposition(yiq.image_I);
            w_Q = dwt.standardDecomposition(yiq.image_Q);

            if (null == w_Y) return;
            if (null == w_I) return;
            if (null == w_Q) return;

            // вычисляем коэффициенты, идентифицирущие изображение (для 3 цветовых каналов)
            cbir.signature[] signatures = new cbir.signature[3];
            signatures[0] = cbir.signature.MakeSignature(w_Y, max_coefs); // коэффициенты, идентифицирующие изображение для составляющей Y
            signatures[1] = cbir.signature.MakeSignature(w_I, max_coefs); // коэффициенты, идентифицирующие изображение для составляющей I
            signatures[2] = cbir.signature.MakeSignature(w_Q, max_coefs); // коэффициенты, идентифицирующие изображение для составляющей Q

            // формируем запрос изображения
            cbir.ImageQuery query = new cbir.ImageQuery();
            query.FillScores(signatures, db);
            if (null == query.scores) return;    // изображений в базе данных нет - сравнивать не с чем
            int[] indexes = new int[query.scores.Length]; // индексы изображений в порядке увеличения индекса соответствия изображения
            for (i = 0; i < indexes.Length; i++) indexes[i] = i;
            // сортировка элементов
            for (i = 0; i < indexes.Length; i++)
            {
                int min_id = indexes[i];
                int min_j = i;
                for (j = i + 1; j < indexes.Length; j++)
                {
                    if (query.scores[indexes[j]] < query.scores[min_id])
                    {
                        min_id = indexes[j];
                        min_j = j;
                    }
                }
                int swap = indexes[i];
                indexes[i] = min_id;
                indexes[min_j] = swap;
            }

            FormMatching frm = new FormMatching();
            frm.GetPictureBox().Image = current_image;
            NumberOfRetrievingImages = Math.Min(indexes.Length, NumberOfRetrievingImages);
            int[] new_indexes = new int[NumberOfRetrievingImages];
            for (i = 0; i < NumberOfRetrievingImages; i++)
                new_indexes[i] = indexes[i];
            db.FillListView(frm.GetListView(), new_indexes);
            frm.Show();
        }

        private void buttonDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            cbir.ImageDatabase db = new cbir.ImageDatabase();
            
            string[] files = System.IO.Directory.GetFiles(fbd.SelectedPath);
            foreach (string file in files)
            {
                try
                {
                    if (string.IsNullOrEmpty(file)) continue;
                    current_image = new Bitmap(file);
                    db.AddImageToDatabase(current_image, "");
                }
                catch (Exception)
                {
                }
            }
            RefreshImageList();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (null == current_image) return;
            FormCanvas frm = new FormCanvas();
            frm.frm = this;  // устанавливаем указатель для обновления отредактированного изображения
            frm.Show();
            frm.CreateProjectFromBitmap(current_image);
        }
    }
}
