namespace ManagedPowerState
{
    using System;
    using System.Runtime.InteropServices;

    using ManagedPowerState.Enums;
    using ManagedPowerState.Helpers;
    using ManagedPowerState.Structs;

    [ComVisible(true)]
    [Guid("90E5ABEA-0097-452A-B6C2-196B14A34136")]
    [ClassInterface(ClassInterfaceType.None)]
    public class PowerState : IPowerState
    {
        private uint success = 0;

        public double GetLastSleepTimeInSeconds()
        {
            var lastSleepTime = this.GetPowerInformation(PowerInformationLevel.LastSleepTime);
            var seconds = TimeHelper.GetMillisecondsFromNanoseconds(lastSleepTime);

            return Convert.ToDouble(seconds);
        }

        public double GetLastWakeTimeInSeconds()
        {
            var lastWakeTime = this.GetPowerInformation(PowerInformationLevel.LastWakeTime);
            var seconds = TimeHelper.GetMillisecondsFromNanoseconds(lastWakeTime);

            return Convert.ToDouble(seconds);
        }

        public TimeSpan GetLastSleepTime()
        {
            var lastSleepTime = this.GetPowerInformation(PowerInformationLevel.LastSleepTime);
            var seconds = TimeHelper.GetMillisecondsFromNanoseconds(lastSleepTime);

            return TimeSpan.FromMilliseconds(seconds);
        }

        public TimeSpan GetLastWakeTime()
        {
            var lastWakeTime = this.GetPowerInformation(PowerInformationLevel.LastWakeTime);
            var seconds = TimeHelper.GetMillisecondsFromNanoseconds(lastWakeTime);

            return TimeSpan.FromMilliseconds(seconds);
        }

        public SystemBatteryState GetSystemBatteryState()
        {
            var result = PowerManagementNative.CallNtPowerInformation(
                PowerInformationLevel.SystemBatteryState,
                IntPtr.Zero,
                0,
                out SystemBatteryState batteryInfo,
                (uint)Marshal.SizeOf(typeof(SystemBatteryState)));

            return this.IsSuccess(result) ? batteryInfo : new SystemBatteryState();
        }

        public SystemPowerInformation GetSystemPowerInformation()
        {
            var result = PowerManagementNative.CallNtPowerInformation(
                PowerInformationLevel.SystemPowerInformation,
                IntPtr.Zero,
                0,
                out SystemPowerInformation systemPowerInfo,
                (uint)Marshal.SizeOf(typeof(SystemBatteryState)));

            return this.IsSuccess(result) ? systemPowerInfo : new SystemPowerInformation();
        }

        private long GetPowerInformation(PowerInformationLevel informationLevel)
        {
            var result = PowerManagementNative.CallNtPowerInformation(
                informationLevel,
                IntPtr.Zero,
                0,
                out IntPtr time,
                (uint)Marshal.SizeOf(typeof(long)));

            return this.IsSuccess(result) ? time.ToInt64() : 0;
        }

        private bool IsSuccess(uint result)
        {
            return result == this.success;
        }
    }
}
