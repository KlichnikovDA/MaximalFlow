using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maxPotok
{
    // Класс для генерации матриц
    public static class MatrixGenerator
    {
        static Random rnd = new Random();

        // Метод для случайной генерации матрицы расстояний
        public static int[,] GenerateRandom(int N)
        {
            // Матрица для заполнения
            int[,] Matrix = new int[N, N];
            // середина матрицы
            int mid = N / 2;
            // величина максимального потока
            int s = 0;

            // заполнение второй части матрицы
            for (int i = 0; i < mid; i++)
                for (int j = mid; j < N; j++)
                {
                    Matrix[i, j] = rnd.Next(1, 4);
                    s += Matrix[i, j];
                }

            // заполнение первой части матрицы
            for (int i = 0; i < mid; i++)
                for (int j = i + 1; j < mid; j++)
                    Matrix[i, j] = rnd.Next(s, s + 10);

            // заполнение третьей части матрицы
            for (int i = mid; i < N; i++)
                for (int j = i + 1; j < N; j++)
                    Matrix[i, j] = rnd.Next(s, s + 10);

            // Перемешивание строк и столбцов матрицы
            for (int k = 0; k < N / 3; k++)
            {
                // Выбор вершин, которые будут переставлены
                int first = rnd.Next(1, N - 1), second = rnd.Next(1, N - 1);

                int temp;
                // Перестановка столбцов
                for (int i = 0; i < N; i++)
                {
                    temp = Matrix[i, first];
                    Matrix[i, first] = Matrix[i, second];
                    Matrix[i, second] = temp;
                }
                // Перестановка строк
                for (int j = 0; j < N; j++)
                {
                    temp = Matrix[first, j];
                    Matrix[first, j] = Matrix[second, j];
                    Matrix[second, j] = temp;
                }
            }

            // вывод созданной матрицы
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < N; j++)
                {
                    Console.Write("{0, -4}", Matrix[i, j]);
                }
            }

            Console.WriteLine("\n" + "Величина максимального потока (генератор): " + s);
            return Matrix;
        }

        // Метод для ручной генерации матрицы расстояний
        public static int[,] GenerateInput(int N)
        {
            // Матрица для заполнения
            int[,] Matrix = new int[N, N];
            // середина матрицы
            int mid = N / 2;
            // величина максимального потока
            int s = 0;
            string[] str;

            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Введите {0} строку матрицы: ", i + 1);
                str = Console.ReadLine().Split(' ');
                for (int j = 0; j < str.Length; j++)
                {
                    Matrix[i, j] = Int32.Parse(str[j]);
                    //if (i < mid && j >= mid)
                    //    s += Int32.Parse(str[j]);
                }
            }

            //// Перемешивание строк и столбцов матрицы
            //for (int k = 0; k < N / 3; k++)
            //{
            //    // Выбор вершин, которые будут переставлены
            //    int first = rnd.Next(1, N - 1), second = rnd.Next(1, N - 1);

            //    int temp;
            //    // Перестановка столбцов
            //    for (int i = 0; i < N; i++)
            //    {
            //        temp = Matrix[i, first];
            //        Matrix[i, first] = Matrix[i, second];
            //        Matrix[i, second] = temp;
            //    }
            //    // Перестановка строк
            //    for (int j = 0; j < N; j++)
            //    {
            //        temp = Matrix[first, j];
            //        Matrix[first, j] = Matrix[second, j];
            //        Matrix[second, j] = temp;
            //    }
            //}

            // вывод созданной матрицы
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < N; j++)
                {
                    Console.Write("{0, -4}", Matrix[i, j]);
                }
            }

            //Console.WriteLine("\n" + "Величина максимального потока (генератор): " + s);
            return Matrix;
        }
    }
}
