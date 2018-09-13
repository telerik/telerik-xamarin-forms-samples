using System.Threading.Tasks;

namespace QSF.Services
{
    public interface IMessageService
    {
        Task ShowMessage(string caption, string message);
        Task ShowMessage(string caption, string message, string button);
    }
}