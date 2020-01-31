namespace Task_4
{
    internal abstract class WorkWithThreading
    {
        internal void OperationWithState(object state)
        {
            this.TakeLockResources();

            var localState = (int)state;

            this.ShowStateNumber(localState);
            localState = DecrementStateNumber(localState);

            if (localState <= 0)
            {
                return;
            }

            this.StartTreadMethod(localState);
        }

        protected abstract void TakeLockResources();

        protected abstract void StartTreadMethod(int localState);

        protected abstract void ShowStateNumber(int localState);

        private static int DecrementStateNumber(int localState)
        {
            return --localState;
        }
    }
}
