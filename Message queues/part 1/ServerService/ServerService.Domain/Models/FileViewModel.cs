namespace ServerService.Models
{
    using System;

    [Serializable]
    public class FileViewModel
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public byte[] Data { get; set; }
    }
}