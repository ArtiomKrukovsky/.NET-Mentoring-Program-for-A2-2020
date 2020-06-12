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
            Console.WriteLine("File: " + e.Name + " " + e.ChangeType);
            PublishFilesData(new List<FileViewModel> { new FileViewModel { FileName = e.FullPath, Title = e.Name } });
        }

        private static void PublishFilesData(List<FileViewModel> files)
        {
            SetFileBytesData(files);
            MessageSender.SendMessage(files);
        }

        private static void WriteInfoToConsole(List<FileViewModel> files)
        {
            foreach (var file in files)
            {
                Console.WriteLine($"File: {file.Title} - Existing");
            }
        }

        private static List<FileViewModel> FindExistFiles(string path)
        {
            var directory = new DirectoryInfo(path);
            var filesPath = directory.GetFiles().Where(x => x.Extension == Constants.FileExpansion)
                .Select(x => new FileViewModel { FileName = x.FullName, Title = x.Name }).ToList();

            return filesPath;
        }

        private static void SetFileBytesData(IEnumerable<FileViewModel> files)
        {
            foreach (var file in files)
            {
               file.Data = File.ReadAllBytes(file.FileName);
            }
        }
    }
}