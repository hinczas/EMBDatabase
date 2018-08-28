namespace EMBDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class partNumericsNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Part", "Voltage", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Part", "Current", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Part", "Quantity", c => c.Int());
            AlterColumn("dbo.Part", "Pin_Count", c => c.Int());
            AlterColumn("dbo.Part", "Price", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Part", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Part", "Pin_Count", c => c.Int(nullable: false));
            AlterColumn("dbo.Part", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Part", "Current", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Part", "Voltage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
