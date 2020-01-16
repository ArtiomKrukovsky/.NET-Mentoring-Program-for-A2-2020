using System;
using System.Threading.Tasks;

namespace Multithreading
{
    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new Task[100];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Factory.StartNew(Callback, i);
            }

            Task.WaitAll(tasks);

            Console.WriteLine("All thread are finished");
            Console.ReadKey();
        }

        private static void Callback(object number)
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine($"Task #{number} – {i}");
            }
        }
    }
}
