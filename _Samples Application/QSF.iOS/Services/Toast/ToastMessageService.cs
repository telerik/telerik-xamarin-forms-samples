using CoreGraphics;
using Foundation;
using QSF.iOS.Services.Toast;
using QSF.Services.Toast;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(ToastMessageService))]
namespace QSF.iOS.Services.Toast
{
    public class ToastMessageService : IToastMessageService
    {
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

            this.alert = UIAlertController.Create(null, message, UIAlertControllerStyle.ActionSheet);

            var rootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var popoverPresentationController = this.alert.PopoverPresentationController;

            if (popoverPresentationController != null)
            {
                var rootView = rootViewController.View;
                var rootRect = rootViewController.View.Bounds;
                var sourceRect = new CGRect(0, rootRect.Height, rootRect.Width, 0);

                popoverPresentationController.SourceView = rootView;
                popoverPresentationController.SourceRect = sourceRect;
                popoverPresentationController.PermittedArrowDirections = 0;
            }

            rootViewController.PresentViewController(this.alert, true, null);
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