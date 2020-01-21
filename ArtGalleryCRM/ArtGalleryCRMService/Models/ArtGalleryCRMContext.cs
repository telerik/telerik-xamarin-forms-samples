using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server.Tables;
using ArtGalleryCRMService.DataObjects;

namespace ArtGalleryCRMService.Models
{
    public class ArtGalleryCRMContext : DbContext
    {
        private const string connectionStringName = "Name=MS_TableConnectionString";

        public ArtGalleryCRMContext() : base(connectionStringName)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                "ServiceTableColumn", 
                (property, attributes) => attributes.Single().ColumnType.ToString()));
        }
    }
}