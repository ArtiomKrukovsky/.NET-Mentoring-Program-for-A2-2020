using System;
using System.Threading.Tasks;

namespace Task_8.Commands
{
    internal static class DataOperations
    {
        internal static async Task SumAcync(int n)
        {
            int sum = 0;

            await Task.Factory.StartNew(() =>
            {
                for (int i = 0; i <= n; i++)
                {
                    sum += i;
                    Task.Delay(500).Wait();
                }
            });

            ShowResult(n, sum);
        }

        private static void ShowResult(int n, int sum)
        {
            Console.WriteLine($"Сумма чисел от 0 до {n} = {sum}");
        }
    }
}
