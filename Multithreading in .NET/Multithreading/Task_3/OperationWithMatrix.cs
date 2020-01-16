using System;
using System.Threading.Tasks;

namespace Task_3
{
    class OperationWithMatrix
    {
        internal static int[,] MultipliesMatrices(int[,] a, int[,] b)
        {
            var result = new int[a.GetLength(0), b.GetLength(1)];

            Parallel.For(0, a.GetLength(0), i =>
            {
                for (int j = 0; j < b.GetLength(1); ++j)
                {
                    for (int k = 0; k < b.GetLength(0); ++k)
                    {
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            });

            return result;
        }

        internal static void ShowMatrice(int[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    Console.Write(a[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
