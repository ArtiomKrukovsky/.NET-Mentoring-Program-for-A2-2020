namespace MessageQueues
{
    using MessageQuery.MQRabbit;
    using MessageQuery.MQRabbit.Services;
    using MessageQuery.Receiver.Services;

    class Program
    {
        static void Main(string[] args)
        {
            StatusSenderService.SendStatus("Starting dictionary listening...");

            CommandReceiverService.ReceiveCommandMessage();

            DirectoryListenerService.FindAndPublishExistFiles();
            DirectoryListenerService.ListenDirectoryAndPublishFiles();
        }
    }
}
