using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maxPotok
{
    class Program
    {
        // посещенные вершины
        static List<int> visited = new List<int>();
        // путь от s до t
        static Stack<int> path = new Stack<int>();
        // матрица
        public static int[,] matrix;
        // кол-во вершин в графе
        static int n;
        // минимальное значение в пути
        static int min = int.MaxValue;
        // начальная, конечная вершины
        public static int s = 0, t;

        // поиск пути от s до t
        public static bool DFS(int node)
        {
            // вершина посещена
            visited.Add(node);
            // для каждой смежной вершины к заданной
            for (int nextNode = 0; nextNode < n; nextNode++)
            {
                if (node == nextNode)
                    continue;
                // если следующая вершина не посещена и не является концевой
                if (!visited.Contains(nextNode) && matrix[node, nextNode] != 0)
                {
                    // если следующая вершина является стоком
                    if (nextNode == t || DFS(nextNode))
                    {
                        // добавляем вершину в путь
                        path.Push(nextNode);
                        return true;
                    }
                }
            }
            return false;
        }

        // поиск минимального значения в пути
        static void Min()
        {
            // начало ребра
            int nodeStart = 0;
            // конец ребра
            int nodeEnd;
            // кол-во вершин в пути
            int c = path.Count();

            for (int i = 0; i < c; i++)
            {
                nodeEnd = path.ElementAt(i);
                    min = Math.Min(Math.Abs(min), Math.Abs(matrix[nodeStart, nodeEnd]));
                nodeStart = nodeEnd;
            }
        }

        // поиск матрицы пропускных способностей
        static void MaxFlow()
        {
            if (!DFS(0))
                return;
            int nodeStart = 0;
            int nodeEnd;
            int c = path.Count();
            Min();

            for (int i = 0; i < c; i++)
            {
                nodeEnd = path.Pop();
                //Console.Write(nodeEnd + " ");
                if (matrix[nodeStart, nodeEnd] > 0)
                {
                    matrix[nodeStart, nodeEnd] -= min;
                    matrix[nodeEnd, nodeStart] -= min;
                }
                if (matrix[nodeStart, nodeEnd] < 0)
                {
                    matrix[nodeStart, nodeEnd] += min;
                    matrix[nodeEnd, nodeStart] += min;
                }
                nodeStart = nodeEnd;
            }
            //Console.WriteLine();
            visited.Clear();
            min = int.MaxValue;
            MaxFlow();
        }

        // величина максимального потока
        static int ValueMaxFlow()
        {
            int m = 0;
            for (int i = 0; i < n; i++)
                m += matrix[n - 1, i];
            return Math.Abs(m);
        }

        // вывод матрицы максимального потока
        static void PrintMatrMaxFlow()
        {
            Console.WriteLine();
            Console.WriteLine("Максимальный поток");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < n; j++)
                {
                    if (matrix[j, i] > 0)
                        Console.Write("0   ");
                    if (matrix[j, i] <= 0)
                        Console.Write("{0, -4}", Math.Abs(matrix[j, i]).ToString());
                }
            }
            Console.WriteLine("\n" + "Величина максимального потока: " + ValueMaxFlow());
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите n");
            n = Convert.ToInt32(Console.ReadLine());
            t = n - 1;
            matrix = new int[n, n];
            bool ok = false;
            do
            {
                Console.WriteLine("1 - генерация случайной матрицы");
                Console.WriteLine("2 - ручной ввод матрицы");
                var Result = Console.ReadKey();
                switch (Result.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        matrix = MatrixGenerator.GenerateRandom(n);
                        ok = true;
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        matrix = MatrixGenerator.GenerateInput(n);
                        ok = true;
                        break;
                    default:
                        ok = false;
                        break;
                }
            }while (!ok);
            MaxFlow();
            PrintMatrMaxFlow();

            Console.ReadLine();
        }
    }
}
