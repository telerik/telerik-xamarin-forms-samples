using System.Threading.Tasks;
using System.Windows.Input;
using tagit.Common;
using Xamarin.Forms;

namespace tagit.Models
{
    public class GettingStartedInformation : ObservableBase
    {
        public GettingStartedInformation()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                GetStartedCommand = new RelayCommand(async () => { await App.NavigationService.PopAsync(); });
            }
            else
            { 
                GetStartedCommand = new RelayCommand(async () => { await App.NavigationService.PopModalAsync(); }); 
            }
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
    }
}