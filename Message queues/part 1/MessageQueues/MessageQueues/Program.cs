namespace MessageQueues
{
    using MessageQuery.MQRabbit;

    class Program
    {
        static void Main(string[] args)
        {
            DirectoryListenerService.FindAndPublishExistFiles();
            DirectoryListenerService.ListenDirectoryAndPublishFiles();
        }
    }
}
