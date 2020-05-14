namespace ManagedPowerState
{
    using System;
    using System.Runtime.InteropServices;

    using ManagedPowerState.Enums;
    using ManagedPowerState.Structs;

    internal static class PowerManagementNative
    {
        [DllImport("powrprof.dll")]
        internal static extern uint CallNtPowerInformation(
             PowerInformationLevel informationLevel,
             IntPtr inputBuffer,
             uint inputBufferSize,
             out SystemBatteryState outputBuffer,
             uint outputBufferSize);

        [DllImport("powrprof.dll")]
        internal static extern uint CallNtPowerInformation(
             PowerInformationLevel informationLevel,
             IntPtr inputBuffer,
             uint inputBufferSize,
             out SystemPowerInformation outputBuffer,
             uint outputBufferSize);

        [DllImport("powrprof.dll")]
        internal static extern uint CallNtPowerInformation(
             PowerInformationLevel informationLevel,
             IntPtr inputBuffer,
             uint inputBufferSize,
             out IntPtr outputBuffer,
             uint outputBufferSize);

        [DllImport("powrprof.dll")]
        internal static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);
    }
}
