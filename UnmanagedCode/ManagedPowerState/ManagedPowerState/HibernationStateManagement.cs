namespace ManagedPowerState
{
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    [Guid("A3CA2116-FC7E-4888-85EB-F2A9FCF2D9BE")]
    [ClassInterface(ClassInterfaceType.None)]
    public class HibernationStateManagement : IHibernationStateManagement
    {
        public void PutComputerToHibernate()
        {
            PowerManagementNative.SetSuspendState(true, true, true);
        }

        public void PutComputerToSleep()
        {
            PowerManagementNative.SetSuspendState(false, true, true);
        }
    }
}
