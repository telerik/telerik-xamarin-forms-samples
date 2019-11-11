using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace telerikerpService.DataObjects
{
    public class Order : EntityData
    {
        public string OrderNumber { get; set; }

        [ForeignKey("Customer")]
        public string CustomerID { get; set; }

        [ForeignKey("ShippingAddress")]
        public string ShippingAddressID { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public DateTimeOffset DueDate { get; set; }

        public string ShipMethod { get; set; }

        public bool IsOnline { get; set; }

        public string Status { get; set; }

        public DateTimeOffset? ShippingAddressModifiedDate { get; set; }

        public virtual CustomerAddress ShippingAddress { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
