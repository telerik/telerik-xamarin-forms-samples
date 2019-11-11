using ErpApp.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ErpApp.ViewModels
{
    public class LoginPageViewModel : MvxViewModel
    {
        public LoginPageViewModel(IMvxNavigationService navigationService, IAuthenticationService authenticationService)
        {
            this.navigationService = navigationService;
            this.authService = authenticationService;
            this.LoginCommand = new MvxCommand(this.Login);
            this.username = "admin";
            this.password = "admin";
        }

        private IMvxNavigationService navigationService;
        private IAuthenticationService authService;
        private string username, password;

        public IMvxCommand LoginCommand { get; }

        public string Username
        {
            get => this.username;
            set => SetProperty(ref this.username, value);
        }
        
        public string Password
        {
            get => this.password;
            set => SetProperty(ref this.password, value);
        }

        private async void Login()
        {
            bool success = await authService.Login(this.username, this.password);
            if (success)
            {
                await this.navigationService.Navigate<RootViewModel>();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Login failed", "Invalid username or password", "Ok");
            }
        }
    }
}
