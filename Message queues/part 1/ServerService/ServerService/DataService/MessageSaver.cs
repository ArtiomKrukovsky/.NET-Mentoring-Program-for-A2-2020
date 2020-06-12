namespace ServerService.DataService
{
    using ServerService.Models;

    public class MessageSaver
    {
        public static void SaveFileToDatabase(File fileModel)
        {
            using (var context = new FileDbContext())
            {
                context.Files.Add(fileModel);
                context.SaveChanges();
            }
        }
    }
}
