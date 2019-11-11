using System;
using System.Collections.Generic;
using ArtGalleryCRMService.DataObjects;

namespace ArtGalleryCRMService.Helpers
{
    // This class is to provide initial sample data to seed the database. It can also be used to clean and reset the database on a schedule
    public static class SampleDataHelpers
    {
        public static IList<Employee> GenerateEmployees()
        {
           return new List<Employee>
            {
                new Employee
                {
                    Id = "91bfb25a-9fba-456b-a1e7-2d1bb958d2e4",
                    Name = "Jane Doe",
                    HireDate = DateTime.Today,
                    OfficeLocation = "Waltham, USA",
                    PhotoUri = "https://picsum.photos/300?image=64",
                    Salary = 103000,
                    VacationBalance = 120,
                    VacationUsed = 38,
                    Notes = "Sales Associate",
                },
                new Employee
                {
                    Id = "12504f18-c9d8-4b33-a359-fd974241cf7e",
                    Name = "Patrick Serra",
                    HireDate = DateTime.Today,
                    OfficeLocation = "Waltham, USA",
                    PhotoUri = "https://picsum.photos/300?image=375",
                    Salary = 83000,
                    VacationBalance = 120,
                    VacationUsed = 75,
                    Notes = "Junior Sales Associate",
                },
                new Employee
                {
                    Id = "81bba592-0f55-4466-b886-35371d572763",
                    Name = "Irina Peterson",
                    HireDate = DateTime.Today,
                    OfficeLocation = "Bedford, USA",
                    PhotoUri = "https://picsum.photos/300?image=373",
                    Salary = 35000,
                    VacationBalance = 60,
                    VacationUsed = 25,
                    Notes = "Senior Sales Associate",
                },
                new Employee
                {
                    Id = "a404482d-d8db-446d-8bc7-d370236caf52",
                    Name = "Alex Konov",
                    HireDate = DateTime.Today,
                    OfficeLocation = "Sofia, Bulgaria",
                    PhotoUri = "https://picsum.photos/300?image=338",
                    Salary = 62500,
                    VacationBalance = 120,
                    VacationUsed = 48,
                    Notes = "SVP Sales",
                },
                new Employee
                {
                    Id = "254735c3-354c-414c-b949-cf347f6930f8", 
                    Name = "Janet Downing",
                    HireDate = DateTime.Today,
                    OfficeLocation = "Sofia, Bulgaria",
                    PhotoUri = "https://picsum.photos/300?image=91",
                    Salary = 56000,
                    VacationBalance = 90,
                    VacationUsed = 32,
                    Notes = "CFO",
                },
            };
        }

        public static IList<Customer> GenerateCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    Id ="5eb347fc-6656-4b30-8d3c-5df79b40d91f",
                    Name = "Amy Adamson",
                    Street = "101 Tremont St",
                    City = "Boston",
                    State = "MA",
                    Country = "USA",
                    ZipCode = "02118",
                    Notes = "Long time customer, handle with care."
                },
                new Customer
                {
                    Id = "0b7ac771-55d9-4a63-ba7c-89dada288c1d",
                    Name = "Jeremy Smith",
                    Street = "2 Main St",
                    City = "Boston",
                    State = "MA",
                    Country = "USA",
                    ZipCode = "02118",
                    Notes = "Art purchaser for the Boston Symphony Orchestra."
                },
                new Customer
                {
                    Id = "9ff6a462-0a75-417a-a25a-8de5179c460d",
                    Name = "Joe McCarthy",
                    Street = "50 Broadway",
                    City = "San Francisco",
                    State = "CA",
                    Country = "USA",
                    ZipCode = "94102",
                    Notes = "Customer in California, prefers abstract art."
                },
                new Customer
                {
                    Id = "e68b82f8-b74e-47d4-afc5-69654d398b07",
                    Name = "Derek Masterson",
                    Street = "2005 Polk SRd",
                    City = "Boston",
                    State = "MA",
                    Country = "USA",
                    ZipCode = "02118",
                    Notes = "Derek buys a new piece every month."
                },
            };
        }

        public static IList<Product> GenerateProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = "a0129b91-621c-4f97-a7ae-049f00f6e4f7",
                    Title = "Subway",
                    PhotoUri = "https://picsum.photos/640/360?image=396",
                    InventoryCount = 75,
                    IsDiscontinued = false,
                    Price = 9.99,
                    Description = "Geometric look at the work commute."
                },
                new Product
                {
                    Id = "ddea0496-3240-4934-9f99-91cfbc97700c",
                    Title = "Mountaintop",
                    PhotoUri = "https://picsum.photos/640/360?image=377",
                    InventoryCount = 75,
                    IsDiscontinued = false,
                    Price = 175.45,
                    Description = "Soar above the valley and reach your goals!"
                },
                new Product
                {
                    Id = "d96d9357-52ff-493f-a9ad-3ac5f92d4639",
                    Title = "Oceanside",
                    PhotoUri = "https://picsum.photos/640/360?image=374",
                    InventoryCount = 5,
                    IsDiscontinued = false,
                    Price = 25.99,
                    Description = "Peaceful intentions and relaxing existence."
                },
                new Product
                {
                    Id = "0ea227f4-e68c-4931-99a7-f67b7be5b6d1",
                    Title = "Lush",
                    PhotoUri = "https://picsum.photos/640/360?image=434",
                    InventoryCount = 25,
                    IsDiscontinued = false,
                    Price = 19.95,
                    Description = "Green life, at its best."
                },
                new Product
                {
                    Id = "3d80fb0c-5a73-464d-8056-5823c55df9fe",
                    Title = "Cityscape",
                    PhotoUri = "https://picsum.photos/640/360?image=420",
                    InventoryCount = 4,
                    IsDiscontinued = false,
                    Price = 124.99,
                    Description = "Urban architecture in all its glory."
                },
                new Product
                {
                    Id = "22fdf426-ecdf-4145-8829-d46efc35fabc",
                    Title = "Beauty",
                    PhotoUri = "https://picsum.photos/640/360?image=444",
                    InventoryCount = 2,
                    IsDiscontinued = false,
                    Price = 159999.95,
                    Description = "Words cannot describe that feeling when beauty, in its raw form, reaches the eyes."
                },
                new Product
                {
                    Id = "659abd75-0011-4d11-b896-8f619f14a7d5",
                    Title = "Lost",
                    PhotoUri = "https://picsum.photos/640/360?image=444",
                    InventoryCount = 21,
                    IsDiscontinued = false,
                    Price = 245,
                    Description = "The journey is its own reward."
                }
            };
        }

        public static IList<Order> GenerateOrders(IList<Employee> employees, IList<Customer> customers, IList<Product> products)
        {
            var orders = new List<Order>();

            var deliveryServices = new[] { "UPS", "FedEx", "USPS", "DHL", "OnTrac", "YRC Freight", "Bolt Express", "Transit Systems" };

            var rand = new Random();

            var loopNumber = 0;
            
            foreach (var customer in customers)
            {
                for (int i = 0; i < 3; i++)
                {
                    // This helps generate a known GUID
                    loopNumber = loopNumber + 1;

                    // pick a random product
                    var product = products[rand.Next(0, products.Count - 1)];

                    // pick a random employee
                    var employee = employees[rand.Next(0, employees.Count - 1)];

                    // pick a delivery service being used to ship the order
                    var deliveryService = deliveryServices[rand.Next(deliveryServices.Length - 1)];
                    
                    // create an order using the iterated customer with the randomized product and employee
                    var order = new Order
                    {
                        Id = $"b650aa26-2f3b-43f9-b641-d16f3736{loopNumber:0000}",
                        CustomerId = customer.Id,
                        EmployeeId = employee.Id,
                        ProductId = product.Id,
                        Quantity = rand.Next(1, 10),
                        OrderDate = DateTime.Today.AddDays(i).AddHours(6),
                        DeliveryService = deliveryService,
                        Street = "100 Main Street",
                        City = "Boston",
                        State = "MA",
                        ZipCode = "02118",
                        Country = "USA",
                        Notes = "This order was automatically generated in a sample data helper."
                    };

                    order.TotalPrice = order.Quantity * product.Price;

                    orders.Add(order);
                }
            }

            return orders;
        }
    }
}