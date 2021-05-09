namespace HumberAutoCarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldsToAspNetUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String());
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Disable", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "MembershipTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "RentalCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RentalCount");
            DropColumn("dbo.AspNetUsers", "MembershipTypeId");
            DropColumn("dbo.AspNetUsers", "Disable");
            DropColumn("dbo.AspNetUsers", "BirthDate");
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
