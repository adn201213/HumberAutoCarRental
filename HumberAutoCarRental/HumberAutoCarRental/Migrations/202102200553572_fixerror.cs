namespace HumberAutoCarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixerror : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "Title");
        }
    }
}
