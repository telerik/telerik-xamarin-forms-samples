using System.Threading.Tasks;

namespace ErpApp.Services
{
    public interface IAuthenticationService
    {
        string UserName { get; }
        Task<bool> IsAuthenticated();
        Task<bool> Login(string username, string passsword);
    }
}
