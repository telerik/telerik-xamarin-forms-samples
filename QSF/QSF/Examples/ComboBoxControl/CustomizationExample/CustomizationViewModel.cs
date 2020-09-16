using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.ComboBoxControl.CustomizationExample
{
    public class CustomizationViewModel : ExampleViewModel
    {
        private int selectedColorIndex = -1;
        private int selectedSizeIndex = -1;
        private int selectedStoreIndex = -1;
        private bool isAvailabilityNotificationOpen;
        private string availabilityText;
        private bool? selectAllChecked = false;
        private bool isInternalCheckChanged;
        public ObservableCollection<object> selectedStores;
        private CancellationTokenSource cancellation;

        public CustomizationViewModel()
        {
            this.Colors = new ObservableCollection<string>()
            {
                "Blue",
                "Yellow",
                "Pink",
                "Orange",
                "Gray"
            };

            this.Sizes = new ObservableCollection<string>()
            {
                "XXS",
                "XS",
                "S",
                "M",
                "L",
                "XL",
                "XXL"
            };

            this.Stores = new ObservableCollection<StoreAddress>();
            this.Stores.Add(new StoreAddress() { City = "New York", Street = "536 Halifax Road", Code = "Woodside, NY 11377", WorkHours = "Open 11am to 6pm" });
            this.Stores.Add(new StoreAddress() { City = "New York", Street = "8339 Belmont Court", Code = "East Elmhurst, NY 11370", WorkHours = "Open 11am to 6pm" });
            this.Stores.Add(new StoreAddress() { City = "New Jersey", Street = "4759 Cherry Camp Road", Code = "NJ 07097", WorkHours = "Open 10am to 7pm" });
            this.Stores.Add(new StoreAddress() { City = "New Jersey", Street = "2301 Finwood Road", Code = "NJ 08901", WorkHours = "Open 10am to 7pm" });
            this.Stores.Add(new StoreAddress() { City = "San Francisco", Street = "3082 Rardin Drive", Code = "CA 94107", WorkHours = "Open 10am to 7pm" });
            this.Stores.Add(new StoreAddress() { City = "San Francisco", Street = "2740 Lynch Street", Code = "CA 94107", WorkHours = "Open 10am to 7pm" });
            this.Stores.Add(new StoreAddress() { City = "San Diego", Street = "1593 Hood Avenue", Code = "CA 92123", WorkHours = "Open 11am to 6pm" });
            this.Stores.Add(new StoreAddress() { City = "San Diego", Street = "2055 Holden Street", Code = "CA 92123", WorkHours = "Open 11am to 6pm" });
            this.Stores.Add(new StoreAddress() { City = "Los Angeles", Street = "28 Woodstock Drive", Code = "CA 90017", WorkHours = "Open 6am to 9pm" });
            this.Stores.Add(new StoreAddress() { City = "Los Angeles", Street = "4798 Euclid Avenue", Code = "CA 90017", WorkHours = "Open 6am to 9pm" });
            this.Stores.Add(new StoreAddress() { City = "Dallas", Street = "2586 Sardis Sta", Code = "X 75201", WorkHours = "Open 10am to 9pm" });
            this.Stores.Add(new StoreAddress() { City = "Austin", Street = "3684 Sundown Lane", Code = "TX 78741", WorkHours = "Open 10am to 67pm" });

            this.SearcAvailabilityhButtonCommand = new Command(this.OnSearchAvailabilityButtonCommandExecuted, this.OnSearchAvailabilityButtonCommandCanExecute);
            this.RemoveTokenCommand = new Command(this.OnRemoveTokenCommand);

            this.cancellation = new CancellationTokenSource();

            this.SelectedStores = new ObservableCollection<object>();

            this.SelectAllCommand = new Command(this.OnSelectAllCommandExecute);
        }

        public ICommand SelectAllCommand { get; set; }

        public ObservableCollection<string> Colors { get; set; }
        public ObservableCollection<string> Sizes { get; set; }
        public ObservableCollection<StoreAddress> Stores { get; set; }
        public ObservableCollection<object> SelectedStores
        {
            get
            {
                return this.selectedStores;
            }
            set
            {
                if (this.selectedStores != value)
                {
                    if (this.selectedStores != null)
                    {
                        this.selectedStores.CollectionChanged -= this.OnSelectedStoresCollectionChanged;
                    }

                    this.selectedStores = value;

                    if (this.selectedStores != null)
                    {
                        this.selectedStores.CollectionChanged += this.OnSelectedStoresCollectionChanged;
                    }

                    this.OnPropertyChanged();
                }
            }
        }
        public ICommand SearcAvailabilityhButtonCommand { get; set; }
        public ICommand RemoveTokenCommand { get; set; }

        public int SelectedColorIndex
        {
            get
            {
                return this.selectedColorIndex;
            }
            set
            {
                if (this.selectedColorIndex != value)
                {
                    this.selectedColorIndex = value;
                    ((Command)this.SearcAvailabilityhButtonCommand).ChangeCanExecute();
                    this.OnPropertyChanged();
                }
            }
        }

        public int SelectedSizeIndex
        {
            get
            {
                return this.selectedSizeIndex;
            }
            set
            {
                if (this.selectedSizeIndex != value)
                {
                    this.selectedSizeIndex = value;
                    ((Command)this.SearcAvailabilityhButtonCommand).ChangeCanExecute();
                    this.OnPropertyChanged();
                }
            }
        }

        public int SelectedStoreIndex
        {
            get
            {
                return this.selectedStoreIndex;
            }
            set
            {
                if (this.selectedStoreIndex != value)
                {
                    this.selectedStoreIndex = value;
                    ((Command)this.SearcAvailabilityhButtonCommand).ChangeCanExecute();
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsAvailabilityNotificationOpen
        {
            get
            {
                return this.isAvailabilityNotificationOpen;
            }
            set
            {
                if (this.isAvailabilityNotificationOpen != value)
                {
                    this.isAvailabilityNotificationOpen = value;
                    if (this.isAvailabilityNotificationOpen)
                    {
                        var rnd = new Random();
                        this.AvailabilityText = $"Product found in {rnd.Next(1, this.SelectedStores.Count + 1)} stores.";
                    }
                    else
                    {
                        Interlocked.Exchange(ref this.cancellation, new CancellationTokenSource()).Cancel();
                    }

                    this.OnPropertyChanged();
                }
            }
        }

        public string AvailabilityText
        {
            get
            {
                return this.availabilityText;
            }
            set
            {
                if (this.availabilityText != value)
                {
                    this.availabilityText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool? SelectAllChecked
        {
            get
            {
                return this.selectAllChecked;
            }
            set
            {
                if (this.selectAllChecked != value)
                {
                    this.selectAllChecked = value;

                    if (!this.isInternalCheckChanged && this.selectAllChecked.HasValue)
                    {
                        if (this.selectAllChecked.Value)
                        {
                            foreach (var store in this.Stores)
                            {
                                if (!this.SelectedStores.Contains(store))
                                {
                                    this.SelectedStores.Add(store);
                                }
                            }
                        }
                        else
                        {
                            this.SelectedStores.Clear();
                        }
                    }

                    this.OnPropertyChanged();
                }
            }
        }

        private void OnSearchAvailabilityButtonCommandExecuted(object obj)
        {
            if (this.isAvailabilityNotificationOpen)
            {
                return;
            }

            CancellationTokenSource cts = this.cancellation;
            this.IsAvailabilityNotificationOpen = true;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                if (cts.IsCancellationRequested)
                {
                    return false;
                }

                this.IsAvailabilityNotificationOpen = false;
                return false;
            });
        }

        private bool OnSearchAvailabilityButtonCommandCanExecute(object arg)
        {
            return this.selectedColorIndex != -1
                && this.selectedSizeIndex != -1
                && this.selectedStoreIndex != -1;
        }



        private void OnRemoveTokenCommand(object obj)
        {
            this.SelectedStores.Remove(obj);
        }

        private void OnSelectedStoresCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var action = e.Action;
            if (action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                this.isInternalCheckChanged = true;
                if (this.SelectedStores.Count == this.Stores.Count)
                {
                    this.SelectAllChecked = true;
                }
                else
                {
                    this.SelectAllChecked = null;
                }
                this.isInternalCheckChanged = false;

                return;
            }

            if (action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                this.isInternalCheckChanged = true;
                if (this.SelectedStores.Count == 0)
                {
                    this.SelectAllChecked = false;
                }
                else
                {
                    this.SelectAllChecked = null;
                }
                this.isInternalCheckChanged = false;
            }
        }

        private void OnSelectAllCommandExecute(object obj)
        {
            if (this.selectAllChecked == null)
            {
                this.SelectAllChecked = false;
            }
            else
            {
                this.SelectAllChecked = !this.selectAllChecked;
            }
        }
    }
}