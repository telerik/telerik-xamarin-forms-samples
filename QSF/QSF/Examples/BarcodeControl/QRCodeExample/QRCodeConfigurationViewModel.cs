using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.XamarinForms.Barcode;

namespace QSF.Examples.BarcodeControl.QRCodeExample
{
    public class QRCodeConfigurationViewModel : ConfigurationViewModel
    {
        private string url;
        private string[] eCLSource;
        private string selectedECL;
        private string[] versionSource;
        private string selectedVersion;
        private string[] encodingSource;
        private string selectedEncoding;
        private int selectedTabIndex;
        private string applicationIndicator;
        private bool isApplicationIndicatorEnabled;
        private string[] fnc1ModeSource;
        private string selectedFnc1Mode;
        private string[] eciNumberSource;
        private string selectedEciNumber;
        private string text;
        private string contactName;
        private string contactFamily;
        private string contactPhone;
        private string contactEmail;
        private string locationLat;
        private string locationLong;

        public QRCodeConfigurationViewModel()
        {
            this.ECLSource = this.GetEnumValues(typeof(ErrorCorrectionLevel));

            var versionSource = Enumerable.Range(0, 40).Select(p => p.ToString()).ToArray();
            versionSource[0] = "Auto";
            this.VersionSource = versionSource;

            this.EncodingSource = this.GetEnumValues(typeof(CodeMode)); ;

            this.Fnc1ModeSource = this.GetEnumValues(typeof(FNC1Mode));

            this.EciNumberSource = this.GetEnumValues(typeof(ECIMode));

            this.SelectedECL = ErrorCorrectionLevel.H.ToString();
            this.SelectedEncoding = CodeMode.Alphanumeric.ToString();
            this.SelectedVersion = "Auto";
            this.SelectedFnc1Mode = FNC1Mode.None.ToString();
            this.SelectedEciNumber = ECIMode.None.ToString();
            this.URL = "http://www.telerik.com/";
            this.Text = "QR Code by Telerik";
            this.ContactName = "Percy";
            this.ContactFamily = "Donovan";
            this.ContactEmail = "d.percy@company.com";
            this.ContactPhone = "+ 1 555 737 458";
            this.LocationLat = "42.650565";
            this.LocationLong = "23.379228";
        }

        public string URL
        {
            get
            {
                return this.url;
            }
            set
            {
                if (this.url != value)
                {
                    this.url = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                if (this.text != value)
                {
                    this.text = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string[] ECLSource
        {
            get
            {
                return this.eCLSource;
            }
            private set
            {
                if (this.eCLSource != value)
                {
                    this.eCLSource = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string SelectedECL
        {
            get
            {
                return this.selectedECL;
            }
            set
            {
                if (this.selectedECL != value)
                {
                    this.selectedECL = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string[] VersionSource
        {
            get
            {
                return this.versionSource;
            }
            private set
            {
                if (this.versionSource != value)
                {
                    this.versionSource = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string SelectedVersion
        {
            get
            {
                return this.selectedVersion;
            }
            set
            {
                if (this.selectedVersion != value)
                {
                    this.selectedVersion = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string[] EncodingSource
        {
            get
            {
                return this.encodingSource;
            }
            private set
            {
                if (this.encodingSource != value)
                {
                    this.encodingSource = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string[] Fnc1ModeSource
        {
            get
            {
                return this.fnc1ModeSource;
            }
            private set
            {
                if (this.fnc1ModeSource != value)
                {
                    this.fnc1ModeSource = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string SelectedFnc1Mode
        {
            get
            {
                return this.selectedFnc1Mode;
            }
            set
            {
                if (this.selectedFnc1Mode != value)
                {
                    this.selectedFnc1Mode = value;
                    this.OnSelectedFnc1ModeChanged();
                    this.OnPropertyChanged();
                }
            }
        }

        public string SelectedEncoding
        {
            get
            {
                return this.selectedEncoding;
            }
            set
            {
                if (this.selectedEncoding != value)
                {
                    this.selectedEncoding = value;
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
            set
            {
                if (this.applicationIndicator != value)
                {
                    this.applicationIndicator = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsApplicationIndicatorEnabled
        {
            get
            {
                return this.isApplicationIndicatorEnabled;
            }
            private set
            {
                if (this.isApplicationIndicatorEnabled != value)
                {
                    this.isApplicationIndicatorEnabled = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string[] EciNumberSource
        {
            get
            {
                return this.eciNumberSource;
            }
            private set
            {
                if (this.eciNumberSource != value)
                {
                    this.eciNumberSource = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string SelectedEciNumber
        {
            get
            {
                return this.selectedEciNumber;
            }
            set
            {
                if (this.SelectedEciNumber != value)
                {
                    this.selectedEciNumber = value;
                    this.OnPropertyChanged();
                }
            }
        }

        internal ConfigurationType ConfigurationType { get; private set; }

        internal int SelectedTabIndex
        {
            get
            {
                return this.selectedTabIndex;
            }
            set
            {
                if (this.selectedTabIndex != value)
                {
                    this.selectedTabIndex = value;
                    this.ConfigurationType = (ConfigurationType)value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string ContactName
        {
            get
            {
                return this.contactName;
            }
            set
            {
                if (this.contactName != value)
                {
                    this.contactName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string ContactFamily
        {
            get
            {
                return this.contactFamily;
            }
            set
            {
                if (this.contactFamily != value)
                {
                    this.contactFamily = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string ContactPhone
        {
            get
            {
                return this.contactPhone;
            }
            set
            {
                if (this.contactPhone != value)
                {
                    this.contactPhone = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string ContactEmail
        {
            get
            {
                return this.contactEmail;
            }
            set
            {
                if (this.contactEmail != value)
                {
                    this.contactEmail = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string LocationLat
        {
            get
            {
                return this.locationLat;
            }
            set
            {
                if (this.locationLat != value)
                {
                    this.locationLat = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string LocationLong
        {
            get
            {
                return this.locationLong;
            }
            set
            {
                if (this.locationLong != value)
                {
                    this.locationLong = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private void OnSelectedFnc1ModeChanged()
        {
            this.IsApplicationIndicatorEnabled = this.SelectedFnc1Mode != FNC1Mode.None.ToString();
        }

        private string[] GetEnumValues(Type type)
        {
            List<string> result = new List<string>();

            var values = Enum.GetValues(type);
            foreach (var value in values)
            {
                result.Add(value.ToString());
            }

            return result.ToArray(); ;
        }
    }
}
