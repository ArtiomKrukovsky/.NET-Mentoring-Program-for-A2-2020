namespace ServerService.DataService
{
    using ServerService.Models;

    public class MessageSaver
    {
        public static void SaveFileToDatabase(byte[] fileData)
        {
            using (var context = new FileDbContext())
            {
                var file = new File
                {
                    Title = "new",
                    Data = fileData,
                    FileName = "new",
                    FileId = 1
                };

                context.Files.Add(file);
                context.SaveChanges();
            }
        }
    }
}
