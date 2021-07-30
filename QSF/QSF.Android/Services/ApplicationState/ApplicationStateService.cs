using System;
using QSF.Droid.Services.ApplicationState;
using QSF.Services;

[assembly: Xamarin.Forms.Dependency(typeof(ApplicationStateService))]
namespace QSF.Droid.Services.ApplicationState
{
    public class ApplicationStateService : IApplicationStateService
    {
        public bool IsApplicationActive => throw new NotImplementedException();
    }
}
