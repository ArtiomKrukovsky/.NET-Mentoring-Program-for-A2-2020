namespace ManagedPowerState.Extension_Methods
{
    using System;

    public static class BooleanExtension
    {
        public static IntPtr ToIntPtr(this bool value)
        {
            var bytes = BitConverter.GetBytes(value);
            return new IntPtr(bytes[0]);
        }

        public static byte ToByte(this bool value)
        {
            var bytes = BitConverter.GetBytes(value);
            return bytes[0];
        }
    }
}
