using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Task_6
{
    class Program
    {
        static Random random = new Random();

        static BlockingCollection<int> q = new BlockingCollection<int>();

        static void Main(string[] args)
        {
            var threads = new Thread[10];

            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(Show);
                threads[i].Start();

                ThreadPool.QueueUserWorkItem(Create);
            }

            q.CompleteAdding();

            foreach (var t in threads)
                t.Join();

            Console.ReadKey();
        }

        private static void Create(object state)
        {
            q.Add(random.Next(1, 10));
        }

        private static void Show()
        {
            foreach (var i in q.GetConsumingEnumerable())
            {
                Console.WriteLine(i);
            }
        }
    }
}
