using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QSF.ViewModels
{
    public class InfoViewModel : ViewModelBase
    {
        private string header;
        private string hyperlinkText;
        private string logoImage;
        private string content;
        private InfoType type;
        private ICommand linkTapped;

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

        public string HyperlinkText
        {
            get
            {
                return this.hyperlinkText;
            }
            private set
            {
                if (this.hyperlinkText != value)
                {
                    this.hyperlinkText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string LogoImage
        {
            get
            {
                return this.logoImage;
            }
            private set
            {
                if (this.logoImage != value)
                {
                    this.logoImage = value;
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

        public ICommand LinkTapped
        {
            get
            {
                return this.linkTapped;
            }
            private set
            {
                if (this.linkTapped != value)
                {
                    this.linkTapped = value;
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
            this.HyperlinkText = infoSettings.HyperlinkText;
            this.LogoImage = infoSettings.LogoImage;
            this.LinkTapped = new Command(this.OnLinkTapped);

            return Task.FromResult(false);
        }

        private void OnLinkTapped(object obj)
        {
            Device.OpenUri(new Uri(obj.ToString()));
        }
    }
}
