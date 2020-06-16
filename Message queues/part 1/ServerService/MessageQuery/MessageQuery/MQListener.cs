namespace MessageQueues
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    using ServerService.DataService;

    using File = ServerService.Models.File;

    public class MQListener
    {
        public static void ReceiveChunkedMessages()
        {
            using (var model = MQConnection.GetRabbitChannel(Constants.QueryName))
            {
                var fileData = new List<byte>();
                string fileName = string.Empty;
                model.BasicQos(0, 1, false);
                var consumer = new EventingBasicConsumer(model);

                consumer.Received += (sender, args) =>
                {
                    IDictionary<string, object> headers = args.BasicProperties.Headers;
                    fileName = GetFileName(fileName, headers);

                    var isLastChunk = Convert.ToBoolean(headers["finished"]);
                    fileData.AddRange(args.Body.ToArray());
                    model.BasicAck(args.DeliveryTag, false);

                    if (!isLastChunk)
                    {
                        return;
                    }

                    Console.WriteLine($"File '{ fileName }' saved!");
                    MessageSaver.SaveFileToDatabase(new File { Data = fileData.ToArray(), Title = fileName });
                    fileName = ResetDataForReceiver(out fileData);
                };
                
                model.BasicConsume(Constants.QueryName, false, consumer);

                Console.WriteLine("Press \'q\' to stop listening message query.");
                while (Console.Read() != 'q')
                {
                }
            }
        }

        private static string GetFileName(string fileName, IDictionary<string, object> headers)
        {
            if (!IsFileNameEmpty(fileName))
            {
                return fileName;
            }

            fileName = Encoding.UTF8.GetString(headers["output-file"] as byte[]);
            Console.WriteLine($"Receive a chunks for '{ fileName }' file ...");

            return fileName;
        }

        private static bool IsFileNameEmpty(string fileName)
        {
            return fileName.Equals(string.Empty);
        }

        private static string ResetDataForReceiver(out List<byte> fileData)
        {
            var fileName = string.Empty;
            fileData = new List<byte>();
            return fileName;
        }
    }
}
