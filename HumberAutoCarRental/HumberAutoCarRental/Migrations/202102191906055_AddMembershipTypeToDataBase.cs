namespace HumberAutoCarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembershipTypeToDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MembershipTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SignUpFee = c.Short(nullable: false),
                        ChargeRateOneDay = c.Byte(nullable: false),
                        ChargeRateOneWeek = c.Byte(nullable: false),
                        ChargeRateOneMonth = c.Byte(nullable: false),
                        ChargeRateThreeMonth = c.Byte(nullable: false),
                        ChargeRateSixMonth = c.Byte(nullable: false),
                        ChargeRateOneYear = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MembershipTypes");
        }
    }
}
