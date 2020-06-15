namespace MessageQuery.MQRabbit
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using RabbitMQ.Client;

    public static class MessageSender
    {
        private const int ChunkSize = 16384;

        public static void SendMessage(List<FileViewModel> files)
        {
            using (var model = MQConnectionService.GetRabbitChannel(Constants.QueryName))
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

            var isFinish = false;
            var fileStream = File.OpenRead(file.FileName);
            var fileSize = Convert.ToInt32(fileStream.Length);

            while (true)
            {
                if (fileSize <= 0)
                {
                    break;
                }

                int read;
                byte[] buffer;
                if (fileSize > ChunkSize)
                {
                    buffer = new byte[ChunkSize];
                    read = fileStream.Read(buffer, 0, ChunkSize);
                }
                else
                {
                    buffer = new byte[fileSize];
                    read = fileStream.Read(buffer, 0, fileSize);
                    isFinish = true;
                }

                var basicProperties = model.CreateBasicProperties();

                basicProperties.Headers = new Dictionary<string, object>
                {
                    { "output-file", file.Title }, { "finished", isFinish }
                };

                model.BasicPublish(string.Empty, Constants.QueryName, basicProperties, buffer);
                fileSize -= read;
            }

            Console.WriteLine("Chunks publish is complete.");
        }
    }
}