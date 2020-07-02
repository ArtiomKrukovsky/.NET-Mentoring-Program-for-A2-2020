namespace ServerService.DataService
{
    using ServerService.Models;

    public class MessageSaver
    {
        public async static void SaveFileToDatabase(File fileModel)
        {
            using (var context = new FileDbContext())
            {
                context.Files.Add(fileModel);
                await context.SaveChangesAsync();
            }
        }

        public async static void SaveStatusToDatabase(Status status)
        {
            using (var context = new FileDbContext())
            {
                context.Statuses.Add(status);
                await context.SaveChangesAsync();
            }
        }
    }
}
