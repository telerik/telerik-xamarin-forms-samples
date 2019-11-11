using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TodoApp
{
    public partial class App : Application
	{
        public App()
        {
            InitializeComponent();

            FreshMvvm.FreshIOC.Container.Register<Services.ITodoService, Services.TodoService>();

            var mainPage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<PageModels.MainPageModel>();
            var container = new FreshMvvm.FreshNavigationContainer(mainPage);
            MainPage = container;
            var welcomePage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<PageModels.WelcomePageModel>();
            container.PushAsync(welcomePage, false);
        }

        public const string ShowWelcomeScreenPreference = "show_welcome_screen";

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

        public static DataAccess.TodoItemContext CreateDatabase()
        {
            IDbFileProvider dbFileProvider = DependencyService.Get<IDbFileProvider>();
            if (dbFileProvider == null)
                return null;

            // Database
            string dblocation = dbFileProvider.GetLocalFilePath("tododb.db");
            System.Diagnostics.Debug.WriteLine($"Database location: {dblocation}");
            DataAccess.TodoItemContext ctx = DataAccess.TodoItemContext.Create(dblocation);
            return ctx;
        }
    }
}
