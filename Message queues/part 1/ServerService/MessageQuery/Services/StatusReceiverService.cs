namespace MessageQueues.Services
{
    using System;
    using System.Text;

    using Newtonsoft.Json;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    using ServerService.DataService;
    using ServerService.Models;

    public class StatusReceiverService
    {
        public static void ReceiveStatusMessage()
        {
            var model = MQConnection.GetRabbitChannel(MessageQueues.Constants.Queries.StatusQuery);
            var consumer = new EventingBasicConsumer(model);

            consumer.Received += Consumer_Received;
            model.BasicConsume(MessageQueues.Constants.Queries.StatusQuery, true, consumer);

            Console.WriteLine("Start status processing...");
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs args)
        {
            var body = args.Body.ToArray();

            var message = Encoding.UTF8.GetString(body);
            var status = JsonConvert.DeserializeObject<Status>(message);

            MessageSaver.SaveStatusToDatabase(status);
        }
    }
}