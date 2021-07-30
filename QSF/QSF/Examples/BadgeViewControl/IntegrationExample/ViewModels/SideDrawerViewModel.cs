using QSF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace QSF.Examples.BadgeViewControl.IntegrationExample
{
    public class SideDrawerViewModel : ExampleViewModel
    {
        private bool isDrawerOpen;

        public SideDrawerViewModel()
        {
            this.IsDrawerOpen = false;
            this.OpenDrawerCommand = new Command(this.OnOpenDrawerExecute);

            this.Inbox = new ObservableCollection<Message>();
            this.Inbox.Add(new Message { Sender = "Weekly Xamarin", Title = "Weekly Xamarin - Issue 299", Content = "Let’s face it, most coding standards are arbitrary. The key to a successful project, however, is not which standards… you follow, but that you are consistent. Jesse Liberty has his list of C# Standards which he has updated.", TimeSended = "16:50" });
            this.Inbox.Add(new Message { Sender = "Stefan Stefanov", Title = "Tree View Customization Example", Content = "The Telerik UI for Xamarin TreeView control can be used to display data in a hierarchical structure with the ability to… collapse or expand data nodes.", TimeSended = "16:43", IsUnread = true });
            this.Inbox.Add(new Message { Sender = "workday-donotreply progress", Title = "Security Alert: Signon from New Device", Content = "Your Workday Account was just signed in to from a new Mac OS X device. You’re getting this email to… make sure it was you.", TimeSended = "15:24", IsUnread = true });
            this.Inbox.Add(new Message { Sender = "Ivan Todorov", Title = "MAUI Sample App", Content = "To build and run the app, you need the latest preview version of Visual Studio 2019 and the required SDKs and… workloads.", TimeSended = "14:30" });
            this.Inbox.Add(new Message { Sender = "Misho Hristov", Title = "UI/UX Modernization – Why Should I?", Content = "To build and run the app, you need the latest preview version of Visual Studio 2019 and the required SDKs and… workloads.", TimeSended = "13:23" });
        }

        public ObservableCollection<Message> Inbox { get; set; }

        public bool IsDrawerOpen
        {
            get
            {
                return this.isDrawerOpen;
            }
            set
            {
                if (this.isDrawerOpen != value)
                {
                    this.isDrawerOpen = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand OpenDrawerCommand { get; set; }

        private void OnOpenDrawerExecute()
        {
            this.IsDrawerOpen = !this.IsDrawerOpen;
        }
    }
}
