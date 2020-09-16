using QSF.Examples.TimePickerControl.Models;
using QSF.ViewModels;
using QSF.ViewModels.Common;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QSF.Examples.TimePickerControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
        private ObservableCollection<Alarm> alarms;
        private PickerConfigurationMenuViewModel timePickerConfigurationViewModel;
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
            this.Alarms = new ObservableCollection<Alarm>
            {
                new Alarm() {SelectedHour = new TimeSpan(6, 0, 0) },
                new Alarm() {SelectedHour = new TimeSpan(7, 0, 0) },
                new Alarm() {SelectedHour = new TimeSpan(8, 0, 0), IsEnabled = true },
            };
            this.AddAlarmCommand = new Command(this.AddAlarm);
            this.timePickerConfigurationViewModel = new PickerConfigurationMenuViewModel();
        }

        public ObservableCollection<Alarm> Alarms
        {
            get
            {
                return this.alarms;
            }
            set
            {
                if (this.alarms != value)
                {
                    this.alarms = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand AddAlarmCommand { get; set; }

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

            this.IsHeaderVisible = this.timePickerConfigurationViewModel.IsHeaderVisible;
            this.PopupHeaderBackgroundColor = this.timePickerConfigurationViewModel.PopupHeaderBackgroundColor.Color;
            this.PopupHeaderFontColor = this.timePickerConfigurationViewModel.PopupHeaderFontColor.Color;
            this.ConfirmationButtonText = this.timePickerConfigurationViewModel.ConfirmationButtonText;
            this.CancellationButtonText = this.timePickerConfigurationViewModel.CancellationButtonText;
            this.SelectedItemFontColor = this.timePickerConfigurationViewModel.SelectedItemFontColor.Color;
            this.SelectedItemFontSize = this.timePickerConfigurationViewModel.SelectedItemFontSize;
            this.SelectedItemFontAttribute = this.timePickerConfigurationViewModel.SelectedItemFontAttribute;
            this.SelectedItemBackgroundColor = this.timePickerConfigurationViewModel.SelectedItemBackgroundColor.Color;
            this.SpinnerItemFontSize = this.timePickerConfigurationViewModel.SpinnerItemFontSize;
            this.SpinnerItemFontAttribute = this.timePickerConfigurationViewModel.SpinnerItemFontAttribute;
            this.SpinnerItemFontColor = this.timePickerConfigurationViewModel.SpinnerItemFontColor.Color;
            this.SpinnerItemBackgroundColor = this.timePickerConfigurationViewModel.SpinnerItemBackgroundColor.Color;
            this.ConfirmationButtonBackgroundColor = this.timePickerConfigurationViewModel.ConfirmationButtonBackgroundColor.Color;
            this.CancellationButtonBackgroundColor = this.timePickerConfigurationViewModel.CancellationButtonBackgroundColor.Color;
        }

        protected override Task NavigateToConfigurationOverride()
        {
            return this.NavigationService.NavigateToConfigurationAsync(this.timePickerConfigurationViewModel);
        }

        private void AddAlarm()
        {
            Alarm lastAlarm = this.Alarms[this.Alarms.Count - 1];
            var nAlarm = new Alarm()
            {
                SelectedHour = lastAlarm.SelectedHour,
                Name = lastAlarm.Name,
                IsEnabled = lastAlarm.IsEnabled
            };
            this.Alarms.Add(nAlarm);
            nAlarm.IsPickerOpened = true;
        }
    }
}
