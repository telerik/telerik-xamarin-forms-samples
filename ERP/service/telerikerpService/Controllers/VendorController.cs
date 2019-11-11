using Microsoft.Azure.Mobile.Server;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using telerikerpService.DataObjects;
using telerikerpService.Models;

namespace telerikerpService.Controllers
{
    public class VendorController : TableController<Vendor>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            telerikerpContext context = new telerikerpContext();
            DomainManager = new EntityDomainManager<Vendor>(context, Request);
        }

        // GET tables/Vendor
        public IQueryable<Vendor> GetAllVendors()
        {
            return Query();
        }

        // GET tables/Vendor/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Vendor> GetVendor(string id)
        {
            return Lookup(id);
        }
    }
}