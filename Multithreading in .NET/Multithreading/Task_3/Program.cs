using System;

namespace Task_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] a = { { 1, 4 }, { 2, 5 }};
            int[,] b = { { 3, 4 }, { 3, 7 }};

            var result = OperationWithMatrix.MultipliesMatrices(a, b);
            OperationWithMatrix.ShowMatrice(result);
            Console.ReadKey();
        }
    }
}
