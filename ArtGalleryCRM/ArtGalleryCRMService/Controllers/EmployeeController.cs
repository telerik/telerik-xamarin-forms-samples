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
    public class EmployeeController : TableController<Employee>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            var context = new ArtGalleryCRMContext();

            DomainManager = new EntityDomainManager<Employee>(context, Request);
        }

        // GET tables/Employee
        public IQueryable<Employee> GetAllEmployees()
        {
            return Query();
        }

        // GET tables/Employee/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Employee> GetEmployee(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Employee/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Employee> PatchEmployee(string id, Delta<Employee> patch)
        {
            // Remove this before deploying to your Azure account.
            throw new HttpException(500, "Demo - Read-only mode");

            // Uncomment before deploying to your Azure account.
            // return UpdateAsync(id, patch);
        }

        // POST tables/Employee
        public async Task<IHttpActionResult> PostEmployee(Employee employee)
        {
            // Remove this before deploying to your Azure account.
            throw new HttpException(500, "Demo - Read-only mode");

            // Uncomment before deploying to your Azure account.
            // Employee current = await InsertAsync(employee);
            // return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Employee/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteEmployee(string id)
        {
            throw new HttpException(500, "Demo - Read-only mode");

            // Uncomment before deploying to your Azure account.
            // return DeleteAsync(id);
        }
    }
}
