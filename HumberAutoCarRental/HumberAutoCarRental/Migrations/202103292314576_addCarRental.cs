namespace HumberAutoCarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCarRental : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarRents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        CarId = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        ScheduledEndDate = c.DateTime(),
                        ActualEndDate = c.DateTime(),
                        AdditionalCharge = c.Double(),
                        RentalPrice = c.Double(nullable: false),
                        RentalDuration = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CarRents");
        }
    }
}
