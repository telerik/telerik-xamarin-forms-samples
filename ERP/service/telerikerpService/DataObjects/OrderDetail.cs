using Microsoft.Azure.Mobile.Server;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace telerikerpService.DataObjects
{
    public class OrderDetail : EntityData
    {
        [ForeignKey("Order")]
        public string OrderID { get; set; }

        [ForeignKey("Product")]
        public string ProductID { get; set; }

        public DateTime ModifiedDate { get; set; }

        public decimal ProductPrice { get; set; }

        public int Count { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
