namespace MessageQueues
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    using ServerService.DataService;

    using File = ServerService.Models.File;

    public class MQListener
    {
        public static void ReceiveMessageAndPublish()
        {
            using (var model = MQConnection.GetRabbitChannel(Constants.QueryName))
            {
                var consumer = new EventingBasicConsumer(model);
                consumer.Received += Consumer_Received;
                
                model.BasicConsume(Constants.QueryName, true, consumer);
                Console.WriteLine("Press \'q\' to stop listening message query.");
                while (Console.Read() != 'q')
                {
                }
            }
        }

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
                    Console.WriteLine("Received a chunks!");

                    IDictionary<string, object> headers = args.BasicProperties.Headers;

                    if (fileName.Equals(string.Empty))
                    {
                        fileName = Encoding.UTF8.GetString(headers["output-file"] as byte[]);
                    }

                    var isLastChunk = Convert.ToBoolean(headers["finished"]);
                    fileData.AddRange(args.Body.ToArray());

                    Console.WriteLine("Chunk saved. Finished? {0}", isLastChunk);
                    model.BasicAck(args.DeliveryTag, false);

                    if (isLastChunk)
                    {
                        MessageSaver.SaveFileToDatabase(new File { Data = fileData.ToArray(), Title = fileName });
                        fileName = ResetDataForReceiver(out fileData);
                    }
                };

                model.BasicConsume(Constants.QueryName, false, consumer);
                Console.WriteLine("Press \'q\' to stop listening message query.");
                while (Console.Read() != 'q')
                {
                }
            }
        }

        private static string ResetDataForReceiver(out List<byte> fileData)
        {
            var fileName = string.Empty;
            fileData = new List<byte>();
            return fileName;
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs args)
        {
            var body = args.Body.ToArray();
            var fileModel = Desserialize(body);

            MessageSaver.SaveFileToDatabase(fileModel);
            Console.WriteLine(Constants.Notification);
        }

        public static File Desserialize(byte[] data)
        {
            var result = new File();
            using (var m = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(m))
                {
                    result.FileName = reader.ReadString();
                    result.Title = reader.ReadString();
                    result.Data = reader.ReadBytes(Convert.ToInt32(m.Length));
                }
            }

            return result;
        }
    }
}
