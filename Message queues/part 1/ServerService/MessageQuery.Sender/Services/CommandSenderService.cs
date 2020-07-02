namespace MessageQueues.Services
{
    using System;
    using System.Text;

    using RabbitMQ.Client;

    using Constants = MessageQueues.Constants;

    public class CommandSenderService
    {
        public static void SendCommand(byte[] commands)
        {
            using (var model = MQConnection.GetRabbitChannel(Constants.Queries.CommandQuery))
            {
                IBasicProperties basicProperties = model.CreateBasicProperties();
                model.BasicPublish(string.Empty, Constants.Queries.CommandQuery, basicProperties, commands);
            }
        }
    }
}