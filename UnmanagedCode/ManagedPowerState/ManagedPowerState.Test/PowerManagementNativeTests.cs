namespace ManagedPowerState.Test
{
    using Xunit;
    using Xunit.Abstractions;

    public class PowerManagementNativeTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        private readonly IHibernationStateManagement hibernationStateManagement = new HibernationStateManagement();
        private readonly IPowerState powerState = new PowerState();

        public PowerManagementNativeTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void CallNtPowerInformation_SystemBatteryState()
        {
            var result = this.powerState.GetSystemBatteryState();
            this.testOutputHelper.WriteLine(result.ToString());
        }

        [Fact]
        public void CallNtPowerInformation_SystemPowerInformation()
        {
            var result = this.powerState.GetSystemPowerInformation();
            this.testOutputHelper.WriteLine(result.ToString());
        }

        [Fact]
        public void CallNtPowerInformation_LastWakeTime()
        {
            var result = this.powerState.GetLastWakeTime();
            this.testOutputHelper.WriteLine(result.ToString());
        }

        [Fact]
        public void CallNtPowerInformation_LastSleepTime()
        {
            var result = this.powerState.GetLastSleepTime();
            this.testOutputHelper.WriteLine(result.ToString());
        }

        [Fact]
        public void PutComputerToHibernateTest()
        {
            this.hibernationStateManagement.PutComputerToHibernate();
        }

        [Fact]
        public void PutComputerToSleepTest()
        {
            this.hibernationStateManagement.PutComputerToSleep();
        }

        [Fact]
        public void CallNtPowerInformation_GetLastSleepTimeInSeconds()
        {
            var result = this.powerState.GetLastSleepTimeInSeconds();
            this.testOutputHelper.WriteLine(result.ToString());
        }

        [Fact]
        public void CallNtPowerInformation_GetLastWakeTimeInSeconds()
        {
            var result = this.powerState.GetLastWakeTimeInSeconds();
            this.testOutputHelper.WriteLine(result.ToString());
        }
    }
}
