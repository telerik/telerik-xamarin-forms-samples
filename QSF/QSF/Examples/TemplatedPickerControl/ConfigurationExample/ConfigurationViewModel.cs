using System.Collections.ObjectModel;
using System.Threading.Tasks;
using QSF.Examples.TemplatedPickerControl.Models;
using QSF.Services;
using QSF.Services.Toast;
using QSF.ViewModels;
using QSF.ViewModels.Common;
using Telerik.XamarinForms.Common;
using Xamarin.Forms;

namespace QSF.Examples.TemplatedPickerControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
        private Country fromCountry, highlightedFromCountry, destinationCountry, highlightedDestinationCountry;
        private City fromCity, highlightedFromCity, destinationCity, highlightedDestinationCity;
        private int adults = 1;
        private int children;
        private PickerConfigurationMenuViewModel templatedPickerConfigurationViewModel;
        private bool isHeaderVisible;
        private Color popupHeaderBackgroundColor;
        private Color popupHeaderFontColor;
        private string confirmationButtonText;
        private string cancellationButtonText;
        private Color selectedItemFontColor;
        private int selectedItemFontSize;
        private FontAttributes selectedItemFontAttribute;
        private Color selectedItemBackgroundColor;
        private int spinnerItemFontSize;
        private FontAttributes spinnerItemFontAttribute;
        private Color spinnerItemFontColor;
        private Color spinnerItemBackgroundColor;
        private Color confirmationButtonBackgroundColor;
        private Color cancellationButtonBackgroundColor;

        public ConfigurationViewModel()
        {
            this.OriginAcceptCommand = new Command(this.OnExecuteOriginAcceptCommand);
            this.OriginCancelCommand = new Command(this.OnExecuteOriginCancelCommand);
            this.DestinationAcceptCommand = new Command(this.OnExecuteDestinationAcceptCommand);
            this.DestinationCancelCommand = new Command(this.OnExecuteDestinationCancelCommand);
            this.SelectFlightCommand = new Command(this.OnExecuteSelectFlightCommand, this.CanExecuteSelectFlightCommand);

            this.Countries = new ObservableCollection<Country>
            {
                new Country
                {
                    Name = "Austria",
                    Cities =
                    {
                        new City
                        {
                            Name = "Graz"
                        },
                        new City
                        {
                            Name = "Innsbruck"
                        },
                        new City
                        {
                            Name = "Linz"
                        },
                        new City
                        {
                            Name = "Ratz"
                        },
                        new City
                        {
                            Name = "Salzburg"
                        },
                        new City
                        {
                            Name = "Vienna"
                        },
                        new City
                        {
                            Name = "Wolfsberg"
                        },
                        new City
                        {
                            Name = "Zeltweg"
                        }
                    }
                },
                new Country
                {
                    Name = "Belgium",
                    Cities =
                    {
                        new City
                        {
                            Name = "Antwerp"
                        },
                        new City
                        {
                            Name = "Assesse"
                        },
                        new City
                        {
                            Name = "Bruges"
                        },
                        new City
                        {
                            Name = "Charleroi"
                        },
                        new City
                        {
                            Name = "Lint"
                        },
                        new City
                        {
                            Name = "Ranst"
                        },
                        new City
                        {
                            Name = "Schaffen"
                        },
                        new City
                        {
                            Name = "Veurne"
                        },
                        new City
                        {
                            Name = "Zingem"
                        },
                    }
                },
                new Country
                {
                    Name = "Denmark",
                    Cities =
                    {
                        new City
                        {
                            Name = "Aalborg"
                        },
                        new City
                        {
                            Name = "Aarhus"
                        },
                        new City
                        {
                            Name = "Billund"
                        },
                        new City
                        {
                            Name = "Copenhagen"
                        },
                        new City
                        {
                            Name = "Karup"
                        },
                        new City
                        {
                            Name = "Odense"
                        },
                        new City
                        {
                            Name = "Viborg"
                        },
                        new City
                        {
                            Name = "Vojens"
                        }
                    }
                },
                new Country
                {
                    Name = "France",
                    Cities =
                    {
                        new City
                        {
                            Name = "Aurillac"
                        },
                        new City
                        {
                            Name = "Belley"
                        },
                        new City
                        {
                            Name = "Bourg-en-Bresse"
                        },
                        new City
                        {
                            Name = "Carcassonne"
                        },
                        new City
                        {
                            Name = "Caen"
                        },
                        new City
                        {
                            Name = "Deauville"
                        },
                        new City
                        {
                            Name = "La Rochelle"
                        },
                        new City
                        {
                            Name = "Nice"
                        },
                        new City
                        {
                            Name = "Marseille"
                        },
                        new City
                        {
                            Name = "Paris - Val-De-Marne"
                        },
                        new City
                        {
                            Name = "Paris - Val d'Oise"
                        },
                        new City
                        {
                            Name = "Rodez"
                        }
                    }
                },
                new Country
                {
                    Name = "Germany",
                    Cities =
                    {
                        new City
                        {
                            Name = "Baden-Baden"
                        },
                        new City
                        {
                            Name = "Berlin"
                        },
                        new City
                        {
                            Name = "Borkum"
                        },
                        new City
                        {
                            Name = "Bremen"
                        },
                        new City
                        {
                            Name = "Dortmund"
                        },
                        new City
                        {
                            Name = "Dresden"
                        },
                        new City
                        {
                            Name = "Hamburg"
                        },
                        new City
                        {
                            Name = "Hannover"
                        },
                        new City
                        {
                            Name = "Leipzig"
                        },
                        new City
                        {
                            Name = "Mannheim"
                        },
                        new City
                        {
                            Name = "Munich"
                        },
                        new City
                        {
                            Name = "Nuremberg"
                        }
                    }
                },
                new Country
                {
                    Name = "Italy",
                    Cities =
                    {
                        new City
                        {
                            Name = "Aosta"
                        },
                        new City
                        {
                            Name = "Bari"
                        },
                        new City
                        {
                            Name = "Bologna"
                        },
                        new City
                        {
                            Name = "Parma"
                        },
                        new City
                        {
                            Name = "Rimini"
                        },
                        new City
                        {
                            Name = "Rome - Fiumicino"
                        },
                        new City
                        {
                            Name = "Rome - Ciampino"
                        }
                    }
                },
                new Country
                {
                    Name = "Netherlands",
                    Cities =
                    {
                        new City
                        {
                            Name = "Amsterdam"
                        },
                        new City
                        {
                            Name = "Bonaire"
                        },
                        new City
                        {
                            Name = "Eindhoven"
                        },
                        new City
                        {
                            Name = "Maastricht"
                        },
                        new City
                        {
                            Name = "Rotterdam"
                        }
                    }
                },
                new Country
                {
                    Name = "Portugal",
                    Cities =
                    {
                        new City
                        {
                            Name = "Braga"
                        },
                        new City
                        {
                            Name = "Cascais"
                        },
                        new City
                        {
                            Name = "Lisbon"
                        },
                        new City
                        {
                            Name = "Porto"
                        }
                    }
                },
                new Country
                {
                    Name = "Spain",
                    Cities =
                    {
                        new City
                        {
                            Name = "Alicante"
                        },
                        new City
                        {
                            Name = "Barcelona"
                        },
                        new City
                        {
                            Name = "Madrid"
                        },
                        new City
                        {
                            Name = "Seville"
                        },
                        new City
                        {
                            Name = "Valencia"
                        },
                        new City
                        {
                            Name = "Zaragoza"
                        }
                    }
                },
                new Country
                {
                    Name = "United Kingdom",
                    Cities =
                    {
                        new City
                        {
                            Name = "Bristol Airport"
                        },
                        new City
                        {
                            Name = "Castle Donington"
                        },
                        new City
                        {
                            Name = "Liverpool"
                        },
                        new City
                        {
                            Name = "London City Airport"
                        },
                        new City
                        {
                            Name = "London Luton"
                        },
                        new City
                        {
                            Name = "Manchester Airport"
                        },
                        new City
                        {
                            Name = "Norwich"
                        },
                        new City
                        {
                            Name = "Southampton"
                        }
                    }
                },
            };
            this.Adults = 1;
            this.Children = 0;
            this.templatedPickerConfigurationViewModel = new PickerConfigurationMenuViewModel();
        }

        public Command DestinationAcceptCommand { get; }

        public Command DestinationCancelCommand { get; }

        public Command OriginCancelCommand { get; }

        public Command OriginAcceptCommand { get; }

        public Command SelectFlightCommand { get; }

        public Country HighlightedFromCountry
        {
            get
            {
                return this.highlightedFromCountry;
            }
            set
            {
                if (this.highlightedFromCountry != value)
                {
                    this.highlightedFromCountry = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Country HighlightedDestinationCountry
        {
            get
            {
                return this.highlightedDestinationCountry;
            }
            set
            {
                if (this.highlightedDestinationCountry != value)
                {
                    this.highlightedDestinationCountry = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Country FromCountry
        {
            get
            {
                return this.fromCountry;
            }
            set
            {
                if (this.fromCountry != value)
                {
                    this.fromCountry = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Country DestinationCountry
        {
            get
            {
                return this.destinationCountry;
            }
            set
            {
                if (this.destinationCountry != value)
                {
                    this.destinationCountry = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public City HighlightedFromCity
        {
            get
            {
                return this.highlightedFromCity;
            }
            set
            {
                if (this.highlightedFromCity != value)
                {
                    this.highlightedFromCity = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public City FromCity
        {
            get
            {
                return this.fromCity;
            }
            set
            {
                if (this.fromCity != value)
                {
                    this.fromCity = value;
                    this.OnPropertyChanged();
                    this.SelectFlightCommand.ChangeCanExecute();
                }
            }
        }

        public City HighlightedDestinationCity
        {
            get
            {
                return this.highlightedDestinationCity;
            }
            set
            {
                if (this.highlightedDestinationCity != value)
                {
                    this.highlightedDestinationCity = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public City DestinationCity
        {
            get
            {
                return this.destinationCity;
            }
            set
            {
                if (this.destinationCity != value)
                {
                    this.destinationCity = value;
                    this.OnPropertyChanged();
                    this.SelectFlightCommand.ChangeCanExecute();
                }
            }
        }

        public int Adults
        {
            get
            {
                return this.adults;
            }
            set
            {
                if (this.adults != value)
                {
                    this.adults = value;
                    this.OnPropertyChanged();
                    this.SelectFlightCommand.ChangeCanExecute();
                }
            }
        }

        public int Children
        {
            get
            {
                return this.children;
            }
            set
            {
                if (this.children != value)
                {
                    this.children = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Country> Countries { get; }

        public override bool HasConfiguration
        {
            get
            {
                return true;
            }
        }

        public override bool IsPopupHintOpen => true;

        public bool IsHeaderVisible
        {
            get
            {
                return this.isHeaderVisible;
            }
            set
            {
                if (this.isHeaderVisible != value)
                {
                    this.isHeaderVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color PopupHeaderBackgroundColor
        {
            get
            {
                return this.popupHeaderBackgroundColor;
            }
            set
            {
                if (this.popupHeaderBackgroundColor != value)
                {
                    this.popupHeaderBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color PopupHeaderFontColor
        {
            get
            {
                return this.popupHeaderFontColor;
            }
            set
            {
                if (this.popupHeaderFontColor != value)
                {
                    this.popupHeaderFontColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string ConfirmationButtonText
        {
            get
            {
                return this.confirmationButtonText;
            }
            set
            {
                if (this.confirmationButtonText != value)
                {
                    this.confirmationButtonText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string CancellationButtonText
        {
            get
            {
                return this.cancellationButtonText;
            }
            set
            {
                if (this.cancellationButtonText != value)
                {
                    this.cancellationButtonText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color SelectedItemFontColor
        {
            get
            {
                return this.selectedItemFontColor;
            }
            set
            {
                if (this.selectedItemFontColor != value)
                {
                    this.selectedItemFontColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color ConfirmationButtonBackgroundColor
        {
            get
            {
                return this.confirmationButtonBackgroundColor;
            }
            set
            {
                if (this.confirmationButtonBackgroundColor != value)
                {
                    this.confirmationButtonBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color CancellationButtonBackgroundColor
        {
            get
            {
                return this.cancellationButtonBackgroundColor;
            }
            set
            {
                if (this.cancellationButtonBackgroundColor != value)
                {
                    this.cancellationButtonBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int SelectedItemFontSize
        {
            get
            {
                return this.selectedItemFontSize;
            }
            set
            {
                if (this.selectedItemFontSize != value)
                {
                    this.selectedItemFontSize = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public FontAttributes SelectedItemFontAttribute
        {
            get
            {
                return this.selectedItemFontAttribute;
            }
            set
            {
                if (this.selectedItemFontAttribute != value)
                {
                    this.selectedItemFontAttribute = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color SelectedItemBackgroundColor
        {
            get
            {
                return this.selectedItemBackgroundColor;
            }
            set
            {
                if (this.selectedItemBackgroundColor != value)
                {
                    this.selectedItemBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int SpinnerItemFontSize
        {
            get
            {
                return this.spinnerItemFontSize;
            }
            set
            {
                if (this.spinnerItemFontSize != value)
                {
                    this.spinnerItemFontSize = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public FontAttributes SpinnerItemFontAttribute
        {
            get
            {
                return this.spinnerItemFontAttribute;
            }
            set
            {
                if (this.spinnerItemFontAttribute != value)
                {
                    this.spinnerItemFontAttribute = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color SpinnerItemFontColor
        {
            get
            {
                return this.spinnerItemFontColor;
            }
            set
            {
                if (this.spinnerItemFontColor != value)
                {
                    this.spinnerItemFontColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color SpinnerItemBackgroundColor
        {
            get
            {
                return this.spinnerItemBackgroundColor;
            }
            set
            {
                if (this.spinnerItemBackgroundColor != value)
                {
                    this.spinnerItemBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        internal override void OnAppearing()
        {
            base.OnAppearing();

            this.IsHeaderVisible = this.templatedPickerConfigurationViewModel.IsHeaderVisible;
            this.PopupHeaderBackgroundColor = this.templatedPickerConfigurationViewModel.PopupHeaderBackgroundColor.Color;
            this.PopupHeaderFontColor = this.templatedPickerConfigurationViewModel.PopupHeaderFontColor.Color;
            this.ConfirmationButtonText = this.templatedPickerConfigurationViewModel.ConfirmationButtonText;
            this.CancellationButtonText = this.templatedPickerConfigurationViewModel.CancellationButtonText;
            this.SelectedItemFontColor = this.templatedPickerConfigurationViewModel.SelectedItemFontColor.Color;
            this.SelectedItemFontSize = this.templatedPickerConfigurationViewModel.SelectedItemFontSize;
            this.SelectedItemFontAttribute = this.templatedPickerConfigurationViewModel.SelectedItemFontAttribute;
            this.SelectedItemBackgroundColor = this.templatedPickerConfigurationViewModel.SelectedItemBackgroundColor.Color;
            this.SpinnerItemFontSize = this.templatedPickerConfigurationViewModel.SpinnerItemFontSize;
            this.SpinnerItemFontAttribute = this.templatedPickerConfigurationViewModel.SpinnerItemFontAttribute;
            this.SpinnerItemFontColor = this.templatedPickerConfigurationViewModel.SpinnerItemFontColor.Color;
            this.SpinnerItemBackgroundColor = this.templatedPickerConfigurationViewModel.SpinnerItemBackgroundColor.Color;
            this.ConfirmationButtonBackgroundColor = this.templatedPickerConfigurationViewModel.ConfirmationButtonBackgroundColor.Color;
            this.CancellationButtonBackgroundColor = this.templatedPickerConfigurationViewModel.CancellationButtonBackgroundColor.Color;
        }

        protected override Task NavigateToConfigurationOverride()
        {
            return this.NavigationService.NavigateToConfigurationAsync(this.templatedPickerConfigurationViewModel);
        }

        private void OnExecuteDestinationCancelCommand()
        {
            this.HighlightedDestinationCity = this.DestinationCity;
            this.HighlightedDestinationCountry = this.DestinationCountry;
        }

        private void OnExecuteDestinationAcceptCommand()
        {
            this.DestinationCity = this.HighlightedDestinationCity;
            this.DestinationCountry = this.HighlightedDestinationCountry;
        }

        private void OnExecuteOriginCancelCommand()
        {
            this.HighlightedFromCity = this.FromCity;
            this.HighlightedFromCountry = this.FromCountry;
        }

        private void OnExecuteOriginAcceptCommand()
        {
            this.FromCity = this.HighlightedFromCity;
            this.FromCountry = this.HighlightedFromCountry;
        }

        private bool CanExecuteSelectFlightCommand()
        {
            return this.FromCity != null && this.DestinationCity != null && this.Adults > 0;
        }

        private void OnExecuteSelectFlightCommand()
        {
            this.FromCity = null;
            this.DestinationCity = null;
            this.Adults = 0;
            this.Children = 0;

            var toastService = DependencyService.Get<IToastMessageService>();

            toastService.ShortAlert("Flight origin and destination chosen.");
        }
    }
}
