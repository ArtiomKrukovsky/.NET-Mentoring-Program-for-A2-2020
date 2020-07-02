namespace MessageQueues
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class MQListener
    {
        public static void ReceiveChunkedMessages(string path)
        {
            using (var model = MQConnection.GetRabbitChannel(Constants.DataQueryName))
            {
                model.BasicQos(0, 1, false);
                var consumer = new EventingBasicConsumer(model);

                consumer.Received += (sender, args) =>
                {
                    IDictionary<string, object> headers = args.BasicProperties.Headers;

                    var fileName = GetFileName(headers);
                    var isLastChunk = GetChunkStatus(headers);
                    var localFileName = $@"{path}\{fileName}";

                    using (FileStream fileStream = new FileStream(localFileName, FileMode.Append, FileAccess.Write))
                    {
                        fileStream.Write(args.Body.ToArray(), 0, args.Body.Length);
                        fileStream.Flush();
                    }

                    model.BasicAck(args.DeliveryTag, false);

                    if (isLastChunk)
                    {
                        Console.WriteLine($"File '{ fileName }' saved!");
                    }
                };
                
                model.BasicConsume(Constants.DataQueryName, false, consumer);
                Console.WriteLine("Start file processing...\nPress \'q\' to stop listening message query.");
                while (Console.Read() != 'q')
                {
                }
            }
        }

        private static bool GetChunkStatus(IDictionary<string, object> headers)
        {
            return Convert.ToBoolean(headers["finished"]);
        }

        private static string GetFileName(IDictionary<string, object> headers)
        {
            var fileName = Encoding.UTF8.GetString(headers["output-file"] as byte[]);

            return fileName;
        }
    }
}
