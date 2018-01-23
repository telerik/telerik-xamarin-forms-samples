using System.Threading.Tasks;
using QSF.Services;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.ListViewControl.SelectionExample
{
    public class DetailsViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private SelectionViewModel viewModel;

        public ListItem SelectedItem
        {
            get
            {
                return this.viewModel.SelectedItem;
            }
        }

        public Command BackCommand { get; private set; }
        public Command FavoriteCommand { get; private set; }
        public Command DeleteCommand { get; private set; }

        public DetailsViewModel()
        {
            this.navigationService = DependencyService.Get<INavigationService>();
            this.BackCommand = new Command(this.OnBack);
            this.FavoriteCommand = new Command(this.OnFavorite);
            this.DeleteCommand = new Command(this.OnDelete);
        }

        internal override Task InitializeAsync(object parameter)
        {
            this.viewModel = (SelectionViewModel)parameter;

            return base.InitializeAsync(parameter);
        }

        private void OnBack()
        {
            this.navigationService.NavigateBackAsync();
        }

        private void OnFavorite()
        {
            this.viewModel.SelectedItem.IsFavorite = !this.viewModel.SelectedItem.IsFavorite;
        }

        private void OnDelete()
        {
            this.viewModel.Items.Remove(this.viewModel.SelectedItem);
            this.navigationService.NavigateBackAsync();
        }
    }
}
