namespace EMBDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Part", "Location_Id", "dbo.Location");
            DropForeignKey("dbo.Part", "Manufacturer_Id", "dbo.Manufacturer");
            DropForeignKey("dbo.Part", "Package_Id", "dbo.Package");
            DropForeignKey("dbo.Part", "Type_Id", "dbo.Type");
            DropIndex("dbo.Part", new[] { "Location_Id" });
            DropIndex("dbo.Part", new[] { "Manufacturer_Id" });
            DropIndex("dbo.Part", new[] { "Package_Id" });
            DropIndex("dbo.Part", new[] { "Type_Id" });
            AlterColumn("dbo.Part", "Location_Id", c => c.Long(nullable: true));
            AlterColumn("dbo.Part", "Manufacturer_Id", c => c.Long(nullable: true));
            AlterColumn("dbo.Part", "Package_Id", c => c.Long(nullable: true));
            AlterColumn("dbo.Part", "Type_Id", c => c.Long(nullable: true));
            CreateIndex("dbo.Part", "Location_Id");
            CreateIndex("dbo.Part", "Manufacturer_Id");
            CreateIndex("dbo.Part", "Package_Id");
            CreateIndex("dbo.Part", "Type_Id");
            AddForeignKey("dbo.Part", "Location_Id", "dbo.Location", "Id");
            AddForeignKey("dbo.Part", "Manufacturer_Id", "dbo.Manufacturer", "Id");
            AddForeignKey("dbo.Part", "Package_Id", "dbo.Package", "Id");
            AddForeignKey("dbo.Part", "Type_Id", "dbo.Type", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Part", "Type_Id1", "dbo.Type");
            DropForeignKey("dbo.Part", "Package_Id1", "dbo.Package");
            DropForeignKey("dbo.Part", "Manufacturer_Id1", "dbo.Manufacturer");
            DropForeignKey("dbo.Part", "Location_Id1", "dbo.Location");
            DropIndex("dbo.Part", new[] { "Type_Id1" });
            DropIndex("dbo.Part", new[] { "Package_Id1" });
            DropIndex("dbo.Part", new[] { "Manufacturer_Id1" });
            DropIndex("dbo.Part", new[] { "Location_Id1" });
            AlterColumn("dbo.Part", "Type_Id", c => c.Long());
            AlterColumn("dbo.Part", "Package_Id", c => c.Long());
            AlterColumn("dbo.Part", "Manufacturer_Id", c => c.Long());
            AlterColumn("dbo.Part", "Location_Id", c => c.Long());
            DropColumn("dbo.Part", "Type_Id1");
            DropColumn("dbo.Part", "Package_Id1");
            DropColumn("dbo.Part", "Manufacturer_Id1");
            DropColumn("dbo.Part", "Location_Id1");
            CreateIndex("dbo.Part", "Type_Id");
            CreateIndex("dbo.Part", "Package_Id");
            CreateIndex("dbo.Part", "Manufacturer_Id");
            CreateIndex("dbo.Part", "Location_Id");
            AddForeignKey("dbo.Part", "Type_Id", "dbo.Type", "Id");
            AddForeignKey("dbo.Part", "Package_Id", "dbo.Package", "Id");
            AddForeignKey("dbo.Part", "Manufacturer_Id", "dbo.Manufacturer", "Id");
            AddForeignKey("dbo.Part", "Location_Id", "dbo.Location", "Id");
        }
    }
}
