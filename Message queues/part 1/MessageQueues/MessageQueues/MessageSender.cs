namespace MessageQueues
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RabbitMQ.Client;

    public static class MessageSender
    {
        public static void SendMessage(IModel model, List<byte[]> files, string routingKey)
        {
            foreach (var file in files)
            {
                model.BasicPublish(String.Empty, routingKey, body: file);
            }
        }
    }
}