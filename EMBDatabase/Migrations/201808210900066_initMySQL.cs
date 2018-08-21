namespace EMBDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initMySQL : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.File",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        File_Type = c.String(maxLength: 50, storeType: "nvarchar"),
                        Hash_Name = c.String(unicode: false),
                        File_Path = c.String(unicode: false),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        Notes = c.String(unicode: false),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        UpdateDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Part",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Number = c.String(maxLength: 50, storeType: "nvarchar"),
                        Keywords = c.String(unicode: false),
                        Voltage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Current = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Pin_Count = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Manufacturer_Id = c.Long(),
                        Package_Id = c.Long(),
                        Location_Id = c.Long(),
                        Type_Id = c.Long(),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        Notes = c.String(unicode: false),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        UpdateDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Location", t => t.Location_Id)
                .ForeignKey("dbo.Manufacturer", t => t.Manufacturer_Id)
                .ForeignKey("dbo.Package", t => t.Package_Id)
                .ForeignKey("dbo.Type", t => t.Type_Id)
                .Index(t => t.Manufacturer_Id)
                .Index(t => t.Package_Id)
                .Index(t => t.Location_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        Notes = c.String(unicode: false),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        UpdateDate = c.DateTime(precision: 0),
                        File_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.File", t => t.File_Id)
                .Index(t => t.File_Id);
            
            CreateTable(
                "dbo.Manufacturer",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Full_Name = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        Website = c.String(unicode: false),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        Notes = c.String(unicode: false),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        UpdateDate = c.DateTime(precision: 0),
                        File_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.File", t => t.File_Id)
                .Index(t => t.File_Id);
            
            CreateTable(
                "dbo.Package",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Number = c.String(maxLength: 50, storeType: "nvarchar"),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        Notes = c.String(unicode: false),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        UpdateDate = c.DateTime(precision: 0),
                        File_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.File", t => t.File_Id)
                .Index(t => t.File_Id);
            
            CreateTable(
                "dbo.PCB",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Version = c.String(unicode: false),
                        StartDate = c.DateTime(precision: 0),
                        EndDate = c.DateTime(precision: 0),
                        Total_Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Changelog = c.String(unicode: false),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        Notes = c.String(unicode: false),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        UpdateDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(maxLength: 50, storeType: "nvarchar"),
                        Version = c.String(maxLength: 50, storeType: "nvarchar"),
                        StartDate = c.DateTime(precision: 0),
                        EndDate = c.DateTime(precision: 0),
                        Log = c.String(unicode: false),
                        Folder_Location = c.String(unicode: false),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        Notes = c.String(unicode: false),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        UpdateDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Type",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        Notes = c.String(unicode: false),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        UpdateDate = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PartFiles",
                c => new
                    {
                        Part_Id = c.Long(nullable: false),
                        File_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Part_Id, t.File_Id })
                .ForeignKey("dbo.Part", t => t.Part_Id, cascadeDelete: true)
                .ForeignKey("dbo.File", t => t.File_Id, cascadeDelete: true)
                .Index(t => t.Part_Id)
                .Index(t => t.File_Id);
            
            CreateTable(
                "dbo.PCBFiles",
                c => new
                    {
                        PCB_Id = c.Long(nullable: false),
                        File_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.PCB_Id, t.File_Id })
                .ForeignKey("dbo.PCB", t => t.PCB_Id, cascadeDelete: true)
                .ForeignKey("dbo.File", t => t.File_Id, cascadeDelete: true)
                .Index(t => t.PCB_Id)
                .Index(t => t.File_Id);
            
            CreateTable(
                "dbo.PCBParts",
                c => new
                    {
                        PCB_Id = c.Long(nullable: false),
                        Part_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.PCB_Id, t.Part_Id })
                .ForeignKey("dbo.PCB", t => t.PCB_Id, cascadeDelete: true)
                .ForeignKey("dbo.Part", t => t.Part_Id, cascadeDelete: true)
                .Index(t => t.PCB_Id)
                .Index(t => t.Part_Id);
            
            CreateTable(
                "dbo.ProjectFiles",
                c => new
                    {
                        Project_Id = c.Long(nullable: false),
                        File_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Project_Id, t.File_Id })
                .ForeignKey("dbo.Project", t => t.Project_Id, cascadeDelete: true)
                .ForeignKey("dbo.File", t => t.File_Id, cascadeDelete: true)
                .Index(t => t.Project_Id)
                .Index(t => t.File_Id);
            
            CreateTable(
                "dbo.ProjectParts",
                c => new
                    {
                        Project_Id = c.Long(nullable: false),
                        Part_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Project_Id, t.Part_Id })
                .ForeignKey("dbo.Project", t => t.Project_Id, cascadeDelete: true)
                .ForeignKey("dbo.Part", t => t.Part_Id, cascadeDelete: true)
                .Index(t => t.Project_Id)
                .Index(t => t.Part_Id);
            
            CreateTable(
                "dbo.ProjectPCBs",
                c => new
                    {
                        Project_Id = c.Long(nullable: false),
                        PCB_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Project_Id, t.PCB_Id })
                .ForeignKey("dbo.Project", t => t.Project_Id, cascadeDelete: true)
                .ForeignKey("dbo.PCB", t => t.PCB_Id, cascadeDelete: true)
                .Index(t => t.Project_Id)
                .Index(t => t.PCB_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Part", "Type_Id", "dbo.Type");
            DropForeignKey("dbo.ProjectPCBs", "PCB_Id", "dbo.PCB");
            DropForeignKey("dbo.ProjectPCBs", "Project_Id", "dbo.Project");
            DropForeignKey("dbo.ProjectParts", "Part_Id", "dbo.Part");
            DropForeignKey("dbo.ProjectParts", "Project_Id", "dbo.Project");
            DropForeignKey("dbo.ProjectFiles", "File_Id", "dbo.File");
            DropForeignKey("dbo.ProjectFiles", "Project_Id", "dbo.Project");
            DropForeignKey("dbo.PCBParts", "Part_Id", "dbo.Part");
            DropForeignKey("dbo.PCBParts", "PCB_Id", "dbo.PCB");
            DropForeignKey("dbo.PCBFiles", "File_Id", "dbo.File");
            DropForeignKey("dbo.PCBFiles", "PCB_Id", "dbo.PCB");
            DropForeignKey("dbo.Part", "Package_Id", "dbo.Package");
            DropForeignKey("dbo.Package", "File_Id", "dbo.File");
            DropForeignKey("dbo.Part", "Manufacturer_Id", "dbo.Manufacturer");
            DropForeignKey("dbo.Manufacturer", "File_Id", "dbo.File");
            DropForeignKey("dbo.Part", "Location_Id", "dbo.Location");
            DropForeignKey("dbo.Location", "File_Id", "dbo.File");
            DropForeignKey("dbo.PartFiles", "File_Id", "dbo.File");
            DropForeignKey("dbo.PartFiles", "Part_Id", "dbo.Part");
            DropIndex("dbo.ProjectPCBs", new[] { "PCB_Id" });
            DropIndex("dbo.ProjectPCBs", new[] { "Project_Id" });
            DropIndex("dbo.ProjectParts", new[] { "Part_Id" });
            DropIndex("dbo.ProjectParts", new[] { "Project_Id" });
            DropIndex("dbo.ProjectFiles", new[] { "File_Id" });
            DropIndex("dbo.ProjectFiles", new[] { "Project_Id" });
            DropIndex("dbo.PCBParts", new[] { "Part_Id" });
            DropIndex("dbo.PCBParts", new[] { "PCB_Id" });
            DropIndex("dbo.PCBFiles", new[] { "File_Id" });
            DropIndex("dbo.PCBFiles", new[] { "PCB_Id" });
            DropIndex("dbo.PartFiles", new[] { "File_Id" });
            DropIndex("dbo.PartFiles", new[] { "Part_Id" });
            DropIndex("dbo.Package", new[] { "File_Id" });
            DropIndex("dbo.Manufacturer", new[] { "File_Id" });
            DropIndex("dbo.Location", new[] { "File_Id" });
            DropIndex("dbo.Part", new[] { "Type_Id" });
            DropIndex("dbo.Part", new[] { "Location_Id" });
            DropIndex("dbo.Part", new[] { "Package_Id" });
            DropIndex("dbo.Part", new[] { "Manufacturer_Id" });
            DropTable("dbo.ProjectPCBs");
            DropTable("dbo.ProjectParts");
            DropTable("dbo.ProjectFiles");
            DropTable("dbo.PCBParts");
            DropTable("dbo.PCBFiles");
            DropTable("dbo.PartFiles");
            DropTable("dbo.Type");
            DropTable("dbo.Project");
            DropTable("dbo.PCB");
            DropTable("dbo.Package");
            DropTable("dbo.Manufacturer");
            DropTable("dbo.Location");
            DropTable("dbo.Part");
            DropTable("dbo.File");
        }
    }
}
