using Foundation;
using QSF.iOS.Services.Toast;
using QSF.Services.Toast;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(ToastMessageService))]
namespace QSF.iOS.Services.Toast
{
    public class ToastMessageService : IToastMessageService
    {
        private const string DefaultBackgroundColor = "#1d1d1e";

        private const double LONG_DELAY = 3.5;
        private const double SHORT_DELAY = 2.0;

        private NSTimer alertDelay;
        private UIAlertController alert;

        public void LongAlert(string message)
        {
            this.ShowAlert(message, LONG_DELAY);
        }

        public void ShortAlert(string message)
        {
            this.ShowAlert(message, SHORT_DELAY);
        }

        private void ShowAlert(string message, double seconds)
        {
            this.alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                this.DismissMessage();
            });

            this.alert = UIAlertController.Create(null, null, UIAlertControllerStyle.ActionSheet);

            var messageAttributes = new UIStringAttributes
            {
                ForegroundColor = UIColor.White,
            };

            var subView = this.alert.View.Subviews.First().Subviews.First().Subviews.First();
            subView.BackgroundColor = Color.FromHex(DefaultBackgroundColor).ToUIColor();

            alert.SetValueForKey(new NSAttributedString(message, messageAttributes), new NSString("attributedMessage"));
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(this.alert, true, null);
        }

        private void DismissMessage()
        {
            if (this.alert != null)
            {
                this.alert.DismissViewController(true, null);
            }
            if (this.alertDelay != null)
            {
                this.alertDelay.Dispose();
            }
        }
    }
}