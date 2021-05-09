namespace HumberAutoCarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMemeberShipsTypesToFixNullError : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.MembershipTypes", "ApplicationUser_Id");
            AddForeignKey("dbo.MembershipTypes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MembershipTypes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.MembershipTypes", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.MembershipTypes", "ApplicationUser_Id");
        }
    }
}
