namespace HumberAutoCarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedMembershipTypeToDataBase : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[MembershipTypes]([Name],[SignUpFee],[ChargeRateOneDay],[ChargeRateOneWeek],[ChargeRateOneMonth],[ChargeRateThreeMonth],[ChargeRateSixMonth],[ChargeRateOneYear]) VALUES ('Pay per Rental',0,100,95,90,80,70,60)");
            Sql("INSERT INTO [dbo].[MembershipTypes]([Name],[SignUpFee],[ChargeRateOneDay],[ChargeRateOneWeek],[ChargeRateOneMonth],[ChargeRateThreeMonth],[ChargeRateSixMonth],[ChargeRateOneYear]) VALUES ('Member',300,90,85,80,70,60,50)");
            Sql("INSERT INTO [dbo].[MembershipTypes]([Name],[SignUpFee],[ChargeRateOneDay],[ChargeRateOneWeek],[ChargeRateOneMonth],[ChargeRateThreeMonth],[ChargeRateSixMonth],[ChargeRateOneYear]) VALUES ('SuperAdmin',0,0,0,0,0,0,0)");

        }


        public override void Down()
        {
        }
    }
}
