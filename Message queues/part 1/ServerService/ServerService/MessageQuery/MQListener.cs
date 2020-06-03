namespace ServerService
{
    using System;

    using MessageQueues;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    using ServerService.DataService;

    public class MQListener
    {
        public static void MessageReceiver()
        {
            using (var model = MQConnection.GetRabbitChannel(Constants.QueryName))
            {
                var consumer = new EventingBasicConsumer(model);
                consumer.Received += Consumer_Received;
                
                model.BasicConsume(Constants.QueryName, true, consumer);
                Console.ReadLine();
            }
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs args)
        {
            var body = args.Body.ToArray();
            MessageSaver.SaveFileToDatabase(body);
            Console.WriteLine(Constants.Notification);
        }
    }
}
