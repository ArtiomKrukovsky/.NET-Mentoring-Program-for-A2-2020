using System;
using System.Threading;

namespace Task_4.Commands
{
    class OperationWithStateUseThreadPool: WorkWithThreading
    {
        static Semaphore sem = new Semaphore(3, 3);

        protected override void TakeLockResources()
        {
            sem.WaitOne();
        }

        protected override void StartTreadMethod(int localState)
        {
            var thread = ThreadPool.QueueUserWorkItem(OperationWithState, localState);
            sem.Release();
        }

        protected override void ShowStateNumber(int localState)
        {
            Console.WriteLine($"ThreadPool-{localState}");
        }
    }
}
