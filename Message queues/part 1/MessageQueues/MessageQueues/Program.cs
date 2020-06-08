namespace MessageQueues
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryListener.FindAndPublishExistFiles();
            DirectoryListener.DirectoryFilesListener();
        }
    }
}
