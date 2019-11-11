using Microsoft.Azure.Mobile.Server;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using telerikerpService.DataObjects;
using telerikerpService.Models;

namespace telerikerpService.Controllers
{
    public class ProductController : TableController<Product>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            telerikerpContext context = new telerikerpContext();
            DomainManager = new EntityDomainManager<Product>(context, Request);
        }

        // GET tables/Product
        public IQueryable<Product> GetAllProducts()
        {
            return Query();
        }

        // GET tables/Product/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Product> GetProduct(string id)
        {
            return Lookup(id);
        }
    }
}