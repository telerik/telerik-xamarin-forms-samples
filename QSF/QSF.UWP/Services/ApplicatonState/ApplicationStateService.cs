using System;
using QSF.Services;
using QSF.UWP.Services.ApplicationState;

[assembly: Xamarin.Forms.Dependency(typeof(ApplicationStateService))]
namespace QSF.UWP.Services.ApplicationState
{
    public class ApplicationStateService : IApplicationStateService
    {
        public bool IsApplicationActive => throw new NotImplementedException();
    }
}
