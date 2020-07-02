namespace MessageQuery.MQRabbit
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using MessageQuery.MQRabbit.Services;

    public class DirectoryListenerService
    {
        public static void FindAndPublishExistFiles()
        {
            if (!Directory.Exists(Constants.DirectoryPath))
            {
                return;
            }

            var files = GetExistFiles();
            MessageSenderService.SendMessage(files);
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

            WaitingMessage();
            Console.WriteLine("Press \'q\' to stop directory listening.");
            while (Console.Read() != 'q')
            {
            }
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.Name + " " + e.ChangeType);
            StatusSenderService.SendStatus($"Find new file - '{e.Name}'.");

            MessageSenderService.SendMessage(new List<FileViewModel>
            {
                new FileViewModel
                {
                    FullName = e.FullPath,
                    Title = e.Name
                }
            });

            WaitingMessage();
        }

        private static List<FileViewModel> GetExistFiles()
        {
            var directory = new DirectoryInfo(Constants.DirectoryPath);
            var files = directory.GetFiles()
                .Where(x => x.Extension == Constants.FileExpansion)
                .Select(x => new FileViewModel
                {
                    FullName = x.FullName,
                    Title = x.Name
                }).ToList();
            
            return files;
        }

        private static void WaitingMessage()
        {
            StatusSenderService.SendStatus("Waiting for new files...");
        }
    }
}