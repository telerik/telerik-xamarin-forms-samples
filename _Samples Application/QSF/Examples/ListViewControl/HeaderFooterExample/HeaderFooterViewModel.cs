using System.Collections.ObjectModel;
using System.ComponentModel;
using QSF.Services.Toast;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.ListViewControl.HeaderFooterExample
{
    public class HeaderFooterViewModel : ExampleViewModel
    {
        private string label;
        private double total;

        public HeaderFooterViewModel()
        {
            this.Label = "Pay Bills";

            this.Items = new ObservableCollection<ListItem>
            {
                new ListItem
                {
                    Name = "Rent",
                    Icon = "\uE85B",
                    Company = "East Orange Flat",
                    Amount = 1800
                },
                new ListItem
                {
                    Name = "Electricity",
                    Icon = "\uE85E",
                    Company = "Energy United",
                    Amount = 32
                },
                new ListItem
                {
                    Name = "Heating",
                    Icon = "\uE85C",
                    Company = "National Fuel Gas",
                    Amount = 98
                },
                new ListItem
                {
                    Name = "Cell Phone",
                    Icon = "\uE85D",
                    Company = "T-Mobile US",
                    Amount = 26
                },
                new ListItem
                {
                    Name = "Auto Loan",
                    Icon = "\uE832",
                    Company = "Ford Ranger",
                    Amount = 320
                },
                new ListItem
                {
                    Name = "Cable Company",
                    Icon = "\uE85F",
                    Company = "Spectrum",
                    Amount = 20
                },
                new ListItem
                {
                    Name = "Trash Pickup",
                    Icon = "\uE827",
                    Company = "Junk Removal NYC",
                    Amount = 18
                },
                new ListItem
                {
                    Name = "Office Rent",
                    Icon = "\uE85B",
                    Company = "NY, Hudson Square 24",
                    Amount = 2600
                },
                new ListItem
                {
                    Name = "Electricity",
                    Icon = "\uE85E",
                    Company = "Direct Energy",
                    Amount = 82
                },
                new ListItem
                {
                    Name = "Heating",
                    Icon = "\uE85C",
                    Company = "Constellation Gas",
                    Amount = 112
                },
                new ListItem
                {
                    Name = "Auto Leasing",
                    Icon = "\uE832",
                    Company = "Chevrolet Trax",
                    Amount = 640
                },
                new ListItem
                {
                    Name = "Internet",
                    Icon = "\uE85F",
                    Company = "RCN",
                    Amount = 24
                },
            };

            foreach (var item in this.Items)
            {
                item.PropertyChanged += this.OnPropertyChanged;
            }

            this.TapCommand = new Command<ListItem>(this.OnTapCommand);
            this.PayCommand = new Command(this.OnPayCommand);
        }

        public string Label
        {
            get
            {
                return this.label;
            }
            private set
            {
                if (this.label != value)
                {
                    this.label = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double Total
        {
            get
            {
                return this.total;
            }
            private set
            {
                if (this.total != value)
                {
                    this.total = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ListItem> Items { get; }
        public Command TapCommand { get; }
        public Command PayCommand { get; }

        private void OnTapCommand(ListItem item)
        {
            item.IsSelected = !item.IsSelected;
        }

        private void OnPayCommand()
        {
            if (this.Total > 0)
            {
                foreach (var item in this.Items)
                {
                    if (item.IsSelected)
                    {
                        item.IsSelected = false;
                        item.Amount = 0;
                    }
                }

                var toastService = DependencyService.Get<IToastMessageService>();

                toastService.ShortAlert("The bills have been paid.");
            }
        }

        private void OnItemSelected()
        {
            var value = 0d;

            foreach (var item in this.Items)
            {
                if (item.IsSelected)
                {
                    value += item.Amount;
                }
            }

            this.Total = value;

            if (value > 0)
            {
                this.Label = $"Pay ${value}";
            }
            else
            {
                this.Label = "Pay Bills";
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            if (eventArgs.PropertyName == nameof(ListItem.IsSelected))
            {
                this.OnItemSelected();
            }
        }
    }
}
