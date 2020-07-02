namespace MessageQueues.Services
{
    using System.Text;
    using System.Text.Json;

    using RabbitMQ.Client.Events;

    using ServerService.Models;

    public class StatusReceiverService
    {
        public void ReceiveStatusMessage()
        {
            using (var model = MQConnection.GetRabbitChannel(Constants.Queries.StatusQuery))
            {
                var consumer = new EventingBasicConsumer(model);

                consumer.Received += (sender, args) =>
                {
                    var message = Encoding.UTF8.GetString(args.Body.ToArray());
                    var status = JsonSerializer.Deserialize<Status>(message);

                };
            }
        }
    }
}