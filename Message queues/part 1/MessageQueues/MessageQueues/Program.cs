namespace MessageQueues
{
    using MessageQuery.MQRabbit;
    using MessageQuery.MQRabbit.Services;

    class Program
    {
        static void Main(string[] args)
        {
            StatusSenderService.SendStatus("Starting dictionary listening...");
            DirectoryListenerService.FindAndPublishExistFiles();
            DirectoryListenerService.ListenDirectoryAndPublishFiles();
        }
    }
}
