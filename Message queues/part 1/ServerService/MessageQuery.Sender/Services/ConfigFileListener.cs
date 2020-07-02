namespace MessageQueues.Services
{
    using System;
    using System.IO;

    public class ConfigFileListener
    {
        public static void ListenConfigFile()
        {
            var watcher = new FileSystemWatcher
            {
                Path = Constants.ConfigFilePath,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName |
                             NotifyFilters.DirectoryName,

                Filter = Constants.ConfigFileName
            };

            watcher.Changed += Watcher_Changed;
            watcher.EnableRaisingEvents = true;
        }

        private static void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            var configBytes = File.ReadAllBytes(e.FullPath);
            CommandSenderService.SendCommand(configBytes);
        }
    }
}