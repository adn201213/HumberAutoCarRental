namespace HumberAutoCarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCarRentAndChangeAvailability : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "Availability", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cars", "Availability", c => c.Boolean(nullable: false));
        }
    }
}
