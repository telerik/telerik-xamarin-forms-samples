using System;
using System.Threading.Tasks;
using ErpApp.Services;
using ErpApp.ViewModels;
using MvvmCross.Exceptions;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ErpApp
{
    public class AppStart : MvxAppStart
    {
        public AppStart(IMvxApplication application, IMvxNavigationService navigationService, IAuthenticationService authenticationService, IErpService erpService)
            : base(application, navigationService)
        {
            this.authenticationService = authenticationService;
            this.erpService = erpService;
        }

        private IAuthenticationService authenticationService;
        private IErpService erpService;

        protected override async Task NavigateToFirstViewModel(object hint = null)
        {
            try
            {
                var isAuthenticated = await this.authenticationService.IsAuthenticated();
                if (isAuthenticated)
                {
                    await NavigationService.Navigate<RootViewModel>();
                }
                else
                {
                    await NavigationService.Navigate<LoginPageViewModel>();
                }
            }
            catch (Exception exception)
            {
                throw exception.MvxWrap();
            }
        }
    }
}
