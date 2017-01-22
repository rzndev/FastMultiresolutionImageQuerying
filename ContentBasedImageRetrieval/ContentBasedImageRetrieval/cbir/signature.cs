using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContentBasedImageRetrieval.cbir
{
    public class signature
    {
        public string name = null;  // наименование изображения
        public string path = null;  // путь к изображению
        public double average;      // коэффициент, пропорциональный среднему цвету всего изображения
        public int[] row_positive;  // позиции положительных коэфиициентов (строка)
        public int[] col_positive;  // позиции положительных коэфиициентов (столбец)
        public int[] row_negative;  // позиции отрицательных коэффициентов (строка)
        public int[] col_negative;  // позиции отрицательных коэффициентов (столбец)

        /// <summary>
        /// сформировать подпись изображения из данных вейвлет-преобразования
        /// </summary>
        /// <param name="data">данные</param>
        /// <param name="size">количество элементов в подписи</param>
        /// <returns></returns>
        public static signature MakeSignature(double[,] data, int size)
        {
            signature result = new signature();
            int rows = data.GetLength(0);
            int cols = data.GetLength(1);
            // набор коэффициентов. После обхода всех элементов данных содержит size наибольших элементов
            bool[] settedup = new bool[size]; // признак того, что элемент заполнен
            int[] row_values = new int[size]; //строки наибольших элементов
            int[] col_values = new int[size]; // столбцы наибольших элементов 
            int i;
            for (i = 0; i < size; i++) settedup[i] = false;
            int row, col;
            bool isFreeElement = true; // есть хотя бы один не заполненный элемент
            // обход по всем элементам data, кроме 0
            for(row = 0; row < rows; row++)
                for (col = 0; col < cols; col++)
                {
                    if (row == 0 && col == 0) continue;
                    for (i = 0; i < size; i++)
                    {
                        if (data[row, col] == 0) continue;
                        if (settedup[i] == false)
                        {
                            if (i == (size-1)) isFreeElement = false; // если заполняется последний элемент, сбрасываем признак наличия не заполненных элементов
                            settedup[i] = true;
                            row_values[i] = row;
                            col_values[i] = col;
                            break;
                        } else if (isFreeElement && i != (size - 1))
                        {
                            // если есть не заполненные элементы, добавляем текущий за последним заполненным
                            continue;
                        }  
                        else if (Math.Abs(data[row,col]) > Math.Abs(data[row_values[i],col_values[i]]))
                        {
                            row_values[i] = row;
                            col_values[i] = col;
                            break;
                        }
                    }
                }
            int positive_cnt;
            positive_cnt = 0;
            int negative_cnt;
            negative_cnt = 0;
            for (i = 0; i < size; i++)
                if (data[row_values[i], col_values[i]] > 0) positive_cnt++; else negative_cnt++;
            int positive_total = positive_cnt;
            int negative_total = negative_cnt;
            positive_cnt = 0;
            negative_cnt = 0;
            result.row_negative = new int[negative_total];
            result.col_negative = new int[negative_total];
            result.row_positive = new int[positive_total];
            result.col_positive = new int[positive_total];
            for(i = 0; i < size; i++)
            {
                if (data[row_values[i], col_values[i]] > 0) 
                {
                    result.row_positive[positive_cnt] = row_values[i];
                    result.col_positive[positive_cnt] = col_values[i];
                    positive_cnt++;
                }else 
                {
                    result.row_negative[negative_cnt] = row_values[i];
                    result.col_negative[negative_cnt] = col_values[i];
                    negative_cnt++;
                }
            }
            result.average = data[0, 0];

            return result;
        }
    }
}
