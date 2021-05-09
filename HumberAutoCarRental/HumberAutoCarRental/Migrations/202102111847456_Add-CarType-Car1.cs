namespace HumberAutoCarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarTypeCar1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PlateID = c.String(),
                        CarName = c.String(),
                        Maker = c.String(),
                        Description = c.String(),
                        ImageUrl = c.String(),
                        Availability = c.Boolean(nullable: false),
                        Price = c.Double(nullable: false),
                        DateAdded = c.DateTime(),
                        MadeYear = c.DateTime(),
                        CarTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CarTypes", t => t.CarTypeId, cascadeDelete: true)
                .Index(t => t.CarTypeId);
            
            CreateTable(
                "dbo.CarTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "CarTypeId", "dbo.CarTypes");
            DropIndex("dbo.Cars", new[] { "CarTypeId" });
            DropTable("dbo.CarTypes");
            DropTable("dbo.Cars");
        }
    }
}
