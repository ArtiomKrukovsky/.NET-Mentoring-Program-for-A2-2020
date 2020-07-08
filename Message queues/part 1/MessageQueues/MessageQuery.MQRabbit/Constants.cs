namespace MessageQuery.MQRabbit
{
    public static class Constants
    {
        public static partial class Queries
        {
            public const string DataQuery = "data-query";

            public const string StatusQuery = "status-query";

            public const string CommandQuery = "topic.command.";
        }

        /// <summary>
        /// These value are suitable for a specific PC.
        /// </summary>
        public const string DirectoryPath = @"E:\.NET-Mentoring-Program-for-A2-2019\Message queues\directory";

        public const string FileExpansion = ".mp4";

        public static int DefaultChunkSize = 16384;

        public const string Exchange = "topic.exchange";

        public const string RoutingKey = "*.command.*";
    }
}
