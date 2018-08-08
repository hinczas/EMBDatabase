namespace EMBDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Manufacturer",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Full_Name = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Website = c.String(),
                        Notes = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Part",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Part_Number = c.String(),
                        Part_Name = c.String(),
                        Type = c.String(),
                        Description = c.String(),
                        Keywords = c.String(),
                        Voltage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Current = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Notes = c.String(),
                        Quantity = c.Int(nullable: false),
                        Pin_Count = c.Int(nullable: false),
                        Manufacturer_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturer", t => t.Manufacturer_Id)
                .Index(t => t.Manufacturer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Part", "Manufacturer_Id", "dbo.Manufacturer");
            DropIndex("dbo.Part", new[] { "Manufacturer_Id" });
            DropTable("dbo.Part");
            DropTable("dbo.Manufacturer");
        }
    }
}
