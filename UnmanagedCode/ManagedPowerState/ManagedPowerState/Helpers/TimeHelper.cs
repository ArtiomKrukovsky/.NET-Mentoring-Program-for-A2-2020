namespace ManagedPowerState.Helpers
{
    internal class TimeHelper
    {
        internal static long GetMillisecondsFromNanoseconds(long time)
        {
            return time / 100000000;
        }
    }
}
