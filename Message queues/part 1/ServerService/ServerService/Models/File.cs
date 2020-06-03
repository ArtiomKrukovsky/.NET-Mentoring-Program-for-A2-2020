namespace ServerService.Models
{
    public class File
    {
        public int FileId { get; set; }

        public string FileName { get; set; }

        public string Title { get; set; }

        public byte[] Data { get; set; }
    }
}
