using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using ArtGalleryCRMService.DataObjects;
using ArtGalleryCRMService.Models;
using Microsoft.Azure.Mobile.Server;

namespace ArtGalleryCRMService.Controllers
{
    // IMPORTANT: Before deploying to your Azure account, enable insert, update and delete:
    // 1. Uncomment the code in the Patch, Post and Delete methods.
    // 2. Delete the "throw new HttpException(500, "Demo - Read-only mode");" lines.
    public class OrderController : TableController<Order>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            var context = new ArtGalleryCRMContext();

            DomainManager = new EntityDomainManager<Order>(context, Request);
        }

        // GET tables/Order
        public IQueryable<Order> GetAllOrders()
        {
            return Query();
        }

        // GET tables/Order/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Order> GetOrder(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Order/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Order> PatchOrder(string id, Delta<Order> patch)
        {
            // Remove this before deploying to your Azure account.
            throw new HttpException(500, "Demo - Read-only mode");

            // Uncomment before deploying to your Azure account.
            // return UpdateAsync(id, patch);
        }

        // POST tables/Order
        public async Task<IHttpActionResult> PostOrder(Order order)
        {
            // Remove this before deploying to your Azure account.
            throw new HttpException(500, "Demo - Read-only mode");

            // Uncomment before deploying to your Azure account.
            // Order current = await InsertAsync(order);
            // return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Order/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteOrder(string id)
        {
            // Remove this before deploying to your Azure account.
            throw new HttpException(500, "Demo - Read-only mode");

            // Uncomment before deploying to your Azure account.
            // return DeleteAsync(id);
        }
    }
}