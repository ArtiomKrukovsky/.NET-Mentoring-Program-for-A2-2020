using System;

namespace Task_4
{
    using Task_4.Commands;

    class Program
    {
        static void Main(string[] args)
        {
            WorkWithThreading workWithThreading1 = new OperationWithStateUseThread();

            WorkWithThreading workWithThreading2 = new OperationWithStateUseThreadPool();

            workWithThreading1.OperationWithState(5);
            workWithThreading2.OperationWithState(7);

            Console.ReadKey();
        }
    }
}
