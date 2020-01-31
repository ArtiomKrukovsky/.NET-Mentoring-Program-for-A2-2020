using System;
using System.Threading.Tasks;
using Task_3.Models;

namespace Task_3
{
    using System.Linq;

    class OperationWithMatrix
    {
        private static int[,] _a;

        private static int[,] _b;

        internal static int[,] MultipliesMatrices(int[,] a, int[,] b)
        {
            _a = a;
            _b = b;

            int halfMatrixNumber = _a.GetLength(0) / 2;
            int endMatrixNumber = _a.GetLength(0);

            var tasks1 = Task.Factory.StartNew(() => ParallelMatrixCalculateCall(new ParallelParamets { startNumber = 0, endNumber = halfMatrixNumber }));
            var tasks2 = Task.Factory.StartNew(() => ParallelMatrixCalculateCall(new ParallelParamets { startNumber = halfMatrixNumber, endNumber = endMatrixNumber }));

            Task.WaitAll(tasks1, tasks2);

            var result = SumMatricesResult(tasks1.Result, tasks2.Result);

            return result;
        }

        internal static void ShowMatrix(int[,] a)
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

        private static int[,] SumMatricesResult(int[,] a, int[,] b)
        {
            var result = new int[a.GetLength(0), b.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    result[i, j] = a[i, j] + b[i, j];
                }
            }

            return result;
        }

        private static int[,] ParallelMatrixCalculateCall(object parallelParameters)
        {
            var parameters = (ParallelParamets)parallelParameters;

            var result = new int[_a.GetLength(0), _b.GetLength(1)];

            Parallel.For(parameters.startNumber, parameters.endNumber, i =>
                {
                    result = MultiplyMatrixCalculated(i);
                });

            return result;
        }

        private static int[,] MultiplyMatrixCalculated(int iteration)
        {
            var result = new int[_a.GetLength(0), _b.GetLength(1)];

            for (int j = 0; j < _b.GetLength(1); ++j)
            {
                for (int k = 0; k < _b.GetLength(0); ++k)
                {
                    result[iteration, j] += _a[iteration, k] * _b[k, j];
                }
            }

            return result;
        }
    }
}
