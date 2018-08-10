namespace EMBDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moreKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Part", "Location_Id", "dbo.Location");
            DropForeignKey("dbo.Part", "Manufacturer_Id", "dbo.Manufacturer");
            DropForeignKey("dbo.Part", "Package_Id", "dbo.Package");
            DropForeignKey("dbo.Part", "Type_Id", "dbo.Type");
            DropIndex("dbo.Part", new[] { "Manufacturer_Id" });
            DropIndex("dbo.Part", new[] { "Package_Id" });
            DropIndex("dbo.Part", new[] { "Location_Id" });
            DropIndex("dbo.Part", new[] { "Type_Id" });
            AlterColumn("dbo.Part", "Manufacturer_Id", c => c.Long());
            AlterColumn("dbo.Part", "Package_Id", c => c.Long());
            AlterColumn("dbo.Part", "Location_Id", c => c.Long());
            AlterColumn("dbo.Part", "Type_Id", c => c.Long());
            CreateIndex("dbo.Part", "Manufacturer_Id");
            CreateIndex("dbo.Part", "Package_Id");
            CreateIndex("dbo.Part", "Location_Id");
            CreateIndex("dbo.Part", "Type_Id");
            AddForeignKey("dbo.Part", "Location_Id", "dbo.Location", "Id");
            AddForeignKey("dbo.Part", "Manufacturer_Id", "dbo.Manufacturer", "Id");
            AddForeignKey("dbo.Part", "Package_Id", "dbo.Package", "Id");
            AddForeignKey("dbo.Part", "Type_Id", "dbo.Type", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Part", "Type_Id", "dbo.Type");
            DropForeignKey("dbo.Part", "Package_Id", "dbo.Package");
            DropForeignKey("dbo.Part", "Manufacturer_Id", "dbo.Manufacturer");
            DropForeignKey("dbo.Part", "Location_Id", "dbo.Location");
            DropIndex("dbo.Part", new[] { "Type_Id" });
            DropIndex("dbo.Part", new[] { "Location_Id" });
            DropIndex("dbo.Part", new[] { "Package_Id" });
            DropIndex("dbo.Part", new[] { "Manufacturer_Id" });
            AlterColumn("dbo.Part", "Type_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.Part", "Location_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.Part", "Package_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.Part", "Manufacturer_Id", c => c.Long(nullable: false));
            CreateIndex("dbo.Part", "Type_Id");
            CreateIndex("dbo.Part", "Location_Id");
            CreateIndex("dbo.Part", "Package_Id");
            CreateIndex("dbo.Part", "Manufacturer_Id");
            AddForeignKey("dbo.Part", "Type_Id", "dbo.Type", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Part", "Package_Id", "dbo.Package", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Part", "Manufacturer_Id", "dbo.Manufacturer", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Part", "Location_Id", "dbo.Location", "Id", cascadeDelete: true);
        }
    }
}
