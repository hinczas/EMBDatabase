namespace EMBDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modulesIntr : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "PCB", newName: "Module");
            //RenameTable(name: "PCBParts", newName: "PartModules");
            //RenameTable(name: "ProjectPCBs", newName: "ProjectModules");
            //RenameTable(name: "PCBFiles", newName: "ModuleFiles");
            //RenameColumn(table: "dbo.PartModules", name: "PCB_Id", newName: "Module_Id");
            //RenameColumn(table: "dbo.ProjectModules", name: "PCB_Id", newName: "Module_Id");
            //RenameColumn(table: "dbo.ModuleFiles", name: "PCB_Id", newName: "Module_Id");
            //RenameIndex(table: "dbo.ModuleFiles", name: "IX_PCB_Id", newName: "IX_Module_Id");
            //RenameIndex(table: "dbo.PartModules", name: "IX_PCB_Id", newName: "IX_Module_Id");
            //RenameIndex(table: "dbo.ProjectModules", name: "IX_PCB_Id", newName: "IX_Module_Id");
            DropPrimaryKey("PartModules");
            AddPrimaryKey("PartModules", new[] { "Part_Id", "Module_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PartModules");
            AddPrimaryKey("dbo.PartModules", new[] { "PCB_Id", "Part_Id" });
            RenameIndex(table: "dbo.ProjectModules", name: "IX_Module_Id", newName: "IX_PCB_Id");
            RenameIndex(table: "dbo.PartModules", name: "IX_Module_Id", newName: "IX_PCB_Id");
            RenameIndex(table: "dbo.ModuleFiles", name: "IX_Module_Id", newName: "IX_PCB_Id");
            RenameColumn(table: "dbo.ModuleFiles", name: "Module_Id", newName: "PCB_Id");
            RenameColumn(table: "dbo.ProjectModules", name: "Module_Id", newName: "PCB_Id");
            RenameColumn(table: "dbo.PartModules", name: "Module_Id", newName: "PCB_Id");
            RenameTable(name: "dbo.ModuleFiles", newName: "PCBFiles");
            RenameTable(name: "dbo.ProjectModules", newName: "ProjectPCBs");
            RenameTable(name: "dbo.PartModules", newName: "PCBParts");
            RenameTable(name: "dbo.Module", newName: "PCB");
        }
    }
}
