namespace MessageQueues
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = DictionaryListener.GetFiles(Constants.DirectoryPath, Constants.FileExpansion);

            using (var model = MQConnection.GetRabbitChannel(Constants.QueryName))
            {
                MessageSender.SendMessage(model, files, Constants.QueryName);
            }
        }
    }
}
