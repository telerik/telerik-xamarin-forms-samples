using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;

namespace ErpApp.Pages
{
    [MvxContentPagePresentation(NoHistory = true, WrapInNavigationPage = false)]
    public partial class LoginPage : MvxContentPage<ViewModels.LoginPageViewModel>
    {
		public LoginPage()
		{
			InitializeComponent();
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();

        //    await Task.Delay(100);
        //    usernameBox.Focus();
        //}
    }
}