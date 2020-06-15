namespace MessageQueues
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

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

        private static void Consumer_Received(object sender, BasicDeliverEventArgs args)
        {
            var body = args.Body.ToArray();
            var fileModel = Desserialize(body);

            MessageSaver.SaveFileToDatabase(fileModel);
            Console.WriteLine(Constants.Notification);
        }

        public static Object ConvertBytesToFile(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();

                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);

                var file = binForm.Deserialize(memStream);
                return file;
            }
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
