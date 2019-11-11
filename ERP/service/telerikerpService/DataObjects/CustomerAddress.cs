using Microsoft.Azure.Mobile.Server;
using System.ComponentModel.DataAnnotations.Schema;

namespace telerikerpService.DataObjects
{
    public class CustomerAddress : EntityData
    {
        [ForeignKey("Customer")]
        public string CustomerID { get; set; }

        public bool Primary { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string POCode { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
