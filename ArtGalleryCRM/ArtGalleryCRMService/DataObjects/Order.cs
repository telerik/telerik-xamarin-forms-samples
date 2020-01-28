using System;
using Microsoft.Azure.Mobile.Server;

namespace ArtGalleryCRMService.DataObjects
{
    public class Order : EntityData
    {
        public string CustomerId { get; set; }

        public string EmployeeId { get; set; }

        public string ProductId { get; set; }

        public double TotalPrice { get; set; }

        public int Quantity { get; set; }

        public DateTime OrderDate { get; set; }

        public string DeliveryService { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public string Notes { get; set; }
    }
}