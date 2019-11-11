using Microsoft.Azure.Mobile.Server;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace telerikerpService.DataObjects
{
    public class Product : EntityData
    {
        public string ProductNumber { get; set; }

        public string Name { get; set; }

        public DateTimeOffset DateAdded { get; set; }

        public string Image { get; set; }

        public decimal Weight { get; set; }

        public decimal Price { get; set; }

        public string Location { get; set; }

        public int StockLevel { get; set; }

        public int Color { get; set; }
    }
}
