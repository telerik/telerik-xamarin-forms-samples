using System;
using System.Diagnostics;
using System.Linq;
using ArtGalleryCRMService.Models;

namespace ArtGalleryCRMService.Helpers
{
    // TODO Remove this for customer-facing versions, it is only used by us to keep the Hangfire database maintenance job to keep the database current.
    // This is to enable Hangfire for background server-based jobs to help maintain the database. It is not related to, or required by, Azure Mobile Service
    public class SampleDatabaseHelpers
    {
        private readonly ArtGalleryCRMContext context;

        public SampleDatabaseHelpers()
        {
            this.context = new ArtGalleryCRMContext();
        }

        public void HeartbeatTestJob()
        {
            try
            {
                // Get CFO's record
                var employee = this.context.Employees.FirstOrDefault(e=>e.Id == "254735c3-354c-414c-b949-cf347f6930f8");

                if (employee != null)
                {
                    employee.Notes = $"CFO (record updated: {DateTime.Now:g})";
                }

                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                var message = ex.Message;

                if (ex.InnerException != null)
                {
                    message += $" - InnerEx: {ex.InnerException.Message}";
                }

                Trace.TraceError(message);
            }
        }
        
        public void ResetDatabaseJob()
        {
            try
            {
                Trace.TraceInformation("ResetDatabaseJob Started"); 
                
                // generate sample data (this will always be the same items and have the same IDs)
                var employees = SampleDataHelpers.GenerateEmployees();
                var customers = SampleDataHelpers.GenerateCustomers();
                var products = SampleDataHelpers.GenerateProducts();
                var orders = SampleDataHelpers.GenerateOrders(employees, customers, products);
                
                foreach (var employee in employees)
                {
                    var matchingEmployee = this.context.Employees.FirstOrDefault(e => e.Id == employee.Id);

                    if (matchingEmployee == null)
                    {
                        this.context.Employees.Add(employee);
                    }
                }

                foreach (var customer in customers)
                {
                    var matchingCustomer = this.context.Customers.FirstOrDefault(e => e.Id == customer.Id);

                    if (matchingCustomer == null)
                    {
                        context.Customers.Add(customer);
                    }
                }

                foreach (var product in products)
                {
                    var matchingProduct = this.context.Products.FirstOrDefault(e => e.Id == product.Id);

                    if (matchingProduct == null)
                    {
                        this.context.Products.Add(product);
                    }
                }

                foreach (var order in orders)
                {
                    var matchingOrder = this.context.Orders.FirstOrDefault(e => e.Id == order.Id);

                    if (matchingOrder == null)
                    {
                        this.context.Orders.Add(order);
                    }
                }

                // commit changes 
                this.context.SaveChangesAsync();
                
                Trace.TraceInformation("ResetDatabaseJob Ended"); 
            }
            catch (Exception ex)
            {
                var message = ex.Message;

                if (ex.InnerException != null)
                {
                    message += $" - InnerEx: {ex.InnerException.Message}";
                }

                Trace.TraceError(message);
            }
        }

        /// <summary>
        /// Background job to move each order's OrderDate up one day so that the data in the demo app is always in view on the ShippingCalendar
        /// </summary>
        public void UpdateOrderDatesJob()
        {
            try
            {
                Trace.TraceInformation("UpdateOrderDatesJob Started");

                // Push every order up one day, 
                foreach (var order in context.Orders)
                {
                    if (order != null)
                    {
                        order.OrderDate = order.OrderDate.AddDays(1);
                    }
                }

                context.SaveChanges();

                Trace.TraceInformation("UpdateOrderDatesJob Ended");
            }
            catch (Exception ex)
            {
                var message = ex.Message;

                if (ex.InnerException != null)
                {
                    message += $" - InnerEx: {ex.InnerException.Message}";
                }

                Trace.TraceError(message);
            }
        }
    }
}