using System;
using telerikerpService.DataObjects;

namespace telerikerpService
{
    public static class SeedData
    {
        static SeedData()
        {
            Customers = new Customer[]
            {
                new Customer()
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerNumber = "C123456",
                    Name = "Alvin Shaw",
                    Email = "alvins@progress.com",
                    Phone = "1 (11) 123 456 7890",
                    CustomerSatisfaction = 0.66M,
                    DefaultDiscount = 0.06M,
                    PreferredCommunicationChannel = "email",
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Person_2.png"
                },
                new Customer()
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerNumber = "C123457",
                    Name = "Lynn James",
                    Email = "LynnJ@telerik.com",
                    Phone = "1 (11) 123 456 7890",
                    CustomerSatisfaction = 0.53M,
                    DefaultDiscount = 0.04M,
                    PreferredCommunicationChannel = "email",
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Person_3.png"
                },
                new Customer()
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerNumber = "C123458",
                    Name = "Raylee Cooke",
                    Email = "RayleeC@telerik.com",
                    Phone = "1 (11) 123 456 7890",
                    CustomerSatisfaction = 0.72M,
                    DefaultDiscount = 0.02M,
                    PreferredCommunicationChannel = "email",
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Person_8.png"
                },
                new Customer()
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerNumber = "C123459",
                    Name = "Karsen Mathews",
                    Email = "KarsenMt@progress.com",
                    Phone = "1 (11) 123 456 7890",
                    CustomerSatisfaction = 0.43M,
                    DefaultDiscount = 0.08M,
                    PreferredCommunicationChannel = "email",
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Person_1.png"
                },
                new Customer()
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerNumber = "C123460",
                    Name = "Jody Nolan",
                    Email = "JodyN@progress.com",
                    Phone = "1 (11) 123 456 7890",
                    CustomerSatisfaction = 0.66M,
                    DefaultDiscount = 0.05M,
                    PreferredCommunicationChannel = "email",
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Person_4.png"
                },
                new Customer()
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerNumber = "C123461",
                    Name = "Liam Khan",
                    Email = "LiamK@telerik.com",
                    Phone = "1 (11) 123 456 7890",
                    CustomerSatisfaction = 0.85M,
                    DefaultDiscount = 0.02M,
                    PreferredCommunicationChannel = "email",
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Person_6.png"
                },
                new Customer()
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerNumber = "C123462",
                    Name = "Devan Roy",
                    Email = "DevanR@progress.com",
                    Phone = "1 (11) 123 456 7890",
                    CustomerSatisfaction = 0.26M,
                    DefaultDiscount = 0.03M,
                    PreferredCommunicationChannel = "email",
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Person_5.png"
                }
            };

            CustomerAddresses = new CustomerAddress[]
            {
                // 0
                new CustomerAddress()
                {
                    Id = Guid.NewGuid().ToString(),
                    Primary = true,
                    CustomerID = Customers[0].Id,
                    Address = "52 New Tothnam Rd",
                    City = "NY",
                    State = "NY",
                    Country = "US",
                    POCode = "10001"
                },
                // 1
                new CustomerAddress()
                {
                    Id = Guid.NewGuid().ToString(),
                    Primary = true,
                    CustomerID = Customers[1].Id,
                    Address = "52 New Tothnam Rd",
                    City = "NY",
                    State = "NY",
                    POCode = "10001"
                },
                // 2
                new CustomerAddress()
                {
                    Id = Guid.NewGuid().ToString(),
                    Primary = true,
                    CustomerID = Customers[2].Id,
                    Address = "52 New Tothnam Rd",
                    City = "NY",
                    State = "NY",
                    Country = "US",
                    POCode = "10001"
                },
                // 3
                new CustomerAddress()
                {
                    Id = Guid.NewGuid().ToString(),
                    Primary = true,
                    CustomerID = Customers[3].Id,
                    Address = "52 New Tothnam Rd",
                    City = "NY",
                    State = "NY",
                    Country = "US",
                    POCode = "10001"
                },
                // 4
                new CustomerAddress()
                {
                    Id = Guid.NewGuid().ToString(),
                    Primary = true,
                    CustomerID = Customers[4].Id,
                    Address = "52 New Tothnam Rd",
                    City = "NY",
                    State = "NY",
                    Country = "US",
                    POCode = "10001"
                },
                // 5
                new CustomerAddress()
                {
                    Id = Guid.NewGuid().ToString(),
                    Primary = true,
                    CustomerID = Customers[5].Id,
                    Address = "52 New Tothnam Rd",
                    City = "NY",
                    State = "NY",
                    Country = "US",
                    POCode = "10001"
                },
                // 6
                new CustomerAddress()
                {
                    Id = Guid.NewGuid().ToString(),
                    Primary = true,
                    CustomerID = Customers[6].Id,
                    Address = "52 New Tothnam Rd",
                    City = "NY",
                    State = "NY",
                    Country = "US",
                    POCode = "10001"
                },
                // 7
                new CustomerAddress()
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerID = Customers[0].Id,
                    Address = "9111 Rose Ann Ave",
                    City = "St. Leonards",
                    State = "New South Wales",
                    Country = "Australia",
                    POCode = "17001"
                },
                // 8
                new CustomerAddress()
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerID = Customers[1].Id,
                    Address = "9111 Rose Ann Ave",
                    City = "St. Leonards",
                    State = "New South Wales",
                    Country = "Australia",
                    POCode = "17001"
                },
                // 9
                new CustomerAddress()
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerID = Customers[2].Id,
                    Address = "9111 Rose Ann Ave",
                    City = "St. Leonards",
                    State = "New South Wales",
                    Country = "Australia",
                    POCode = "17001"
                },
                // 10
                new CustomerAddress()
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerID = Customers[3].Id,
                    Address = "9111 Rose Ann Ave",
                    City = "St. Leonards",
                    State = "New South Wales",
                    Country = "Australia",
                    POCode = "17001"
                },
                // 11
                new CustomerAddress()
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerID = Customers[4].Id,
                    Address = "9111 Rose Ann Ave",
                    City = "St. Leonards",
                    State = "New South Wales",
                    Country = "Australia",
                    POCode = "17001"
                },
                // 12
                new CustomerAddress()
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerID = Customers[5].Id,
                    Address = "9111 Rose Ann Ave",
                    City = "St. Leonards",
                    State = "New South Wales",
                    Country = "Australia",
                    POCode = "17001"
                },
                // 13
                new CustomerAddress()
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerID = Customers[6].Id,
                    Address = "9111 Rose Ann Ave",
                    City = "St. Leonards",
                    State = "New South Wales",
                    Country = "Australia",
                    POCode = "17001"
                }
            };

            Products = new Product[]
            {
                new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductNumber = "P1234",
                    DateAdded = new DateTime(2017, 05, 12),
                    Name = "Grips",
                    Weight = 20,
                    Price = 45,
                    Color = System.Drawing.Color.FromArgb(0xFF, 0x8C, 0xAE).ToArgb(),
                    Location = "Tool Crib",
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Product_6.jpg",
                    StockLevel = 45
                },
                new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductNumber = "P1235",
                    DateAdded = new DateTime(2017, 06, 17),
                    Name = "Flat Washer",
                    Weight = 0.6M,
                    Price = 21.00M,
                    Color = System.Drawing.Color.FromArgb(0xFF, 0xAE, 0x8C).ToArgb(),
                    Location = "Tool Crib",
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Product_4.jpg",
                    StockLevel = 345
                }, new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductNumber = "P1236",
                    DateAdded = new DateTime(2017, 05, 14),
                    Name = "Fork End",
                    Weight = 12,
                    Price = 32,
                    Color = System.Drawing.Color.FromArgb(0x8C, 0xAE, 0xFF).ToArgb(),
                    Location = "Tool Crib",
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Product_3.jpg",
                    StockLevel = 45
                },
                new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductNumber = "P1237",
                    DateAdded = new DateTime(2017, 06, 17),
                    Name = "Pinch Bolt",
                    Weight = 17,
                    Price = 75,
                    Color = System.Drawing.Color.FromArgb(0xFF, 0x8F, 0xAC).ToArgb(),
                    Location = "Tool Crib",
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Product_7.jpg",
                    StockLevel = 45
                },
                new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductNumber = "P1238",
                    DateAdded = new DateTime(2017, 08, 12),
                    Name = "Headset",
                    Weight = 37,
                    Price = 275,
                    Color = System.Drawing.Color.FromArgb(0xFF, 0x7A, 0x1E).ToArgb(),
                    Location = "Tool Crib",
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Product_2.jpg",
                    StockLevel = 6
                },
                new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductNumber = "P1239",
                    DateAdded = new DateTime(2017, 05, 07),
                    Name = "Front Brakes",
                    Weight = 67,
                    Price = 175,
                    Color = System.Drawing.Color.FromArgb(0xFF, 0xFF, 0xAE).ToArgb(),
                    Location = "Tool Crib",
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Product_5.jpg",
                    StockLevel = 12
                },
                new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductNumber = "P1240",
                    DateAdded = new DateTime(2017, 11, 17),
                    Name = "Hub",
                    Weight = 17,
                    Price = 75,
                    Color = System.Drawing.Color.FromArgb(0x1A, 0x8C, 0xAE).ToArgb(),
                    Location = "Tool Crib",
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Product_8.jpg",
                    StockLevel = 12
                },
                new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductNumber = "P1241",
                    DateAdded = new DateTime(2017, 06, 12),
                    Name = "Crankset",
                    Weight = 17,
                    Price = 35,
                    Color = System.Drawing.Color.FromArgb(0xFF, 0x8C, 0xFF).ToArgb(),
                    Location = "Tool Crib",
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Product_1.jpg",
                    StockLevel = 5
                }
            };

            Vendors = new Vendor[]
            {
                new Vendor()
                {
                    Id = Guid.NewGuid().ToString(),
                    VendorNumber = "V123456",
                    Name = "NextBump",
                    Rating = 3,
                    AnnualRevenue = 123000,
                    SalesAmmount = 230000,
                    OrderFrequency = "S",
                    Phone = "1 (11) 123 456",
                    LastOrderDate = new DateTime(2017, 06, 11),
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Vendor_5.png"
                },
                new Vendor()
                {
                    Id = Guid.NewGuid().ToString(),
                    VendorNumber = "V123457",
                    Name = "MoTech",
                    Rating = 3,
                    AnnualRevenue = 453000,
                    SalesAmmount = 501000,
                    OrderFrequency = "S",
                    Phone = "1 (11) 123 456",
                    LastOrderDate = new DateTime(2018, 07, 12),
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Vendor_4.png"
                },
                new Vendor()
                {
                    Id = Guid.NewGuid().ToString(),
                    VendorNumber = "V123458",
                    Name = "YoDev",
                    Rating = 3,
                    AnnualRevenue = 12453000,
                    SalesAmmount = 196000,
                    OrderFrequency = "S",
                    Phone = "1 (11) 123 456",
                    LastOrderDate = new DateTime(2012, 12, 03),
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Vendor_3.png"
                },
                new Vendor()
                {
                    Id = Guid.NewGuid().ToString(),
                    VendorNumber = "V123459",
                    Name = "Burnkey",
                    Rating = 3,
                    AnnualRevenue = 353000,
                    OrderFrequency = "S",
                    Phone = "1 (11) 123 456",
                    LastOrderDate = new DateTime(2018, 06, 04),
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Vendor_2.png"
                },
                new Vendor()
                {
                    Id = Guid.NewGuid().ToString(),
                    VendorNumber = "V123460",
                    Name = "Portabox",
                    Rating = 3,
                    AnnualRevenue = 153000,
                    SalesAmmount = 162000,
                    OrderFrequency = "S",
                    Phone = "1 (11) 123 456",
                    LastOrderDate = new DateTime(2018, 07, 15),
                    Image = "https://telerikerpappstorage.blob.core.windows.net/erpapppics/Vendor_1.png"
                }
            };

            Orders = new Order[]
            {
                new Order()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderNumber = "O123456",
                    CustomerID = Customers[2].Id,
                    OrderDate = new DateTime(2018, 07, 12),
                    DueDate = new DateTime(2018, 08, 12),
                    IsOnline = true,
                    Status = "Delivered",
                    ShipMethod = "ZY Express",
                    ShippingAddressID = CustomerAddresses[2].Id,
                    ShippingAddressModifiedDate = new DateTime(2017, 06, 02)
                },
                new Order()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderNumber = "O123457",
                    CustomerID = Customers[2].Id,
                    OrderDate = new DateTime(2018, 08, 13),
                    DueDate = new DateTime(2018, 08, 20),
                    IsOnline = true,
                    Status = "In Progress",
                    ShipMethod = "ZY Express",
                    ShippingAddressID = CustomerAddresses[2].Id,
                    ShippingAddressModifiedDate = new DateTime(2017, 06, 02)
                },
                new Order()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderNumber = "O123458",
                    CustomerID = Customers[1].Id,
                    OrderDate = new DateTime(2018, 12, 11),
                    DueDate = new DateTime(2018, 12, 26),
                    IsOnline = true,
                    Status = "Completed",
                    ShipMethod = "ZY Express",
                    ShippingAddressID = CustomerAddresses[8].Id,
                    ShippingAddressModifiedDate = new DateTime(2017, 06, 02)
                },
                new Order()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderNumber = "O123459",
                    CustomerID = Customers[0].Id,
                    OrderDate = new DateTime(2018, 12, 12),
                    DueDate = new DateTime(2018, 12, 27),
                    IsOnline = true,
                    Status = "In Progress",
                    ShipMethod = "ZY Express",
                    ShippingAddressID = CustomerAddresses[7].Id,
                    ShippingAddressModifiedDate = new DateTime(2017, 06, 02)
                },
                new Order()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderNumber = "O123460",
                    CustomerID = Customers[1].Id,
                    OrderDate = new DateTime(2018, 12, 12),
                    DueDate = new DateTime(2018, 12, 27),
                    IsOnline = true,
                    Status = "In Progress",
                    ShipMethod = "ZY Express",
                    ShippingAddressID = CustomerAddresses[8].Id,
                    ShippingAddressModifiedDate = new DateTime(2017, 06, 02)
                },
                new Order()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderNumber = "O123461",
                    CustomerID = Customers[3].Id,
                    OrderDate = new DateTime(2018, 12, 11),
                    DueDate = new DateTime(2018, 12, 26),
                    IsOnline = true,
                    Status = "Completed",
                    ShipMethod = "ZY Express",
                    ShippingAddressID = CustomerAddresses[10].Id,
                    ShippingAddressModifiedDate = new DateTime(2017, 06, 02)
                },
                new Order()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderNumber = "O123462",
                    CustomerID = Customers[4].Id,
                    OrderDate = new DateTime(2018, 12, 11),
                    DueDate = new DateTime(2018, 12, 26),
                    IsOnline = true,
                    Status = "Completed",
                    ShipMethod = "ZY Express",
                    ShippingAddressID = CustomerAddresses[4].Id,
                    ShippingAddressModifiedDate = new DateTime(2017, 06, 02)
                },
                new Order()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderNumber = "O123463",
                    CustomerID = Customers[5].Id,
                    OrderDate = new DateTime(2018, 12, 11),
                    DueDate = new DateTime(2018, 12, 26),
                    IsOnline = true,
                    Status = "Completed",
                    ShipMethod = "ZY Express",
                    ShippingAddressID = CustomerAddresses[5].Id,
                    ShippingAddressModifiedDate = new DateTime(2017, 06, 02)
                },
                new Order()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderNumber = "O123464",
                    CustomerID = Customers[6].Id,
                    OrderDate = new DateTime(2018, 12, 11),
                    DueDate = new DateTime(2018, 12, 26),
                    IsOnline = true,
                    Status = "Completed",
                    ShipMethod = "ZY Express",
                    ShippingAddressID = CustomerAddresses[6].Id,
                    ShippingAddressModifiedDate = new DateTime(2017, 06, 02)
                }
            };

            OrderDetails = new OrderDetail[]
            {
                new OrderDetail()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderID = Orders[0].Id,
                    ProductID = Products[4].Id,
                    ModifiedDate = new DateTime(2018, 07, 12),
                    ProductPrice = 275,
                    Count = 2
                },
                new OrderDetail()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderID = Orders[1].Id,
                    ProductID = Products[3].Id,
                    ModifiedDate = new DateTime(2018, 07, 12),
                    ProductPrice = 36,
                    Count = 1
                },
                new OrderDetail()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderID = Orders[2].Id,
                    ProductID = Products[0].Id,
                    ModifiedDate = new DateTime(2018, 12, 11),
                    ProductPrice = 36,
                    Count = 1
                },
                new OrderDetail()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderID = Orders[3].Id,
                    ProductID = Products[0].Id,
                    ModifiedDate = new DateTime(2018, 12, 12),
                    ProductPrice = 36,
                    Count = 1
                },
                new OrderDetail()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderID = Orders[4].Id,
                    ProductID = Products[0].Id,
                    ModifiedDate = new DateTime(2018, 12, 12),
                    ProductPrice = 36,
                    Count = 1
                },
                new OrderDetail()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderID = Orders[5].Id,
                    ProductID = Products[3].Id,
                    ModifiedDate = new DateTime(2018, 12, 11),
                    ProductPrice = 36,
                    Count = 1
                },
                new OrderDetail()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderID = Orders[6].Id,
                    ProductID = Products[4].Id,
                    ModifiedDate = new DateTime(2018, 12, 11),
                    ProductPrice = 36,
                    Count = 1
                },
                new OrderDetail()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderID = Orders[7].Id,
                    ProductID = Products[5].Id,
                    ModifiedDate = new DateTime(2018, 12, 11),
                    ProductPrice = 36,
                    Count = 1
                },
                new OrderDetail()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderID = Orders[8].Id,
                    ProductID = Products[6].Id,
                    ModifiedDate = new DateTime(2018, 12, 11),
                    ProductPrice = 36,
                    Count = 1
                }
            };
        }

        public static Customer[] Customers { get; }

        public static CustomerAddress[] CustomerAddresses { get; }

        public static Product[] Products { get; }

        public static Vendor[] Vendors { get; }

        public static Order[] Orders { get; }

        public static OrderDetail[] OrderDetails { get; }
    }
}
