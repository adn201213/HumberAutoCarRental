namespace HumberAutoCarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixErrorWithUserRegistration1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "RentalCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "RentalCount", c => c.Int(nullable: false));
        }
    }
}
