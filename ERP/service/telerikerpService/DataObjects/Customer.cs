using Microsoft.Azure.Mobile.Server;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace telerikerpService.DataObjects
{
    public class Customer : EntityData
    {
        public string CustomerNumber { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Image { get; set; }

        public decimal DefaultDiscount { get; set; }

        public decimal CustomerSatisfaction { get; set; }

        public string PreferredCommunicationChannel { get; set; }

        public virtual ICollection<CustomerAddress> Addresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
