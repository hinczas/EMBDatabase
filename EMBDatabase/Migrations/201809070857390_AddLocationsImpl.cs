namespace EMBDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocationsImpl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Type", "File_Id", c => c.Long());
            CreateIndex("dbo.Type", "File_Id");
            AddForeignKey("dbo.Type", "File_Id", "dbo.File", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Type", "File_Id", "dbo.File");
            DropIndex("dbo.Type", new[] { "File_Id" });
            DropColumn("dbo.Type", "File_Id");
        }
    }
}
