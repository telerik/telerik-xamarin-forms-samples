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
    public class CustomerController : TableController<Customer>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            var context = new ArtGalleryCRMContext();

            DomainManager = new EntityDomainManager<Customer>(context, Request);
        }

        // GET tables/Customer
        public IQueryable<Customer> GetAllCustomers()
        {
            return Query();
        }

        // GET tables/Customer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Customer> GetCustomer(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Customer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Customer> PatchCustomer(string id, Delta<Customer> patch)
        {
            // Remove this before deploying to your Azure account.
            throw new HttpException(500, "Demo - Read-only mode");

            // Uncomment before deploying to your Azure account.
            // return UpdateAsync(id, patch);
        }

        // POST tables/Customer
        public async Task<IHttpActionResult> PostCustomer(Customer customer)
        {
            // Remove this before deploying to your Azure account.
            throw new HttpException(500, "Demo - Read-only mode");

            // Uncomment before deploying to your Azure account.
            // Customer current = await InsertAsync(customer);
            // return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Customer/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteCustomer(string id)
        {
            // Remove this before deploying to your Azure account.
            throw new HttpException(500, "Demo - Read-only mode");

            // Uncomment before deploying to your Azure account.
            // return DeleteAsync(id);
        }
    }
}