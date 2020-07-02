namespace MessageQueues
{
    public static class Constants
    {
        public static partial class Queries
        {
            public const string DataQuery = "data-query";

            public const string StatusQuery = "status-query";

            public const string CommandQuery = "command-query";
        }
        
        /// <summary>
        /// These value are suitable for a specific PC.
        /// </summary>
        public static string ConfigFilePath = @"E:\.NET-Mentoring-Program-for-A2-2019\Message queues";

        public static string ConfigFileName = "app.config.json";
    }
}
