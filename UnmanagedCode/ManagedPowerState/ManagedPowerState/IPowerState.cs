namespace ManagedPowerState
{
    using System;
    using System.Runtime.InteropServices;

    using ManagedPowerState.Structs;

    [ComVisible(true)]
    [Guid("5BC81E2C-C08B-49B0-9D8B-0764D2C44B5D")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IPowerState
    {
        TimeSpan GetLastSleepTime();

        TimeSpan GetLastWakeTime();

        SystemBatteryState GetSystemBatteryState();

        SystemPowerInformation GetSystemPowerInformation();

        double GetLastSleepTimeInSeconds();

        double GetLastWakeTimeInSeconds();

        void ReserveHibernationFile();

        void RemoveHibernationFile();
    }
}
