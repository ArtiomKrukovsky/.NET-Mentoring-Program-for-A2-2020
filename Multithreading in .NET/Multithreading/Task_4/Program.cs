using System;

namespace Task_4
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkWithThreading.OperationWithStateUseThread(10);
            WorkWithThreading.OperationWithStateUseThreadPool(10);
            
            Console.ReadKey();
        }
    }
}
