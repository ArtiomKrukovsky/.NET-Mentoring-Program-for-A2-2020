namespace MessageQuery.MQRabbit
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DirectoryListener
    {
        public static void FindAndPublishExistFiles()
        {
            if (!Directory.Exists(Constants.DirectoryPath))
            {
                return;
            }

            var files = FindExistFiles();
            MessageSender.SendMessage(files);
        }

        public static void ListenDirectoryAndPublishFiles()
        {
            var watcher = new FileSystemWatcher
            {
                Path = Constants.DirectoryPath,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName |
                NotifyFilters.DirectoryName,

                Filter = $"*{Constants.FileExpansion}"
            };
            
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
            MessageSender.SendMessage(new List<FileViewModel>
            {
                new FileViewModel
                {
                    FileName = e.FullPath,
                    Title = e.Name
                }
            });
        }

        private static List<FileViewModel> FindExistFiles()
        {
            var directory = new DirectoryInfo(Constants.DirectoryPath);
            var files = directory.GetFiles()
                .Where(x => x.Extension == Constants.FileExpansion)
                .Select(x => new FileViewModel
                {
                    FileName = x.FullName,
                    Title = x.Name
                }).ToList();
            
            return files;
        }
    }
}