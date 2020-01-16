using System;
using System.Threading;

namespace Task_4
{
    class WorkWithThreading
    {
        static Semaphore sem = new Semaphore(3, 3);

        public static void OperationWithStateUseThread(object state)
        {
            var localState = (int)state;

            ShowStateNumber(localState);
            DecrementStateNumber(localState);

            if (localState <= 0)
            {
                return;
            }

            var thread = new Thread(new ParameterizedThreadStart(OperationWithStateUseThread));
            thread.Start(localState);

            thread.Join();
        }

        public static void OperationWithStateUseThreadPool(object state)
        {
            sem.WaitOne();

            var localState = (int)state;

            ShowStateNumber(localState);
            DecrementStateNumber(localState);

            if (localState <= 0)
            {
                return;
            }

            var thread = ThreadPool.QueueUserWorkItem(OperationWithStateUseThreadPool, localState);

            sem.Release();
        }

        private static void ShowStateNumber(int localState)
        {
            Console.WriteLine($"Thread-{localState}");
        }

        private static void DecrementStateNumber(int localState)
        {
            localState--;
        }
    }
}
