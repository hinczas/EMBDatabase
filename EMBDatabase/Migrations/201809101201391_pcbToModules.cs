namespace EMBDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pcbToModules : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PCB", "Type_Id", c => c.Long());
            AlterColumn("dbo.PCB", "Total_Cost", c => c.Decimal(precision: 18, scale: 2));
            CreateIndex("dbo.PCB", "Type_Id");
            AddForeignKey("dbo.PCB", "Type_Id", "dbo.Type", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PCB", "Type_Id", "dbo.Type");
            DropIndex("dbo.PCB", new[] { "Type_Id" });
            AlterColumn("dbo.PCB", "Total_Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.PCB", "Type_Id");
        }
    }
}
