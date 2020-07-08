namespace MessageQueues
{
    using System;

    using MessageQuery.MQRabbit;
    using MessageQuery.MQRabbit.Services;
    using MessageQuery.Receiver.Services;

    class Program
    {
        static void Main(string[] args)
        {
            string userName;

            do
            {
                Console.WriteLine("Enter your name:");
                userName = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(userName));

            StatusSenderService.SendStatus("Starting dictionary listening...");

            CommandReceiverService.ReceiveCommandMessage(userName);

            DirectoryListenerService.FindAndPublishExistFiles();
            DirectoryListenerService.ListenDirectoryAndPublishFiles();
        }
    }
}
