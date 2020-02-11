using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task_8.Commands
{
    internal static class DataOperations
    {
        internal static async Task SumAcync(int n, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("Операция прервана");
                return;
            }

            await Task.Factory.StartNew(() =>
            {
                Sum(n, token);
            });
        }

        private static void Sum(int n, CancellationToken token)
        {
            int sum = 0;

            for (int i = 0; i <= n; i++)
            {
                sum += i;

                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Операция прервана");
                    return;
                }

                Thread.Sleep(1000);
            }

            ShowResult(n, sum);
        }

        private static void ShowResult(int n, int sum)
        {
            Console.WriteLine($"Сумма чисел от 0 до {n} = {sum}");
        }
    }
}
