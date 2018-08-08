namespace EMBDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BigOne : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.File",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        File_Type = c.String(maxLength: 50),
                        Hash_Name = c.String(),
                        Name = c.String(maxLength: 50),
                        Description = c.String(),
                        Notes = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(),
                        Notes = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
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
                        Number = c.String(maxLength: 50),
                        Name = c.String(maxLength: 50),
                        Description = c.String(),
                        Notes = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
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
                        Version = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Total_Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Changelog = c.String(),
                        Name = c.String(maxLength: 50),
                        Description = c.String(),
                        Notes = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(maxLength: 50),
                        Version = c.String(maxLength: 50),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Log = c.String(),
                        Folder_Location = c.String(),
                        Name = c.String(maxLength: 50),
                        Description = c.String(),
                        Notes = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Type",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(),
                        Notes = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
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
            
            AddColumn("dbo.Manufacturer", "File_Id", c => c.Long());
            AddColumn("dbo.Part", "Number", c => c.String(maxLength: 50));
            AddColumn("dbo.Part", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Part", "Name", c => c.String(maxLength: 50));
            AddColumn("dbo.Part", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Part", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.Part", "Location_Id", c => c.Long());
            AddColumn("dbo.Part", "Package_Id", c => c.Long());
            AddColumn("dbo.Part", "Type_Id", c => c.Long());
            AlterColumn("dbo.Manufacturer", "Name", c => c.String(maxLength: 50));
            CreateIndex("dbo.Manufacturer", "File_Id");
            CreateIndex("dbo.Part", "Location_Id");
            CreateIndex("dbo.Part", "Package_Id");
            CreateIndex("dbo.Part", "Type_Id");
            AddForeignKey("dbo.Part", "Location_Id", "dbo.Location", "Id");
            AddForeignKey("dbo.Part", "Package_Id", "dbo.Package", "Id");
            AddForeignKey("dbo.Part", "Type_Id", "dbo.Type", "Id");
            AddForeignKey("dbo.Manufacturer", "File_Id", "dbo.File", "Id");
            DropColumn("dbo.Part", "Part_Number");
            DropColumn("dbo.Part", "Part_Name");
            DropColumn("dbo.Part", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Part", "Type", c => c.String());
            AddColumn("dbo.Part", "Part_Name", c => c.String());
            AddColumn("dbo.Part", "Part_Number", c => c.String());
            DropForeignKey("dbo.Manufacturer", "File_Id", "dbo.File");
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
            DropIndex("dbo.Location", new[] { "File_Id" });
            DropIndex("dbo.Part", new[] { "Type_Id" });
            DropIndex("dbo.Part", new[] { "Package_Id" });
            DropIndex("dbo.Part", new[] { "Location_Id" });
            DropIndex("dbo.Manufacturer", new[] { "File_Id" });
            AlterColumn("dbo.Manufacturer", "Name", c => c.String());
            DropColumn("dbo.Part", "Type_Id");
            DropColumn("dbo.Part", "Package_Id");
            DropColumn("dbo.Part", "Location_Id");
            DropColumn("dbo.Part", "UpdateDate");
            DropColumn("dbo.Part", "CreateDate");
            DropColumn("dbo.Part", "Name");
            DropColumn("dbo.Part", "Price");
            DropColumn("dbo.Part", "Number");
            DropColumn("dbo.Manufacturer", "File_Id");
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
            DropTable("dbo.Location");
            DropTable("dbo.File");
        }
    }
}
