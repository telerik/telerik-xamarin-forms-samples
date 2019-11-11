using System.Threading.Tasks;
using System.Windows.Input;
using tagit.Common;

namespace tagit.Models
{
    public class GettingStartedInformation : ObservableBase
    {
        public GettingStartedInformation()
        {
            GetStartedCommand = new RelayCommand(async () => { await GetStartedAsync(); });
        }

        private string _image;

        private bool _isFinalItem;

        private string _subtitle;

        private string _title;
        
        public ICommand GetStartedCommand { get; }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Subtitle
        {
            get => _subtitle;
            set => SetProperty(ref _subtitle, value);
        }

        public string Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public bool IsFinalItem
        {
            get => _isFinalItem;
            set => SetProperty(ref _isFinalItem, value);
        }

        private async Task GetStartedAsync()
        {
            await App.NavigationService.PopModalAsync();
        }
    }
}