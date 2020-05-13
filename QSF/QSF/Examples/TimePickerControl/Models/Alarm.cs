using System;
using System.Windows.Input;
using Telerik.XamarinForms.Common;
using Xamarin.Forms;

namespace QSF.Examples.TimePickerControl.Models
{
    public class Alarm : NotifyPropertyChangedBase
    {
        private TimeSpan? selectedHour;
        private string name;
        private bool isEnabled, isPickerOpened, isCustomized;
        private Color pickerTextColor;
        private Color nameTextColor;

        public Alarm()
        {
            this.name = "Alarm";
            this.isEnabled = false;
            this.isCustomized = false;
            this.pickerTextColor = Color.Black;
            this.nameTextColor = Color.FromHex("#6A6A6A");
            this.AccentColor = Color.FromHex("#B73562");
            this.SwitchColor = Color.FromHex("#66008265");
        }

        public bool IsCustomized
        {
            get
            {
                return this.isCustomized;
            }
            set
            {
                if (this.isCustomized != value)
                {
                    this.isCustomized = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsPickerOpened
        {
            get
            {
                return this.isPickerOpened;
            }
            set
            {
                if (this.isPickerOpened != value)
                {
                    this.isPickerOpened = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public TimeSpan? SelectedHour
        {
            get
            {
                return this.selectedHour;
            }
            set
            {
                if (this.selectedHour != value)
                {
                    this.selectedHour = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                if (this.isEnabled != value)
                {
                    this.isEnabled = value;
                    this.OnPropertyChanged();
                    this.ChangeToggledColor();
                }
            }
        }

        public Color PickerTextColor
        {
            get
            {
                return this.pickerTextColor;
            }
            set
            {
                if (this.pickerTextColor != value)
                {
                    this.pickerTextColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color NameTextColor
        {
            get
            {
                return this.nameTextColor;
            }
            set
            {
                if (this.nameTextColor != value)
                {
                    this.nameTextColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color AccentColor { get; }

        public Color SwitchColor { get; }

        private void ChangeToggledColor()
        {
            if (this.isEnabled)
            {
                this.PickerTextColor = this.AccentColor;
                this.NameTextColor = this.AccentColor;
            }
            else
            {
                this.PickerTextColor = Color.Black;
                this.NameTextColor = Color.FromHex("#6A6A6A");
            }
        }
    }
}
