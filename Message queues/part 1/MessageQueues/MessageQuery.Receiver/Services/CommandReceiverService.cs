namespace MessageQuery.Receiver.Services
{
    using System;
    using System.Text;

    using MessageQuery.MQRabbit;

    using Newtonsoft.Json;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    using Constants = MessageQuery.MQRabbit.Constants;

    public class CommandReceiverService
    {
        public static void ReceiveCommandMessage()
        {
            var model = MQConnection.GetRabbitChannel(Constants.Queries.CommandQuery);
            var consumer = new EventingBasicConsumer(model);

            consumer.Received += Command_Received;
            model.BasicConsume(Constants.Queries.CommandQuery, true, consumer);
        }

        private static void Command_Received(object sender, BasicDeliverEventArgs args)
        {
            var body = args.Body.ToArray();
            var commandsJson = Encoding.UTF8.GetString(body);

            ApplySettings(commandsJson);
        }

        private static void ApplySettings(string commandsJson)
        {
            if (string.IsNullOrEmpty(commandsJson))
            {
                return;
            }

            var commands = JsonConvert.DeserializeObject<dynamic>(commandsJson);

            Constants.DefaultChunkSize = commands.Size;
        }
    }
}