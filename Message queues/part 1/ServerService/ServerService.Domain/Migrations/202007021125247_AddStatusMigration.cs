namespace ServerService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        MaxMessageSize = c.Int(nullable: false),
                        CurrentStatus = c.String(),
                    })
                .PrimaryKey(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Status");
        }
    }
}
