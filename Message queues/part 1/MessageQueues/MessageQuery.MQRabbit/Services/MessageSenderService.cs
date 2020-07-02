namespace MessageQuery.MQRabbit
{
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using System.IO;

    using MessageQuery.MQRabbit.Services;

    using RabbitMQ.Client;

    public static class MessageSenderService
    {
        public static void SendMessage(List<FileViewModel> files)
        {
            using (var model = MQConnectionService.GetRabbitChannel(Constants.Queries.DataQuery))
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
            StatusSenderService.SendStatus($"Processes the sequence - '{file.Title}'...");

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
                var buffer = pool.Rent(isFinish ? fileSize : Constants.DefaultChunkSize);

                var readChunkSize = GetStreamData(fileStream, buffer);
                fileSize -= readChunkSize;

                var basicProperties = BuildBasicProperties(model, file, isFinish);
                model.BasicPublish(string.Empty, Constants.Queries.DataQuery, basicProperties, buffer);
            }

            Console.WriteLine("Chunks publish is complete.");
            StatusSenderService.SendStatus($"Publish the sequence - '{file.Title}'.");
        }

        private static bool IsChunkLargerFileSize(int fileSize)
        {
            return fileSize <= Constants.DefaultChunkSize;
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