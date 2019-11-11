using Microsoft.Azure.Mobile.Server;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using telerikerpService.DataObjects;
using telerikerpService.Models;

namespace telerikerpService.Controllers
{
    public class CustomerController : TableController<Customer>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            telerikerpContext context = new telerikerpContext();
            DomainManager = new EntityDomainManager<Customer>(context, Request);
        }

        // GET tables/Customer
        [ExpandProperty("Addresses")]
        public IQueryable<Customer> GetAllCustomers()
        {
            return Query();
        }

        // GET tables/Customer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [ExpandProperty("Addresses")]
        [ExpandProperty("Orders")]
        public SingleResult<Customer> GetCustomer(string id)
        {
            return Lookup(id);
        }
    }
}