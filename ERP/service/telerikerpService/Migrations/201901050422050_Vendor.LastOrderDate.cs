namespace telerikerpService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VendorLastOrderDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendors", "LastOrderDate", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vendors", "LastOrderDate");
        }
    }
}
