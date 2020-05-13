using System.Collections.ObjectModel;
using QSF.Examples.TemplatedPickerControl.Models;
using QSF.Services.Toast;
using Telerik.XamarinForms.Common;
using Xamarin.Forms;

namespace QSF.Examples.TemplatedPickerControl.CustomizationExample
{
    public class ViewModel : NotifyPropertyChangedBase
    {
        private Country fromCountry, highlightedFromCountry, destinationCountry, highlightedDestinationCountry;
        private City fromCity, highlightedFromCity, destinationCity, highlightedDestinationCity;
        private int adults = 1;
        private int children;

        public ViewModel()
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
                this.UpdateValue(ref this.highlightedFromCountry, value);
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
                this.UpdateValue(ref this.highlightedDestinationCountry, value);
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
                this.UpdateValue(ref this.fromCountry, value);
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
                this.UpdateValue(ref this.destinationCountry, value);
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
                this.UpdateValue(ref this.highlightedFromCity, value);
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
                if (this.UpdateValue(ref this.fromCity, value))
                {
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
                this.UpdateValue(ref this.highlightedDestinationCity, value);
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
                if (this.UpdateValue(ref this.destinationCity, value))
                {
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
                if (this.UpdateValue(ref this.adults, value))
                {
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
                this.UpdateValue(ref this.children, value);
            }
        }

        public ObservableCollection<Country> Countries { get; }

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
