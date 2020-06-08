namespace MessageQueues
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DirectoryListener
    {
        public static void FindAndPublishExistFiles()
        {
            var path = Constants.DirectoryPath;
            if (!Directory.Exists(path))
            {
                return;
            }

            var filesPath = FindExistFiles(path);
            WriteInfoToConsole(filesPath);
            PublishFilesData(filesPath);
        }

        public static void DirectoryFilesListener()
        {
            var watcher = new FileSystemWatcher();

            watcher.Path = Constants.DirectoryPath;

            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            watcher.Filter = $"*{Constants.FileExpansion}";
            watcher.Created += OnCreated;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Press \'q\' to stop directory listening.");
            while (Console.Read() != 'q')
            {
            }
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            PublishFilesData(new List<string> { e.FullPath });
        }

        private static void PublishFilesData(List<string> filesPath)
        {
            var filesData = ConvertFilesToBytes(filesPath);
            MessageSender.SendMessage(filesData);
        }

        private static void WriteInfoToConsole(List<string> filesPath)
        {
            foreach (var filePath in filesPath)
            {
                Console.WriteLine($"File: {filePath} - Existing");
            }
        }

        private static List<string> FindExistFiles(string path)
        {
            var directory = new DirectoryInfo(path);
            var filesPath = directory.GetFiles().Where(x => x.Extension == Constants.FileExpansion)
                .Select(x => x.FullName).ToList();

            return filesPath;
        }

        private static IEnumerable<byte[]> ConvertFilesToBytes(IEnumerable<string> filesPath)
        {
            return filesPath.Select(File.ReadAllBytes).ToList();
        }
    }
}