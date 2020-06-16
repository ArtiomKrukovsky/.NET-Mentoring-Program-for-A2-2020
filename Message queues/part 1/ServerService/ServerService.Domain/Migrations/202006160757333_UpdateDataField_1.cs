namespace ServerService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataField_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "Data", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "Data");
        }
    }
}
