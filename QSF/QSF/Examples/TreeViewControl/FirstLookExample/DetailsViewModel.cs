using System;
using System.Threading.Tasks;
using QSF.Services;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.TreeViewControl.FirstLookExample
{
    public class DetailsViewModel : ViewModelBase
    {
        private FolderViewModel folder;

        public FolderViewModel Folder
        {
            get
            {
                return this.folder;
            }
            private set
            {
                if (this.folder != value)
                {
                    this.folder = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Command BackCommand { get; private set; }

        public DetailsViewModel()
        {
            this.BackCommand = new Command(this.OnBackCommand);
        }

        internal override Task InitializeAsync(object parameter)
        {
            this.Folder = (FolderViewModel)parameter;

            return base.InitializeAsync(parameter);
        }

        private void OnBackCommand()
        {
            var navigationService = DependencyService.Get<INavigationService>();

            navigationService.NavigateBackAsync();
        }
    }
}
