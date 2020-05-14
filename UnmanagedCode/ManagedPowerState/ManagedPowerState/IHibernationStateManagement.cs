namespace ManagedPowerState
{
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    [Guid("33BF08C2-D99C-46C2-9F61-C4B7C3060F88")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IHibernationStateManagement
    {
        void PutComputerToHibernate();

        void PutComputerToSleep();
    }
}
