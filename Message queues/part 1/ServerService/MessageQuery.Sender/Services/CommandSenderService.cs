namespace MessageQueues.Services
{
    using RabbitMQ.Client;

    using Constants = MessageQueues.Constants;

    public class CommandSenderService
    {
        public static void SendCommand(byte[] commands)
        {
            using (var model = MQConnection.GetRabbitChannel())
            {
                //model.ExchangeDeclare(exchange: "topic_command", type: "topic");

                IBasicProperties basicProperties = model.CreateBasicProperties();
                basicProperties.Persistent = false;
                model.BasicPublish("topic.exchange", Constants.Queries.CommandQuery, basicProperties, commands);
            }
        }
    }
}