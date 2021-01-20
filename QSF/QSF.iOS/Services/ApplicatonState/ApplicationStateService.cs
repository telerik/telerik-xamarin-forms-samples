using QSF.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(QSF.iOS.Services.ApplicatonState.ApplicationStateService))]
namespace QSF.iOS.Services.ApplicatonState
{
    public class ApplicationStateService : IApplicationStateService
    {
        public bool IsApplicationActive => UIApplication.SharedApplication.ApplicationState != UIApplicationState.Background;
    }
}
