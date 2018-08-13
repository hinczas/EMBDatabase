namespace EMBDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.File", "File_Path", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.File", "File_Path");
        }
    }
}
