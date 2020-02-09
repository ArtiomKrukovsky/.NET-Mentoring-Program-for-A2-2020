using System;
using Task_8.Commands;

namespace Task_8
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Введите значение N");
                    var n = Convert.ToInt32(Console.ReadLine());

                    if (n <= 0)
                    {
                        Console.WriteLine($"Введено неверное значение N");
                        continue;
                    }

                    var result = DataOperations.SumAcync(n);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
