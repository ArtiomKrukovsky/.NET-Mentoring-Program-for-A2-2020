namespace MessageQuery.MQRabbit.Services
{
    using System.Text;
    using System.Text.Json;

    using MessageQuery.MQRabbit.Models;

    using RabbitMQ.Client;
    using Constants = MessageQuery.MQRabbit.Constants;

    public class StatusSenderService
    {
        public static void SendStatus(string status)
        {
           var statusMessage = BuildStatusMessage(status);

           using (var model = MQConnection.GetRabbitChannel(Constants.Queries.StatusQuery))
           {
               IBasicProperties basicProperties = model.CreateBasicProperties();

               var body = SerializeMessageInBytes(statusMessage);
               model.BasicPublish("", Constants.Queries.StatusQuery, basicProperties, body);
           }
        }

        private static byte[] SerializeMessageInBytes(StatusViewModel statusMessage)
        {
            var json = JsonSerializer.Serialize(statusMessage);
            return Encoding.UTF8.GetBytes(json);
        }

        private static StatusViewModel BuildStatusMessage(string status)
        {
            return new StatusViewModel
            {
                MaxMessageSize = Constants.DefaultChunkSize, CurrentStatus = status
            };
        }
    }
}
