namespace EMBDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueIndexes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.File", "File_Type", c => c.String(maxLength: 32, storeType: "nvarchar"));
            AlterColumn("dbo.File", "File_Path", c => c.String(maxLength: 512, storeType: "nvarchar"));
            AlterColumn("dbo.File", "Name", c => c.String(maxLength: 64, storeType: "nvarchar"));
            AlterColumn("dbo.Part", "Number", c => c.String(maxLength: 64, storeType: "nvarchar"));
            AlterColumn("dbo.Part", "Name", c => c.String(maxLength: 64, storeType: "nvarchar"));
            AlterColumn("dbo.Location", "Name", c => c.String(maxLength: 64, storeType: "nvarchar"));
            AlterColumn("dbo.Manufacturer", "Full_Name", c => c.String(maxLength: 512, storeType: "nvarchar"));
            AlterColumn("dbo.Manufacturer", "Name", c => c.String(maxLength: 64, storeType: "nvarchar"));
            AlterColumn("dbo.Package", "Number", c => c.String(maxLength: 64, storeType: "nvarchar"));
            AlterColumn("dbo.Package", "Name", c => c.String(maxLength: 64, storeType: "nvarchar"));
            AlterColumn("dbo.PCB", "Version", c => c.String(maxLength: 16, storeType: "nvarchar"));
            AlterColumn("dbo.PCB", "Name", c => c.String(maxLength: 64, storeType: "nvarchar"));
            AlterColumn("dbo.Project", "Name", c => c.String(maxLength: 64, storeType: "nvarchar"));
            AlterColumn("dbo.Type", "Name", c => c.String(maxLength: 64, storeType: "nvarchar"));
            CreateIndex("dbo.File", new[] { "Name", "File_Path" }, unique: true, name: "UI_Name");
            CreateIndex("dbo.Part", new[] { "Name", "Number" }, unique: true, name: "UI_Name");
            CreateIndex("dbo.Location", "Name", unique: true, name: "UI_Name");
            CreateIndex("dbo.Manufacturer", new[] { "Name", "Full_Name" }, unique: true, name: "UI_Name");
            CreateIndex("dbo.Package", new[] { "Name", "Number" }, unique: true, name: "UI_Name");
            CreateIndex("dbo.PCB", new[] { "Name", "Version" }, unique: true, name: "UI_Name");
            CreateIndex("dbo.Project", new[] { "Name", "Code", "Version" }, unique: true, name: "UI_Name");
            CreateIndex("dbo.Type", "Name", unique: true, name: "UI_Name");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Type", "UI_Name");
            DropIndex("dbo.Project", "UI_Name");
            DropIndex("dbo.PCB", "UI_Name");
            DropIndex("dbo.Package", "UI_Name");
            DropIndex("dbo.Manufacturer", "UI_Name");
            DropIndex("dbo.Location", "UI_Name");
            DropIndex("dbo.Part", "UI_Name");
            DropIndex("dbo.File", "UI_Name");
            AlterColumn("dbo.Type", "Name", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.Project", "Name", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.PCB", "Name", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.PCB", "Version", c => c.String(unicode: false));
            AlterColumn("dbo.Package", "Name", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.Package", "Number", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.Manufacturer", "Name", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.Manufacturer", "Full_Name", c => c.String(unicode: false));
            AlterColumn("dbo.Location", "Name", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.Part", "Name", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.Part", "Number", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.File", "Name", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.File", "File_Path", c => c.String(unicode: false));
            AlterColumn("dbo.File", "File_Type", c => c.String(maxLength: 50, storeType: "nvarchar"));
        }
    }
}
