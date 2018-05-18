using QSF.ViewModels;
using System;
using System.Threading.Tasks;
using Telerik.XamarinForms.Barcode;
using Xamarin.Forms.Internals;

namespace QSF.Examples.BarcodeControl.QRCodeExample
{
    public class QRCodeViewModel : ExampleViewModel
    {
        private const string ConfigurationMessage = "To configure the QR Code please use the icon in the navigation bar";

        private QRCodeConfigurationViewModel configurationViewModel;
        private string value;
        private int version;
        private CodeMode codeMode;
        private ErrorCorrectionLevel eCL;
        private ECIMode eCIMode;
        private FNC1Mode fNC1Mode;
        private string applicationIndicator;
        private bool isValid;
        private string errorMessage;
        private bool showErrorMessage;
        private string messageText;

        public QRCodeViewModel()
        {
            this.configurationViewModel = new QRCodeConfigurationViewModel();
            this.ECL = ErrorCorrectionLevel.H;
            this.MessageText = ConfigurationMessage;
        }

        public override bool HasConfiguration
        {
            get
            {
                return true;
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int Version
        {
            get
            {
                return this.version;
            }
            private set
            {
                if (this.version != value)
                {
                    this.version = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public CodeMode CodeMode
        {
            get
            {
                return this.codeMode;
            }
            private set
            {
                if (this.codeMode != value)
                {
                    this.codeMode = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ErrorCorrectionLevel ECL
        {
            get
            {
                return this.eCL;
            }
            set
            {
                if (this.eCL != value)
                {
                    this.eCL = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ECIMode ECIMode
        {
            get
            {
                return this.eCIMode;
            }
            private set
            {
                if (this.eCIMode != value)
                {
                    this.eCIMode = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public FNC1Mode FNC1Mode
        {
            get
            {
                return this.fNC1Mode;
            }
            private set
            {
                if (this.fNC1Mode != value)
                {
                    this.fNC1Mode = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string ApplicationIndicator
        {
            get
            {
                return this.applicationIndicator;
            }
            private set
            {
                if (this.applicationIndicator != value)
                {
                    this.applicationIndicator = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                if (this.isValid != value)
                {
                    this.isValid = value;
                    this.OnIsValidPropertyChanged();
                    this.OnPropertyChanged();
                }
            }
        }

        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                if (this.errorMessage != value)
                {
                    this.errorMessage = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool ShowErrorMessage
        {
            get
            {
                return this.showErrorMessage;
            }
            set
            {
                if (this.showErrorMessage != value)
                {
                    this.showErrorMessage = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string MessageText
        {
            get
            {
                return this.messageText;
            }
            set
            {
                if (this.messageText != value)
                {
                    this.messageText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        protected override Task NavigateToConfigurationOverride()
        {
            this.ShowErrorMessage = false;
            this.MessageText = ConfigurationMessage;
            return this.NavigationService.NavigateToConfigurationAsync(this.configurationViewModel);
        }

        internal override void OnAppearing()
        {
            base.OnAppearing();

            switch (this.configurationViewModel.ConfigurationType)
            {
                case ConfigurationType.Link:
                    {
                        this.GenerateLinkQRCode();
                    }
                    break;

                case ConfigurationType.Text:
                    {
                        this.GenerateTextQRCode();
                    }
                    break;

                case ConfigurationType.Contact:
                    {
                        this.GenerateContactQRCode();
                    }
                    break;

                case ConfigurationType.Location:
                    {
                        this.GenerateLocationQRCode();
                    }
                    break;
            }
        }

        private void GenerateLinkQRCode()
        {
            this.Version = this.configurationViewModel.VersionSource.IndexOf(this.configurationViewModel.SelectedVersion);
            this.CodeMode = (CodeMode)Enum.Parse(typeof(CodeMode), this.configurationViewModel.SelectedEncoding);
            this.ECL = (ErrorCorrectionLevel)Enum.Parse(typeof(ErrorCorrectionLevel), this.configurationViewModel.SelectedECL);
            this.Value = this.configurationViewModel.URL;
            this.ECIMode = ECIMode.UTF8;
            this.FNC1Mode = FNC1Mode.None;
            this.ApplicationIndicator = null;
        }

        private void GenerateTextQRCode()
        {
            this.Version = this.configurationViewModel.VersionSource.IndexOf(this.configurationViewModel.SelectedVersion);
            this.CodeMode = (CodeMode)Enum.Parse(typeof(CodeMode), this.configurationViewModel.SelectedEncoding);
            this.ECL = (ErrorCorrectionLevel)Enum.Parse(typeof(ErrorCorrectionLevel), this.configurationViewModel.SelectedECL);
            this.Value = this.configurationViewModel.Text;
            this.ECIMode = (ECIMode)Enum.Parse(typeof(ECIMode), this.configurationViewModel.SelectedEciNumber);
            this.FNC1Mode = (FNC1Mode)Enum.Parse(typeof(FNC1Mode), this.configurationViewModel.SelectedFnc1Mode);
            this.ApplicationIndicator = this.configurationViewModel.IsApplicationIndicatorEnabled ? this.configurationViewModel.ApplicationIndicator : null;
        }

        private void GenerateContactQRCode()
        {
            string vCardText = "BEGIN:VCARD\r\nVERSION:2.1\r\nN:";
            vCardText += this.configurationViewModel.ContactName + " " + this.configurationViewModel.ContactFamily + "\r\n";
            vCardText += "TEL;WORK;VOICE:" + this.configurationViewModel.ContactPhone + "\r\n";
            vCardText += "EMAIL;PREF;INTERNET:" + this.configurationViewModel.ContactEmail + "\r\n";
            vCardText += "END:VCARD";

            this.CodeMode = CodeMode.Byte;
            this.Version = this.configurationViewModel.VersionSource.IndexOf(this.configurationViewModel.SelectedVersion);
            this.ECL = (ErrorCorrectionLevel)Enum.Parse(typeof(ErrorCorrectionLevel), this.configurationViewModel.SelectedECL);
            this.ECIMode = ECIMode.None;
            this.FNC1Mode = FNC1Mode.None;
            this.ApplicationIndicator = null;

            this.Value = vCardText;
        }

        private void GenerateLocationQRCode()
        {
            double latitude;
            double longitude;
            double.TryParse(this.configurationViewModel.LocationLat, out latitude);
            double.TryParse(this.configurationViewModel.LocationLong, out longitude);

            string url = "http://maps.google.com/maps?q=" + latitude.ToString() + "," + longitude.ToString() + "&num=1&t=m&z=12";

            this.CodeMode = CodeMode.Byte;
            this.Version = this.configurationViewModel.VersionSource.IndexOf(this.configurationViewModel.SelectedVersion);
            this.ECL = (ErrorCorrectionLevel)Enum.Parse(typeof(ErrorCorrectionLevel), this.configurationViewModel.SelectedECL);
            this.ECIMode = ECIMode.None;
            this.FNC1Mode = FNC1Mode.None;
            this.ApplicationIndicator = null;
            this.Value = url;
        }

        private void OnIsValidPropertyChanged()
        {
            if (!this.IsValid)
            {
                this.MessageText = this.ErrorMessage;
                this.ShowErrorMessage = true;
                var oldConfiguration = this.configurationViewModel;

                this.configurationViewModel = new QRCodeConfigurationViewModel();
                this.GenerateLinkQRCode();

                this.configurationViewModel = oldConfiguration;
            }
        }
    }
}
