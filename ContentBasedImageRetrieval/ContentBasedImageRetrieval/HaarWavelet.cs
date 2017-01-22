using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContentBasedImageRetrieval
{
    /// <summary>
    /// 2D вейвлет-преобразование Хаара
    /// </summary>
    public class HaarWavelet
    {
        /// <summary>
        /// метод стандартного вейвлет-преобразования
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public double[,] standardDecomposition(double[,] image)
        {
            int rows = image.GetLength(0); // количество строк в исходном изображении
            int cols = image.GetLength(1); // количество столбцов в исходном изображении
            if (rows % 2 != 0) return null; // количество строк не кратно 2
            if (cols % 2 != 0) return null; // количество столбцов не кратно 2
            //if (cols != rows) return null;  // количество столбцов не равно количеству строк
            int r, c;                       // текущие строка и столбец
            // декомпозиция строк
            double[] temp;
            double[] decomposed;
            int temp_size = rows > cols ? rows : cols;
            temp = new double[temp_size];
            double[,] result = new double[rows, cols];
            // разложение строк
            for (r = 0; r < rows; r++)
            {
                for (c = 0; c < cols; c++) temp[c] = image[r, c];
                decomposed = decomposition(temp);
                for (c = 0; c < cols; c++) result[r, c] = decomposed[c];
            }
            // разложение столбцов
            for (c = 0; c < cols; c++)
            {
                for (r = 0; r < rows; r++)
                {
                    temp[r] = result[r, c];
                    decomposed = decomposition(temp);
                    result[r, c] = decomposed[r];
                }
            }
            return result;
        }

        private double[] decomposition(double[] elements)
        {
            int i, j;
            double factor = Math.Sqrt((double)elements.Length);
            for (i = 0; i < elements.Length; i++)
            {
                elements[i] = elements[i] / factor;
            }
            int g = elements.Length;

            while (g >= 2)
            {
                double[] temp = new double[g];
                for (j = 0; j < g; j++) temp[j] = elements[j];
                temp = decompositionStep(temp);
                for (j = 0; j < g; j++) elements[j] = temp[j];
                g = g / 2;
            }
            return elements;
        }

        private double[] decompositionStep(double[] elements)
        {
            int i;
            double root2 = Math.Sqrt(2.0);
            double[] newElements = new double[elements.Length];
            for (i = 0; i < elements.Length / 2; i++)
            {
                newElements[i] = (elements[2 * i] + elements[2 * i + 1]) / root2;
                newElements[elements.Length / 2 + i] =
                (elements[2 * i] - elements[2 * i + 1]) / root2;
            }
            return newElements;
        }

        

    }
}
