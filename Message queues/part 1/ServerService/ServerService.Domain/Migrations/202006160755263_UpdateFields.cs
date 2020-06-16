namespace ServerService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFields : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Files", "FileName");
            DropColumn("dbo.Files", "Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "Data", c => c.Binary());
            AddColumn("dbo.Files", "FileName", c => c.String());
        }
    }
}
