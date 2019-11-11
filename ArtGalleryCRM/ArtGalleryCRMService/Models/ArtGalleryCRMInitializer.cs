using System.Data.Entity;
using ArtGalleryCRMService.DataObjects;
using ArtGalleryCRMService.Helpers;

namespace ArtGalleryCRMService.Models
{
    /// <summary>
    /// Seeds the database with tables for the related entities. 
    /// </summary>
    public class ArtGalleryCRMInitializer : CreateDatabaseIfNotExists<ArtGalleryCRMContext>
    {
        protected override void Seed(ArtGalleryCRMContext context)
        {
            // Generate Employees Customers and Products data
            var employees = SampleDataHelpers.GenerateEmployees();
            var customers = SampleDataHelpers.GenerateCustomers();
            var products = SampleDataHelpers.GenerateProducts();
            var orders = SampleDataHelpers.GenerateOrders(employees, customers, products);

            // Seed the Database tables
            foreach (var employee in employees) context.Set<Employee>().Add(employee);
            foreach (var customer in customers) context.Set<Customer>().Add(customer);
            foreach (var product in products) context.Set<Product>().Add(product);
            foreach (var order in orders) context.Set<Order>().Add(order);

            base.Seed(context);
        }
    }
}