using System;

namespace Task_7
{
    class CriteriaTaskOperations
    {
        public static void Do()
        {
            Console.WriteLine("Do");
        }

        public static void CriteriaNumberOne()
        {
            Console.WriteLine("Complete task 1");
        }

        public static void CriteriaNumberTwo()
        {
            Console.WriteLine("Fail task 2");
            int zero = 0;
            int number = 10 / zero;
        }

        public static void CriteriaNumberThree(object tokenParametrs)
        {
            var _tokenParametrs = (TokenCancelModel)tokenParametrs;
            _tokenParametrs.CancelTokenSource.Cancel();

            if (_tokenParametrs.Token.IsCancellationRequested)
            {
                Console.WriteLine($"Canceled 3");
                _tokenParametrs.Token.ThrowIfCancellationRequested();
            }
        }

        public static void CriteriaNumberTour()
        {
            Console.WriteLine("Task 4");
        }
    }
}
