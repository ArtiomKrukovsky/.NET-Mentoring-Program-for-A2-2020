using System;

namespace Task_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] a = { { 1, 4 }, { 2, 5 }, { 3, 6 }, { 4, 9 } };
            int[,] b = { { 3, 4, 6, 8 }, { 3, 7, 3, 1 } };

            var result = OperationWithMatrix.MultipliesMatrices(a, b);
            OperationWithMatrix.ShowMatrix(result);
            Console.ReadKey();
        }
    }
}
