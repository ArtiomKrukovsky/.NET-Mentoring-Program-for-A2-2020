namespace ServerService
{
    using System;
    using System.IO;

    using MessageQueues;
    using MessageQueues.Services;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the path where you want to save the files:");
            var path = Console.ReadLine();

            if (!Directory.Exists(path))
            {
                Console.WriteLine("Directory with this path don't exist, press any key");
                Console.ReadKey();
                return;
            }

            StatusReceiverService.ReceiveStatusMessage();
            MQListener.ReceiveChunkedMessages(path);
        }
    }
}
