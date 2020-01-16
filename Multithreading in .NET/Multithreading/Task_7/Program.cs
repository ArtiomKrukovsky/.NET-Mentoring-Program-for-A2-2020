using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task_7
{
    class Program
    {
        static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        static CancellationToken token = cancelTokenSource.Token;

        static void Main(string[] args)
        {
            var tokenCancelModel = new TokenCancelModel
            {
                Token = token,
                CancelTokenSource = cancelTokenSource
            };

            Task.Factory.StartNew(CriteriaTaskOperations.Do).ContinueWith(result => CriteriaTaskOperations.CriteriaNumberOne())
                .ContinueWith(result => CriteriaTaskOperations.CriteriaNumberTwo(), TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.AttachedToParent)
                .ContinueWith(result => CriteriaTaskOperations.CriteriaNumberThree(tokenCancelModel), token, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.AttachedToParent, TaskScheduler.Default)
                .ContinueWith(result => CriteriaTaskOperations.CriteriaNumberTour(), TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.RunContinuationsAsynchronously);

            Console.ReadKey();
        }
    }
}
