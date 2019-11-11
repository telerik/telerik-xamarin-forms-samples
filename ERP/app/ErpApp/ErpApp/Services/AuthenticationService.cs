using System;
using System.Threading.Tasks;

namespace ErpApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public string UserName { get; private set; }
        public Task<bool> IsAuthenticated()
        {
            // TODO: Cache authentication 
            return Task.FromResult(false);
        }

        public Task<bool> Login(string username, string password)
        {
            this.UserName = username;
            // allows login with random username and password combination
            return Task.FromResult(true);
        }
    }
}