using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContentBasedImageRetrieval.cbir
{
    /// <summary>
    /// класс изображения, хранящий изображение в цветовом пространстве YIQ
    /// </summary>
    public class YIQ
    {
        public double[,] image_Y; // Y составляющая изображения
        public double[,] image_I; // I составляющая изображения
        public double[,] image_Q; // Q составляющая изображения

       public void Create_YIQ_from_RGB(double[,] image_R, double[,] image_G, double[,] image_B)
        {
            int rows = image_R.GetLength(0); // количество строк в изображении
            int cols = image_G.GetLength(1); // количество столбцов в изображении
            int r, c;                        // текущие строка и столбец
            image_Y = new double[rows, cols];
            image_I = new double[rows, cols];
            image_Q = new double[rows, cols];
            for(r = 0; r < rows; r++)
                for(c = 0; c < cols; c++)
                {
                    image_Y[r,c] = ((0.299d * image_R[r,c]) + (0.587d * image_G[r,c]) + (0.114d * image_B[r,c])) / 256;
                    image_I[r,c] = ((0.596d * image_R[r,c]) - (0.274d * image_G[r,c]) - (0.322d * image_B[r,c])) / 256;
                    image_Q[r,c] = ((0.212d * image_R[r,c]) - (0.523d * image_G[r,c]) + (0.311d * image_B[r,c])) / 256;
                }
        }
    }
}
