using System;
using System.Threading.Tasks;
using Task_3.Models;

namespace Task_3
{
    class OperationWithMatrix
    {
        internal static int[,] MultipliesMatrices(int[,] a, int[,] b)
        {
            var matrices = new Matrices
            {
                firstMatrix = a,
                secondMatrix = b
            };

            if (matrices.firstMatrix.GetLength(1) != matrices.secondMatrix.GetLength(0))
            {
                Console.WriteLine("\n Number of columns in First Matrix should be equal to Number of rows in Second Matrix.");
                Console.WriteLine("\n Please re-enter correct dimensions.");
                return null;
            }

            var halfMatrixNumber = matrices.firstMatrix.GetLength(0) / 2;
            var endMatrixNumber = matrices.firstMatrix.GetLength(0);

            var tasks1 = Task.Factory.StartNew(
                    () => ParallelMultiplyMatrixCalculateCall(
                        new ParallelParamets
                        {
                            startNumber = 0,
                            endNumber = halfMatrixNumber
                        },
                        matrices));

            var tasks2 = Task.Factory.StartNew(
                    () => ParallelMultiplyMatrixCalculateCall(
                        new ParallelParamets
                            {
                                startNumber = halfMatrixNumber,
                                endNumber = endMatrixNumber
                            },
                        matrices));

            Task.WaitAll(tasks1, tasks2);

            var result = SumMatricesResult(new Matrices{firstMatrix = tasks1.Result,secondMatrix = tasks2.Result} );

            return result;
        }

        internal static void ShowMatrix(int[,] result)
        {
            if (result == null)
            {
                return;
            }

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    Console.Write(result[i, j] + " ");
                }

                Console.WriteLine();
            }
        }

        private static int[,] SumMatricesResult(Matrices matrices)
        {
            if (matrices.firstMatrix == null || matrices.secondMatrix == null)
            {
                return null;
            }

            var result = new int[matrices.firstMatrix.GetLength(0), matrices.secondMatrix.GetLength(1)];

            for (int row = 0; row < matrices.firstMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrices.firstMatrix.GetLength(1); col++)
                {
                    result[row, col] = matrices.firstMatrix[row, col] + matrices.secondMatrix[row, col];
                }
            }

            return result;
        }

        private static int[,] ParallelMultiplyMatrixCalculateCall(ParallelParamets parameters, Matrices matrices)
        {
            if (matrices.firstMatrix == null || matrices.secondMatrix == null)
            {
                return null;
            }

            var result = new int[matrices.firstMatrix.GetLength(0), matrices.secondMatrix.GetLength(1)];

            Parallel.For(parameters.startNumber, parameters.endNumber, row =>
            {
                for (int col = 0; col < matrices.secondMatrix.GetLength(1); ++col)
                {
                    for (int i = 0; i < matrices.secondMatrix.GetLength(0); ++i)
                    {
                        result[row, col] += matrices.firstMatrix[row, i] * matrices.secondMatrix[i, col];
                    }
                }
            });

            return result;
        }
    }
}
