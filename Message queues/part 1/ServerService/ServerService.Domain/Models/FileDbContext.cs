namespace ServerService.Models
{
    using System.Data.Entity;

    public class FileDbContext : DbContext
    {
        public DbSet<File> Files { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public FileDbContext()
            : base("MessageDB")
        {
        }
    }
}
