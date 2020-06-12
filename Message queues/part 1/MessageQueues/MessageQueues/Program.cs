namespace MessageQueues
{
    using MessageQuery.MQRabbit;

    class Program
    {
        static void Main(string[] args)
        {
            DirectoryListener.FindAndPublishExistFiles();
            DirectoryListener.DirectoryFilesListener();
        }
    }
}
