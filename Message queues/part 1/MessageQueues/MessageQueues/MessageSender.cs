namespace MessageQueues
{
    using System.Collections.Generic;

    using RabbitMQ.Client;

    public static class MessageSender
    {
        public static void SendMessage(IEnumerable<byte[]> filesData)
        {
            using (var model = MQConnection.GetRabbitChannel(Constants.QueryName))
            {
                PublishData(model, filesData, Constants.QueryName);
            }
        }

        private static void PublishData(IModel model, IEnumerable<byte[]> filesData, string routingKey)
        {
            foreach (var body in filesData)
            {
                model.BasicPublish(string.Empty, routingKey, body: body);
            }
        }
    }
}