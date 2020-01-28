using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;

namespace ArtGalleryCRM.UwpTasks
{
    public sealed class UpdateTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var toastContent = new ToastContent
            {
                Visual = new ToastVisual
                {
                    BindingGeneric = new ToastBindingGeneric
                    {
                        Children =
                        {
                            new AdaptiveText
                            {
                                Text = "Art Gallery CRM Updated!"
                            },
                            new AdaptiveText
                            {
                                Text = "Check out the updated UI for Xamarin CRM demo."
                            },
                            new AdaptiveImage
                            {
                                Source = "https://progressdevsupport.blob.core.windows.net/xamarin-crm/notification-images/uwp-update-notification-image.png"
                            }
                        }
                    }
                }
            };

            var toast = new ToastNotification(toastContent.GetXml());

            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
