namespace HumberAutoCarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CarTypeChangeFromIntToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarTypes", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarTypes", "Name", c => c.Int(nullable: false));
        }
    }
}
