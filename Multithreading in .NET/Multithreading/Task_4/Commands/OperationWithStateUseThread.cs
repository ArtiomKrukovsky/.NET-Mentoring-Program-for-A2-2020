using System;

namespace Task_4.Commands
{
    using System.Threading;

    internal class OperationWithStateUseThread: WorkWithThreading
    {
        protected override void TakeLockResources()
        {
            return;
        }

        protected override void StartTreadMethod(int localState)
        {
            var thread = new Thread(new ParameterizedThreadStart(this.OperationWithState));
            thread.Start(localState);

            thread.Join();
        }

        protected override void ShowStateNumber(int localState)
        {
            Console.WriteLine($"Thread-{localState}");
        }
    }
}
