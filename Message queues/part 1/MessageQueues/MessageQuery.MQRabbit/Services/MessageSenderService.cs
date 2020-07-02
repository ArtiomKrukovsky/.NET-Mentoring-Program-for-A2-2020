namespace MessageQuery.MQRabbit
{
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using System.IO;

    using RabbitMQ.Client;

    public static class MessageSenderService
    {
        private const int DefaultChunkSize = 16384;

        public static void SendMessage(List<FileViewModel> files)
        {
            using (var model = MQConnectionService.GetRabbitChannel(Constants.DataQueryName))
            {
                foreach (var file in files)
                {
                    PublishFileByChunkMessage(model, file);
                }
            }
        }
        
        private static void PublishFileByChunkMessage(IModel model, FileViewModel file)
        {
            Console.WriteLine($"File: {file.Title} - existing");
            Console.WriteLine("Starting file read operation...");

            var fullName = file.FullName;
            var fileStream = File.OpenRead(fullName);
            var fileSize = Convert.ToInt32(new FileInfo(fullName).Length);

            var pool = ArrayPool<byte>.Shared;
            while (true)
            {
                if (fileSize <= 0)
                {
                    break;
                }

                var isFinish = IsChunkLargerFileSize(fileSize);
                var buffer = pool.Rent(isFinish ? fileSize : DefaultChunkSize);

                var readChunkSize = GetStreamData(fileStream, buffer);
                fileSize -= readChunkSize;

                var basicProperties = BuildBasicProperties(model, file, isFinish);
                model.BasicPublish(string.Empty, Constants.DataQueryName, basicProperties, buffer);
            }

            Console.WriteLine("Chunks publish is complete.");
        }

        private static bool IsChunkLargerFileSize(int fileSize)
        {
            return fileSize <= DefaultChunkSize;
        }

        private static int GetStreamData(FileStream fileStream, byte[] buffer)
        {
            return fileStream.Read(buffer, 0, buffer.Length);
        }

        private static IBasicProperties BuildBasicProperties(IModel model, FileViewModel file, bool isFinish)
        {
            var basicProperties = model.CreateBasicProperties();

            basicProperties.Headers = new Dictionary<string, object>
            {
                { "output-file", file.Title }, { "finished", isFinish }
            };

            return basicProperties;
        }
    }
}