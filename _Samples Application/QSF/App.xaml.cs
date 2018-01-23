using QSF.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using QSF.Services.Serialization;

namespace QSF
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            this.InitDependencies();

            var configurationService = DependencyService.Get<IConfigurationService>();
            configurationService.InitializeAsync();

            this.InitNavigation();
        }

        private void InitDependencies()
        {
            DependencyService.Register<IConfigurationService, XmlConfigurationService>();
            DependencyService.Register<IResourceService, AssemblyResourceService>();
            DependencyService.Register<INavigationService, NavigationService>();
            DependencyService.Register<IControlsService, ControlsService>();
            DependencyService.Register<IThemesService, ThemesService>();
            DependencyService.Register<ISearchService, SearchService>();
            DependencyService.Register<ISerializationService, SerializationService>();
        }

        private Task InitNavigation()
        {
            var navigationService = DependencyService.Get<INavigationService>();
            return navigationService.InitializeAsync();
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
