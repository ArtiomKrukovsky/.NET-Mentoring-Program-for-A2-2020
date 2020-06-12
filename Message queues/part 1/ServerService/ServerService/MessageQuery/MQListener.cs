namespace ServerService
{
    using System;
    using System.Text;
    using MessageQueues;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    using ServerService.DataService;
    using ServerService.Models;
    using Newtonsoft.Json;

    public class MQListener
    {
        public static void MessageReceiver()
        {
            using (var model = MQConnection.GetRabbitChannel(Constants.QueryName))
            {
                var consumer = new EventingBasicConsumer(model);
                consumer.Received += Consumer_Received;
                
                model.BasicConsume(Constants.QueryName, true, consumer);
                Console.WriteLine("Press \'q\' to stop listening message query.");
                while (Console.Read() != 'q')
                {
                }
            }
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs args)
        {
            var body = args.Body.ToArray();

            var message = Encoding.UTF8.GetString(body);
            var fileModel = JsonConvert.DeserializeObject<File>(message);

            MessageSaver.SaveFileToDatabase(fileModel);
            Console.WriteLine(Constants.Notification);
        }
    }
}
