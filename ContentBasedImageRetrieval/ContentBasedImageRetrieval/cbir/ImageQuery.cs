using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContentBasedImageRetrieval.cbir
{
    /// <summary>
    /// класс выполнения запроса к изображению
    /// </summary>
    public class ImageQuery
    {

        public const double  WEIGHT_Y_0 = 5.00;
        public const double  WEIGHT_Y_1 =  .83;
        public const double  WEIGHT_Y_2 = 1.01;
        public const double  WEIGHT_Y_3 =  .52;
        public const double  WEIGHT_Y_4 =  .47;
        public const double  WEIGHT_Y_5 =  .30;
    
        public double[] WEIGHTS_Y = new double[]{
	    WEIGHT_Y_0, WEIGHT_Y_1, WEIGHT_Y_2, WEIGHT_Y_3, 
	    WEIGHT_Y_4, WEIGHT_Y_5};
    
        public const double  WEIGHT_I_0 = 19.21;
        public const double  WEIGHT_I_1 =  1.26;
        public const double  WEIGHT_I_2 =   .44;
        public const double  WEIGHT_I_3 =   .53;
        public const double  WEIGHT_I_4 =   .28;
        public const double  WEIGHT_I_5 =   .14;
    
        public double[] WEIGHTS_I = new double[]{
	    WEIGHT_I_0, WEIGHT_I_1, WEIGHT_I_2, WEIGHT_I_3, 
	    WEIGHT_I_4, WEIGHT_I_5};

        public const double  WEIGHT_Q_0 = 34.37;
        public const double  WEIGHT_Q_1 =   .36;
        public const double  WEIGHT_Q_2 =   .45;
        public const double  WEIGHT_Q_3 =   .14;
        public const double  WEIGHT_Q_4 =   .18;
        public const double  WEIGHT_Q_5 =   .27;

        public double[] WEIGHTS_Q = new double[]{
	    WEIGHT_Q_0, WEIGHT_Q_1, WEIGHT_Q_2, WEIGHT_Q_3, 
	    WEIGHT_Q_4, WEIGHT_Q_5};


        public double[,] WEIGHTS;

        public ImageQuery()
        {
            WEIGHTS = new double[3, 6];
            int i;
            for (i = 0; i < 6; i++) WEIGHTS[0,i] = WEIGHTS_Y[i];
            for (i = 0; i < 6; i++) WEIGHTS[1, i] = WEIGHTS_I[i];
            for (i = 0; i < 6; i++) WEIGHTS[2, i] = WEIGHTS_Q[i];
        }

        public double[] scores;   // оценки соответствия для каждого изображения из базы данных

        /// <summary>
        /// Заполнить таблицу индексов соответствия
        /// </summary>
        /// <param name="db">база данных с изображениями</param>
        /// <param name="sig">набор характеристик искомого изображения. sig[0] - Y, sig[1] - I, sig[2] - Q</param>
        public void FillScores(signature[] sig, ImageDatabase db)
        {
            int i, j, k;
            int color;          // анализируемая цветовая составляющаяж
            signature db_sig;   // набор характеристик текущего изображения из базы данных для текущего канала цветности
            scores = null;
            if (db.NumberOfImages() == 0) return;
            scores = new double[db.NumberOfImages()];

            // цикл для каждого изображения из базы данных индекс соостветствия инициализируем значением 0
            for (i = 0; i < db.NumberOfImages(); i++)
            {
                scores[i] = 0.0;
            }
            // цикл для каждого канала цвета
            for (color = 0; color < 3; color++)
            {
                // цикл для каждого изображения из базы данных
                // модифицируем индекс с учетом весовых коэффициентов
                for (i = 0; i < db.NumberOfImages(); i++)
                {
                    db_sig = db.GetSignatureOfImage(i, color);
                    scores[i] += WEIGHTS[color,0] * Math.Abs(sig[color].average - db_sig.average);
                    // просмотр положительных наибольших элементов из sig
                    for (j = 0; j < sig[color].col_positive.Length; j++)
                    {
                        // поиск такого же элемента в db_sig
                        for (k = 0; k < db_sig.col_positive.Length; k++)
                            if ((sig[color].col_positive[j] == db_sig.col_positive[k]) &&
                                (sig[color].row_positive[j] == db_sig.row_positive[k]))
                            {
                                scores[i] -= WEIGHTS[color, bin(sig[color].col_positive[j], sig[color].row_positive[j])];
                                break;
                            }
                    }
                }
            }
        }

        /// <summary>
        /// группировка коэффициентов в наборы
        /// </summary>
        /// <param name="x">x позиция коэффициента</param>
        /// <param name="y">y позиция коэффициента</param>
        /// <returns></returns>
        private int bin(int x, int y)
        {
            double ln_2 = Math.Log(2);

            double bin = Math.Min(Math.Max(Math.Floor(Math.Log(x) / ln_2),
                               Math.Floor(Math.Log(y) / ln_2)), 5.0);

            return (int)bin;
        }

    }
}
