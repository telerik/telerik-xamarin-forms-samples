namespace telerikerpService.Migrations
{
    using Microsoft.Azure.Mobile.Server.Tables;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<telerikerpService.Models.telerikerpContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new EntityTableSqlGenerator());
            ContextKey = "telerikerpService.Models.telerikerpContext";
        }

        protected override void Seed(telerikerpService.Models.telerikerpContext context)
        {
            //  This method will be called after migrating to the latest version.

            if (!context.Customers.Any())
            {
                context.Customers.AddOrUpdate(SeedData.Customers);
                context.CustomerAddresses.AddOrUpdate(SeedData.CustomerAddresses);
            }

            if (!context.Products.Any())
            {
                context.Products.AddOrUpdate(SeedData.Products);
            }

            if (!context.Vendors.Any())
            {
                context.Vendors.AddOrUpdate(SeedData.Vendors);
            }

            if (!context.Orders.Any())
            {
                context.Orders.AddOrUpdate(SeedData.Orders);
                context.OrderDetails.AddOrUpdate(SeedData.OrderDetails);
            }
        }
    }
}
