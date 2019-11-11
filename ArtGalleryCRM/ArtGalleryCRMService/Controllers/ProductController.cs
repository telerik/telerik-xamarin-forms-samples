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
    public class ProductController : TableController<Product>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            var context = new ArtGalleryCRMContext();
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

        // PATCH tables/Product/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Product> PatchProduct(string id, Delta<Product> patch)
        {
            // Remove this before deploying to your Azure account.
            throw new HttpException(500, "Demo - Read-only mode");

            // Uncomment before deploying to your Azure account.
            // return UpdateAsync(id, patch);
        }

        // POST tables/Product
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            // Remove this before deploying to your Azure account.
            throw new HttpException(500, "Demo - Read-only mode");

            // Uncomment before deploying to your Azure account.
            // Product current = await InsertAsync(product);
            // return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Product/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteProduct(string id)
        {
            // Remove this before deploying to your Azure account.
            throw new HttpException(500, "Demo - Read-only mode");

            // Uncomment before deploying to your Azure account.
            // return DeleteAsync(id);
        }
    }
}