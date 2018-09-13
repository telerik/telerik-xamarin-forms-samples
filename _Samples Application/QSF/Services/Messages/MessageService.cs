using System.Threading.Tasks;
using Xamarin.Forms;

namespace QSF.Services
{
    public class MessageService : IMessageService
    {
        public Task ShowMessage(string caption, string message)
        {
            return this.ShowMessage(caption, message, "OK");
        }

        public Task ShowMessage(string caption, string message, string button)
        {
            return Application.Current.MainPage.DisplayAlert(caption, message, button);
        }
    }
}
