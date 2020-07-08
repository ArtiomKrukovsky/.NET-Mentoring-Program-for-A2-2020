namespace MessageQuery.MQRabbit
{
    using RabbitMQ.Client;

    public class MQConnection
    {
        public static IModel GetRabbitChannel(string queueName)
        {
            var connection = GetRabbitConnection();
            IModel model = connection.CreateModel();

            model.QueueDeclare(queueName, true, false, false, null);
            return model;
        }

        public static IModel GetRabbitChannel()
        {
            var connection = GetRabbitConnection();
            IModel model = connection.CreateModel();
            return model;
        }

        private static IConnection GetRabbitConnection()
        {
            var factory = new ConnectionFactory
            {
                UserName = "root",
                Password = "root",
                VirtualHost = "/",
                HostName = "localhost",
                Port = 5672
            };

            var connection = factory.CreateConnection();
            return connection;
        }
    }
}
