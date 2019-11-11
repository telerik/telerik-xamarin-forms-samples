using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ArtGalleryCRM.Forms.Common;
using ArtGalleryCRM.Forms.Models;
using CommonHelpers.Common;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {
        public WelcomeViewModel()
        {
            this.Title = "Welcome!";
            this.GetStartedCommand = new Command(async () => { await this.GetStartedAsync(); });
        }

        public ObservableCollection<WelcomeCard> WelcomeCards { get; } = new ObservableCollection<WelcomeCard>
        {
            new WelcomeCard
            {
                Title = "Art Gallery CRM",
                Subtitle = "This quick intro will familiarize you with the main features of the app, slide to the next item to continue.",
                IconSource = ImageSource.FromFile("Welcome1.png")
            },
            new WelcomeCard
            {
                Title = "Employees",
                Subtitle = "You can explore and manage employees. Employee details page contains charted metrics, such as Sales and Orders statistics.",
                IconSource = ImageSource.FromFile("Welcome2.png")
            },
            new WelcomeCard
            {
                Title = "Customers",
                Subtitle = "The Customers page allows you to locate and drill down into customer details and see any related orders.",
                IconSource = ImageSource.FromFile("Welcome3.png")
            },
            new WelcomeCard
            {
                Title = "Products",
                Subtitle = "Quickly locate and edit your company's products, including details like stock quantity, price, and more.",
                IconSource = ImageSource.FromFile("Welcome4.png")
            },
            new WelcomeCard
            {
                Title = "Orders",
                Subtitle = "Exploring orders is fast and easy using a DataGrid interface. Order details page displays the related Product, Employee and Customer entities.",
                IconSource = ImageSource.FromFile("Welcome5.png")
            },
            new WelcomeCard
            {
                IsFinalItem = true,
                Title = "Get Started",
                Subtitle = "Click the button below to get started on the Employees page. Open the side menu to explore the other features.",
                IconSource = ImageSource.FromFile("Welcome6.png")
            }
        };

        public Command GetStartedCommand { get; }

        private async Task GetStartedAsync()
        {
            Settings.IsFirstRun = false;

            if (App.RootPage != null)
            {
                // We use regular navigation, instead of PageViewmodel methods, because this is a special modal on top of the entire MasterDetailPage
                await App.RootPage.Navigation.PopModalAsync();
            }
        }
    }
}