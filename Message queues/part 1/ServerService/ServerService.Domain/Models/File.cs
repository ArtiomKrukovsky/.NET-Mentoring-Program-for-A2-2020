namespace ServerService.Models
{
    using System;

    [Serializable]
    public class File
    {
        public int FileId { get; set; }

        public string Title { get; set; }

        public byte[] Data { get; set; }
    }
}
