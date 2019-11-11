namespace telerikerpService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderIsOnlineandStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsOnline", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Status");
            DropColumn("dbo.Orders", "IsOnline");
        }
    }
}
