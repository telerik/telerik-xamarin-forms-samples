using Microsoft.Azure.Mobile.Server;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using telerikerpService.DataObjects;
using telerikerpService.Models;

namespace telerikerpService.Controllers
{
    public class OrderController : TableController<Order>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            telerikerpContext context = new telerikerpContext();
            DomainManager = new EntityDomainManager<Order>(context, Request);
        }

        // GET tables/Order
        [ExpandProperty("OrderDetails")]
        public IQueryable<Order> GetAllOrders()
        {
            return Query();
        }

        // GET tables/Order/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [ExpandProperty("OrderDetails")]
        [ExpandProperty("Customer")]
        [ExpandProperty("ShippingAddress")]
        public SingleResult<Order> GetOrder(string id)
        {
            return Lookup(id);
        }
    }
}