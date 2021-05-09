namespace HumberAutoCarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changecarModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cars", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "Title", c => c.String());
        }
    }
}
