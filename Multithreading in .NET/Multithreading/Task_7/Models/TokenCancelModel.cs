using System.Threading;

namespace Task_7
{
    class TokenCancelModel
    {
        public CancellationToken Token { get; set; }

        public CancellationTokenSource CancelTokenSource { get; set; }
    }
}
