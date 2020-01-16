using System;
using System.Threading.Tasks;

namespace Task_8
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите значение N");
                var n = Convert.ToInt32(Console.ReadLine());

                var result = SumAcync(n);
            }
        }

        private static async Task SumAcync(int n)
        {
            int sum = 0;

            await Task.Factory.StartNew(() =>
            {
                 for (int i = 0; i < n; i++)
                 {
                    sum += i;
                    Task.Delay(500).Wait();
                 }
            });

            Console.WriteLine($"Сумма чисел от 0 до {n} = {sum}");
        }
    }
}
