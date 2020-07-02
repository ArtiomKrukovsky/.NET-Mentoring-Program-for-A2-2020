namespace ServerService.Models
{
    public class Status
    {
        public int StatusId { get; set; }

        public int MaxMessageSize { get; set; }

        public string CurrentStatus { get; set; }
    }
}