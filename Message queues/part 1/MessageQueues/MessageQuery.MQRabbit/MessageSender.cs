namespace MessageQuery.MQRabbit
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;

    using Newtonsoft.Json;

    using RabbitMQ.Client;

    public static class MessageSender
    {
        public static void SendMessage(List<FileViewModel> files)
        {
            using (var model = MQConnection.GetRabbitChannel(Constants.QueryName))
            {
                PublishData(model, files, Constants.QueryName);
            }
        }

        private static void PublishData(IModel model, IEnumerable<FileViewModel> filesJson, string routingKey)
        {
            foreach (var fileJson in filesJson)
            {
                var body = Serialize(fileJson);
                model.BasicPublish(string.Empty, routingKey, body: body);
            }   
        }

        private static IEnumerable<string> ConvertFilesToJson(List<FileViewModel> files)
        {
            var filesJson = files.Select(x => JsonConvert.SerializeObject(x));
            return filesJson;
        }

        private static byte[] ConvertJsonToBytes(string fileJson)
        {
            var bytes = Encoding.UTF8.GetBytes(fileJson);
            return bytes;
        }

        private static byte[] ConvertFileToBytes(FileViewModel model)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, model);
                return ms.ToArray();
            }
        }

        public static byte[] Serialize(FileViewModel model)
        {
            using (var m = new MemoryStream())
            {
                using (var writer = new BinaryWriter(m))
                {
                    writer.Write(model.FileName);
                    writer.Write(model.Title);
                    writer.Write(model.Data);
                }

                return m.ToArray();
            }
        }
    }
}