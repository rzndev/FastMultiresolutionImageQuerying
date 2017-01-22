using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace ContentBasedImageRetrieval.cbir
{
    /// <summary>
    /// класс работы с набором изображений
    /// </summary>
    public class ImageDatabase
    {
        public const int max_coefs = 60; // количество идентифицирующих коэффициентов изображения
        public static int max_idx = 0;          // наибольшее значение индекса. Используется для предотвращения повторного использования индексов в текущем сеансе работы программы
        /// <summary>
        /// добавить изображение в базу данных
        /// </summary>
        /// <param name="conn">соединение с локальной базой данной</param>
        /// <param name="path">путь к оригиналу изображения на диске</param>
        /// <param name="name">наименование изображения</param>
        /// <param name="Y"></param>
        /// <param name="I"></param>
        /// <param name="Q"></param>
        public void AddImage(SqlCeConnection conn, string path, string name, double[,] Y, double[,] I, double[,] Q, int next_id)
        {
            int i;
            HaarWavelet dwt = new HaarWavelet(); // класс вейвлет преобразования
            double[,] w_Y; // вейвлет-преобразование составляющей Y
            double[,] w_I; // вейвлет-преобразование составляющей I
            double[,] w_Q; // вейвлет-преобразование составляющей Q


            if (null == conn) return;

            // выполняем вейвлет-декомпозицию
            w_Y = dwt.standardDecomposition(Y); 
            w_I = dwt.standardDecomposition(I);
            w_Q = dwt.standardDecomposition(Q);

            if (null == w_Y) return;
            if (null == w_I) return;
            if (null == w_Q) return;

            // вычисляем коэффициенты, идентифицирующие изображение
            signature sig_Y = signature.MakeSignature(w_Y, max_coefs); // коэффициенты, идентифицирующие изображение для составляющей Y
            signature sig_I = signature.MakeSignature(w_I, max_coefs); // коэффициенты, идентифицирующие изображение для составляющей I
            signature sig_Q = signature.MakeSignature(w_Q, max_coefs); // коэффициенты, идентифицирующие изображение для составляющей Q



            // получаем идентификатор для следующего изображения
            // выполняем запрос к базе данных
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            SqlCeCommand cmd;
            //SqlCeCommand cmd = new SqlCeCommand("select max(id) from image", conn);
            //SqlCeDataReader rd = cmd.ExecuteReader();
            //int next_id = 0;
            ////if (rd.HasRows)
            //{
            //    if (rd.Read())
            //    {
            //        if (rd.IsDBNull(0))
            //            next_id = 0;
            //        else
            //        {
            //            next_id = rd.GetInt32(0); // выбираем наибольшее значение идентификатора из базы данных
            //            next_id++;                // получаем следующее значение идентификатора
            //        }
            //    };
            //}
            
            //rd.Close();
            // добавляем информацию об изображении в базу данных
            string insertInfoSql = "INSERT INTO image (id, name, path, average_Y, average_I, average_Q)     VALUES (@id, @name, @path, @average_Y, @average_I, @average_Q)";
            cmd = new SqlCeCommand(insertInfoSql, conn);

            SqlCeParameter param;
            param = new SqlCeParameter();
            param.ParameterName = "@id";
            param.SqlDbType = System.Data.SqlDbType.Int;
            param.Direction = System.Data.ParameterDirection.Input;
            param.Value = next_id;
            cmd.Parameters.Add(param);

            param = new SqlCeParameter();
            param.ParameterName = "@name";
            param.SqlDbType = System.Data.SqlDbType.NVarChar;
            param.Direction = System.Data.ParameterDirection.Input;
            param.Value = name;
            cmd.Parameters.Add(param);

            param = new SqlCeParameter();
            param.ParameterName = "@path";
            param.SqlDbType = System.Data.SqlDbType.NVarChar;
            param.Direction = System.Data.ParameterDirection.Input;
            param.Value = path;
            cmd.Parameters.Add(param);

            param = new SqlCeParameter();
            param.ParameterName = "@average_Y";
            param.SqlDbType = System.Data.SqlDbType.Float;
            param.Direction = System.Data.ParameterDirection.Input;
            param.Value = sig_Y.average;
            cmd.Parameters.Add(param);

            param = new SqlCeParameter();
            param.ParameterName = "@average_I";
            param.SqlDbType = System.Data.SqlDbType.Float;
            param.Direction = System.Data.ParameterDirection.Input;
            param.Value = sig_I.average;
            cmd.Parameters.Add(param);

            param = new SqlCeParameter();
            param.ParameterName = "@average_Q";
            param.SqlDbType = System.Data.SqlDbType.Float;
            param.Direction = System.Data.ParameterDirection.Input;
            param.Value = sig_Q.average;
            cmd.Parameters.Add(param);

            cmd.ExecuteNonQuery();

            // добавляем информацию о положительных значимых коэффициентах
            insertInfoSql = "INSERT INTO positive (id, row, col, color) VALUES (@id, @row, @col, @color)";
            cmd = new SqlCeCommand(insertInfoSql, conn);
            // компонент цвета Y
            for (i = 0; i < sig_Y.col_positive.Length; i++)
            {
                cmd.Parameters.Clear();
                param = new SqlCeParameter();
                param.ParameterName = "@id";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = next_id;
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@row";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = sig_Y.row_positive[i];
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@col";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = sig_Y.col_positive[i];
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@color";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = 0;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
            }
            // компонент цвета I
            for (i = 0; i < sig_I.col_positive.Length; i++)
            {
                cmd.Parameters.Clear();
                param = new SqlCeParameter();
                param.ParameterName = "@id";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = next_id;
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@row";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = sig_I.row_positive[i];
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@col";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = sig_I.col_positive[i];
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@color";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = 1;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
            }
            // компонент цвета Q
            for (i = 0; i < sig_Q.col_positive.Length; i++)
            {
                cmd.Parameters.Clear();
                param = new SqlCeParameter();
                param.ParameterName = "@id";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = next_id;
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@row";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = sig_Q.row_positive[i];
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@col";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = sig_Q.col_positive[i];
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@color";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = 2;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
            }
            // добавление информации для значимых отрицательных элементов
            insertInfoSql = "INSERT INTO negative (id, row, col, color) VALUES (@id, @row, @col, @color)";
            cmd = new SqlCeCommand(insertInfoSql, conn);
            // компонент цвета Y
            for (i = 0; i < sig_Y.col_negative.Length; i++)
            {
                cmd.Parameters.Clear();
                param = new SqlCeParameter();
                param.ParameterName = "@id";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = next_id;
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@row";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = sig_Y.row_negative[i];
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@col";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = sig_Y.col_negative[i];
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@color";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = 0;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
            }
            // компонент цвета I
            for (i = 0; i < sig_I.col_negative.Length; i++)
            {
                cmd.Parameters.Clear();
                param = new SqlCeParameter();
                param.ParameterName = "@id";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = next_id;
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@row";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = sig_I.row_negative[i];
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@col";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = sig_I.col_negative[i];
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@color";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = 1;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
            }
            // компонент цвета Q
            for (i = 0; i < sig_Q.col_negative.Length; i++)
            {
                cmd.Parameters.Clear();
                param = new SqlCeParameter();
                param.ParameterName = "@id";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = next_id;
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@row";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = sig_Q.row_negative[i];
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@col";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = sig_Q.col_negative[i];
                cmd.Parameters.Add(param);

                param = new SqlCeParameter();
                param.ParameterName = "@color";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = 2;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        /// <summary>
        /// Получить количество изображений в базе данных
        /// </summary>
        /// <returns></returns>
        public int NumberOfImages()
        {
            SqlCeConnection conn = new SqlCeConnection(global::ContentBasedImageRetrieval.Properties.Settings.Default.cbirConnectionString);
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            string sql = "SELECT count(id) FROM image";
            SqlCeCommand cmd = new SqlCeCommand(sql, conn);
            int nelems = (int)cmd.ExecuteScalar();
            conn.Close();
            return nelems;
        }

        /// <summary>
        /// Получить набор идентифицирующих коэффициентов для изображения
        /// </summary>
        /// <param name="idx">индекс изображения в базе данных</param>
        /// <param name="color">индекс цветовой составляющей, для которой извлекается набор идентифицирующих коэффициентов 0 - Y, 1 - I, 2 - Q</param>
        /// <returns></returns>
        public signature GetSignatureOfImage(int idx, int color)
        {
            int[] list;    // список идентификаторов изображений
            int nelems;    // количество элементов в списке
            int i;
            signature result = new signature();
            result.col_negative = null;
            result.col_positive = null;
            result.row_negative = null;
            result.row_positive = null;
            SqlCeConnection conn = new SqlCeConnection(global::ContentBasedImageRetrieval.Properties.Settings.Default.cbirConnectionString);
            list = getIdxList(conn);
            int id = list[idx];
            string sql = "SELECT name, path, average_Y, average_I, average_Q FROM image WHERE id=@id";
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            SqlCeCommand cmd = new SqlCeCommand(sql, conn);

            SqlCeParameter param = new SqlCeParameter();
            param.ParameterName = "@id";
            param.SqlDbType = System.Data.SqlDbType.Int;
            param.Direction = System.Data.ParameterDirection.Input;
            param.Value = id;
            cmd.Parameters.Add(param);

            SqlCeDataReader dr = cmd.ExecuteReader();
            //if (dr.HasRows)
            {
                if (dr.Read())
                {
                    result.name = dr.GetString(0);
                    result.path = dr.GetString(1);
                    switch (color)
                    {
                        case 0:
                            result.average = dr.GetDouble(2);
                            break;
                        case 1:
                            result.average = dr.GetDouble(3);
                            break;
                        case 2:
                            result.average = dr.GetDouble(4);
                            break;
                    }
                    dr.Close();
                    // получаем количество положительных элементов
                    sql = "SELECT count(id) FROM positive WHERE id=@id and color=@color";
                    cmd = new SqlCeCommand(sql, conn);

                    param = new SqlCeParameter();
                    param.ParameterName = "@id";
                    param.SqlDbType = System.Data.SqlDbType.Int;
                    param.Direction = System.Data.ParameterDirection.Input;
                    param.Value = id;
                    cmd.Parameters.Add(param);

                    param = new SqlCeParameter();
                    param.ParameterName = "@color";
                    param.SqlDbType = System.Data.SqlDbType.TinyInt;
                    param.Direction = System.Data.ParameterDirection.Input;
                    param.Value = color;
                    cmd.Parameters.Add(param);
                    nelems = (int)cmd.ExecuteScalar();
                    if (nelems > 0)
                    {
                        // извлекаем информацию о положительных элементах из базы данных
                        result.col_positive = new int[nelems];
                        result.row_positive = new int[nelems];
                        i = 0;
                        sql = "SELECT row, col FROM positive WHERE id=@id and color=@color";
                        cmd = new SqlCeCommand(sql, conn);

                        param = new SqlCeParameter();
                        param.ParameterName = "@id";
                        param.SqlDbType = System.Data.SqlDbType.Int;
                        param.Direction = System.Data.ParameterDirection.Input;
                        param.Value = id;
                        cmd.Parameters.Add(param);

                        param = new SqlCeParameter();
                        param.ParameterName = "@color";
                        param.SqlDbType = System.Data.SqlDbType.TinyInt;
                        param.Direction = System.Data.ParameterDirection.Input;
                        param.Value = color;
                        cmd.Parameters.Add(param);
                        dr = cmd.ExecuteReader();
                        //if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                result.col_positive[i] = dr.GetInt32(0);
                                result.row_positive[i] = dr.GetInt32(1);
                            }
                        }
                        dr.Close();
                    }
                    // получаем количество отрицательных элементов
                    sql = "SELECT count(id) FROM negative WHERE id=@id and color=@color";
                    cmd = new SqlCeCommand(sql, conn);

                    param = new SqlCeParameter();
                    param.ParameterName = "@id";
                    param.SqlDbType = System.Data.SqlDbType.Int;
                    param.Direction = System.Data.ParameterDirection.Input;
                    param.Value = id;
                    cmd.Parameters.Add(param);

                    param = new SqlCeParameter();
                    param.ParameterName = "@color";
                    param.SqlDbType = System.Data.SqlDbType.TinyInt;
                    param.Direction = System.Data.ParameterDirection.Input;
                    param.Value = color;
                    cmd.Parameters.Add(param);
                    nelems = (int)cmd.ExecuteScalar();
                    if (nelems > 0)
                    {
                        // извлекаем информацию об отрицательных элементах из базы данных
                        result.col_negative= new int[nelems];
                        result.row_negative = new int[nelems];
                        i = 0;
                        sql = "SELECT row, col FROM negative WHERE id=@id and color=@color";
                        cmd = new SqlCeCommand(sql, conn);

                        param = new SqlCeParameter();
                        param.ParameterName = "@id";
                        param.SqlDbType = System.Data.SqlDbType.Int;
                        param.Direction = System.Data.ParameterDirection.Input;
                        param.Value = id;
                        cmd.Parameters.Add(param);

                        param = new SqlCeParameter();
                        param.ParameterName = "@color";
                        param.SqlDbType = System.Data.SqlDbType.TinyInt;
                        param.Direction = System.Data.ParameterDirection.Input;
                        param.Value = color;
                        cmd.Parameters.Add(param);
                        dr = cmd.ExecuteReader();
                        //if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                result.col_negative[i] = dr.GetInt32(0);
                                result.row_negative[i] = dr.GetInt32(1);
                            }
                        }
                        dr.Close();
                    }

                }
                else
                {
                    dr.Close();
                    return null;
                }
            }
            
            conn.Close();
            return result;
        }

        /// <summary>
        /// получить список идентификаторов изображений
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public int[] getIdxList(SqlCeConnection conn)
        {
            int i = 0;
            int[] result = null;
            if (null == conn) return null;
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            string sql = "SELECT count(id) FROM image";
            SqlCeCommand cmd = new SqlCeCommand(sql, conn);
            int nelems = (int)cmd.ExecuteScalar();
            if (nelems == 0) return null;
            result = new int[nelems];
            sql = "SELECT id FROM image";
            cmd = new SqlCeCommand(sql, conn);
            SqlCeDataReader dr = cmd.ExecuteReader();
            //if (dr.HasRows)
            {
                while (dr.Read())
                {
                    result[i++] = dr.GetInt32(0);
                }
            }
            dr.Close();
            conn.Close();
            if (i < nelems) return null;
            return result;
        }

        /// <summary>
        /// Заполнить элемент управления listView уменьшенными копиями изображений
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="indexes"></param>
        public void FillListView(ListView listView, int[] indexes)
        {
            int[] list;    // список идентификаторов изображений
            int i;
            string name;
            string path;
            if (indexes == null) return;
            SqlCeConnection conn = new SqlCeConnection(global::ContentBasedImageRetrieval.Properties.Settings.Default.cbirConnectionString);
            list = getIdxList(conn);
            string sql = "SELECT name, path FROM image WHERE id=@id";
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            SqlCeCommand cmd = new SqlCeCommand(sql, conn);
            ImageList picList = new ImageList();
            picList.ImageSize = new Size(120, 120);
            picList.ColorDepth = ColorDepth.Depth32Bit;
            ImageList picListLarge = new ImageList();
            picListLarge.ImageSize = new Size(120, 120);
            picListLarge.ColorDepth = ColorDepth.Depth32Bit;
            string[] names = new string[indexes.Length];
            for (i = 0; i < indexes.Length; i++)
            {
                SqlCeParameter param = new SqlCeParameter();
                param.ParameterName = "@id";
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = list[indexes[i]];            // получаем индекс изображения из массива indexes
                cmd.Parameters.Clear();
                cmd.Parameters.Add(param);

                SqlCeDataReader dr = cmd.ExecuteReader();
                //if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        name = dr.GetString(0);
                        names[i] = name;
                        path = dr.GetString(1);
                        Image im = Image.FromFile(path);
                        double coef = (double)im.Height / (double)im.Width;
                        int maxv = (im.Height > im.Width) ? im.Height : im.Width;
                        double scale = 120.0 / (double)maxv;
                        var bmp = new Bitmap((int)120, (int)120);
                        var graph = Graphics.FromImage(bmp);
                        var scaleWidth = (int)(im.Width * scale);
                        var scaleHeight = (int)(im.Height * scale);
                        graph.DrawImage(im, new Rectangle(0, 0, scaleWidth, scaleHeight));
                        //graph.DrawImage(im, new Rectangle(((int)im.Width - scaleWidth) / 2, ((int)im.Height - scaleHeight) / 2, scaleWidth, scaleHeight));

                        Image pic = bmp; // im.GetThumbnailImage(120, (int)(120 * coef), null, new IntPtr());
                        picList.Images.Add(pic);
                        picListLarge.Images.Add(pic);
                    }
                }
            }
            listView.LargeImageList = picListLarge;
            listView.SmallImageList = picList;
            listView.View = View.LargeIcon;
           
            for (i = 0; i < picList.Images.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i;
                item.Text = names[i];
                listView.Items.Add(item);
            }
        }

        public static double Scale(double fromMin, double fromMax, double toMin, double toMax, double x)
        {
            if (fromMax - fromMin == 0) return 0;
            double value = (toMax - toMin) * (x - fromMin) / (fromMax - fromMin) + toMin;
            if (value > toMax)
            {
                value = toMax;
            }
            if (value < toMin)
            {
                value = toMin;
            }
            return value;
        }

        public void AddImageToDatabase(Bitmap img, string name)
        {
            int i, j;
            Color c;
            YIQ yiq = new YIQ();
            
            if (null == img) return;
            int width, height;
            width = img.Width;
            height = img.Height;
            if (width < 2) return;
            if (height < 2) return;
            if ((width % 2) != 0) width -= 1;
            if ((height % 2) != 0) height -= 1;
            double[,] Red = new double[width, height];
            double[,] Green = new double[width, height];
            double[,] Blue = new double[width, height];
            for(j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                {
                    c = img.GetPixel(i, j);
                    Red[i, j] = (double)Scale(0, 255, -1, 1, c.R);
                    Green[i, j] = (double)Scale(0, 255, -1, 1, c.G);
                    Blue[i, j] = (double)Scale(0, 255, -1, 1, c.B);
                }
            yiq.Create_YIQ_from_RGB(Red, Green, Blue);
            
            // соединение с базой данных
            SqlCeConnection conn = new SqlCeConnection(global::ContentBasedImageRetrieval.Properties.Settings.Default.cbirConnectionString);
            // получение имени файла для сохранения в базу данных
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            SqlCeCommand cmd = new SqlCeCommand("select max(id) from image", conn);
            SqlCeDataReader rd = cmd.ExecuteReader();
            int next_id = 0;
            //if (rd.HasRows)
            {
                if (rd.Read())
                {
                    if (rd.IsDBNull(0))
                    {
                        next_id = 0;
                    }
                    else
                    {
                        next_id = rd.GetInt32(0); // выбираем наибольшее значение идентификатора из базы данных
                        next_id++;                // получаем следующее значение идентификатора
                        if (next_id > max_idx) max_idx = next_id;
                    }
                };
            }
            if (next_id <= max_idx)
            {
                max_idx++;
                next_id = max_idx;
            }
            rd.Close();
            conn.Close();
            string path = "images\\";
            path = path + next_id.ToString();
            path = path + ".jpg";
            //GC.Collect(); // освобождаем не используемые объекты
            //System.IO.File.Delete(path);
            img.Save(path, ImageFormat.Jpeg);
            AddImage(conn, path, name, yiq.image_Y, yiq.image_I, yiq.image_Q, next_id);
        }

        /// <summary>
        /// удалить изображение из базы данных
        /// </summary>
        /// <param name="index"></param>
        public void RemoveImageFromDatabase(int index)
        {
            int[] list;    // список идентификаторов изображений
            SqlCeConnection conn = new SqlCeConnection(global::ContentBasedImageRetrieval.Properties.Settings.Default.cbirConnectionString);
            list = getIdxList(conn);
            if (index >= list.Length) return;
            int id = list[index];
            string sql = "DELETE FROM image WHERE id=@id";
            SqlCeCommand cmd = new SqlCeCommand(sql, conn);
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            SqlCeParameter param = new SqlCeParameter();
            param.ParameterName = "@id";
            param.SqlDbType = System.Data.SqlDbType.Int;
            param.Direction = System.Data.ParameterDirection.Input;
            param.Value = id;
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();

            sql = "DELETE FROM positive WHERE id=@id";
            cmd = new SqlCeCommand(sql, conn);
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            param = new SqlCeParameter();
            param.ParameterName = "@id";
            param.SqlDbType = System.Data.SqlDbType.Int;
            param.Direction = System.Data.ParameterDirection.Input;
            param.Value = id;
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();

            sql = "DELETE FROM negative WHERE id=@id";
            cmd = new SqlCeCommand(sql, conn);
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            param = new SqlCeParameter();
            param.ParameterName = "@id";
            param.SqlDbType = System.Data.SqlDbType.Int;
            param.Direction = System.Data.ParameterDirection.Input;
            param.Value = id;
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();
        }

    }
}
