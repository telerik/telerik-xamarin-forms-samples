using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ErpApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Constants.ApplicationURL))
            {
                throw new NotImplementedException("You need to update ErpApp (Portable)/Constants.cs with your Azure Mobile Service URL.");
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
