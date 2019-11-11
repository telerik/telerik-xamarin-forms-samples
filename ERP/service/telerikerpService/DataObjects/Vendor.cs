using Microsoft.Azure.Mobile.Server;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace telerikerpService.DataObjects
{
    public class Vendor : EntityData
    {
        public string VendorNumber { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public byte Rating { get; set; }

        public decimal AnnualRevenue { get; set; }

        public decimal SalesAmmount { get; set; }

        public string Phone { get; set; }

        public string OrderFrequency { get; set; }

        public DateTimeOffset? LastOrderDate { get; set; }
    }
}
