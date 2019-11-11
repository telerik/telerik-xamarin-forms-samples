using Xamarin.Forms;

namespace QSF.Services.Toast
{
    public interface IToastMessageService
    {
        void LongAlert(string message);

        void ShortAlert(string message);
    }
}
