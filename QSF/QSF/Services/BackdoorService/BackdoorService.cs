using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Services.BackdoorService
{
    public class BackdoorService : IBackdoorService
    {
        public void NavigateToExample(string examplePath)
        {
            var examplePahtParts = examplePath.Split('.');

            INavigationService navigationService = DependencyService.Get<INavigationService>();
            ExampleInfo exampleInfo = new ExampleInfo(examplePahtParts[0], examplePahtParts[1]);
            navigationService.NavigateToExampleAsync(exampleInfo);
        }

        public void NavigateToHome()
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            navigationService.NavigateToAsync<HomeViewModel>();
        }
    }
}
