namespace MessageQueues
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DictionaryListener
    {
        public static List<byte[]> GetFiles(string path, string extension)
        {
            if (!Directory.Exists(path))
            {
                return new List<byte[]>();
            }

            return GetFilesData(path, extension);
        }

        private static List<byte[]> GetFilesData(string path, string extension)
        {
            var directory = new DirectoryInfo(path);
            var files = directory.GetFiles().Where(x => x.Extension == extension);

            return ConvertFilesToListBytes(files);
        }

        private static List<byte[]> ConvertFilesToListBytes(IEnumerable<FileInfo> files)
        {
            var fileData = new List<byte[]>();
            foreach (var fileInfo in files)
            {
                byte[] bytes = File.ReadAllBytes(fileInfo.FullName);
                fileData.Add(bytes);
            }

            return fileData;
        }
    }
}