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
        public static void ReceiveCommandMessage(string userName)
        {
            var query = Constants.Queries.CommandQuery + userName.ToLower();

            var model = MQConnection.GetRabbitChannel(query);
            var consumer = new EventingBasicConsumer(model);

            model.QueueBind(query, Constants.Exchange, Constants.RoutingKey);

            consumer.Received += Command_Received;
            model.BasicConsume(query, true, consumer);
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

            if (commands.Size != null)
            {
                Constants.DefaultChunkSize = commands.Size;
                Console.WriteLine("New command was applied");
            }
        }
    }
}