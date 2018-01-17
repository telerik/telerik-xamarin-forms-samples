using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QSF.ViewModels
{
    public class InfoViewModel : ViewModelBase
    {
        private string header;
        private string content;
        private InfoType type;

        public InfoViewModel()
        {
            this.AppBarLeftButtonCommand = new Command(async () => await this.NavigateBack());
        }

        public ICommand AppBarLeftButtonCommand { get; }

        public string Header
        {
            get
            {
                return this.header;
            }
            private set
            {
                if (this.header != value)
                {
                    this.header = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Content
        {
            get
            {
                return this.content;
            }
            private set
            {
                if (this.content != value)
                {
                    this.content = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public InfoType Type
        {
            get
            {
                return this.type;
            }
            private set
            {
                if (this.type != value)
                {
                    this.type = value;
                    this.OnPropertyChanged();
                }
            }
        }

        internal override Task InitializeAsync(object parameter)
        {
            InfoViewSettings infoSettings = parameter as InfoViewSettings;

            this.Type = infoSettings.Type;
            this.Header = infoSettings.Header;
            this.Content = infoSettings.Content;

            return Task.FromResult(false);
        }
    }
}
