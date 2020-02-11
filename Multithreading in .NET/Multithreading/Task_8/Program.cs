using System;
using System.Threading;
using Task_8.Commands;

namespace Task_8
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int n = EnterNumber();

                while (true)
                {
                    var cancellationTokenSource = new CancellationTokenSource();
                    var token = cancellationTokenSource.Token;

                    if (n <= 0)
                    {
                        Console.WriteLine($"Введено неверное значение N");
                        continue;
                    }

                    var result = DataOperations.SumAcync(n, token);

                    n = EnterNumber();
                    cancellationTokenSource.Cancel();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }

        private static int EnterNumber()
        {
            Console.WriteLine("Введите значение N");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
