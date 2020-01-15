using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.AgendaViewConfigurationExample
{
    public class ConfigurationViewModel : ViewModels.ConfigurationViewModel
    {
        private string monthItemFormat;
        private string weekItemStartDateFormat;
        private string weekItemEndDateFormat;
        private string dayItemFormat;
        private string appointmentItemStartDateFormat;
        private string appointmentItemEndDateFormat;
        private string appointmentItemTimeFormat;

        public ConfigurationViewModel()
        {
            this.appointmentItemStartDateFormat = "dd MMMM";
            this.appointmentItemEndDateFormat = "dd MMMM";
            this.weekItemEndDateFormat = "d";
            this.monthItemFormat = "MMMM yyyy";
            this.weekItemStartDateFormat = "MMMM d";
            this.dayItemFormat = "EEEE, d MMMM";

            if (Device.RuntimePlatform == Device.iOS)
            {
                this.appointmentItemTimeFormat = "HH:mm";
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                this.appointmentItemTimeFormat = "h:mm a";
            }
        }

        public string MonthItemFormat
        {
            get
            {
                return this.monthItemFormat;
            }
            set
            {
                if (this.monthItemFormat != value)
                {
                    this.monthItemFormat = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string WeekItemStartDateFormat
        {
            get
            {
                return this.weekItemStartDateFormat;
            }
            set
            {
                if (this.weekItemStartDateFormat != value)
                {
                    this.weekItemStartDateFormat = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string WeekItemEndDateFormat
        {
            get
            {
                return this.weekItemEndDateFormat;
            }
            set
            {
                if (this.weekItemEndDateFormat != value)
                {
                    this.weekItemEndDateFormat = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string DayItemFormat
        {
            get
            {
                return this.dayItemFormat;
            }
            set
            {
                if (this.dayItemFormat != value)
                {
                    this.dayItemFormat = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string AppointmentItemStartDateFormat
        {
            get
            {
                return this.appointmentItemStartDateFormat;
            }
            set
            {
                if (this.appointmentItemStartDateFormat != value)
                {
                    this.appointmentItemStartDateFormat = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string AppointmentItemEndDateFormat
        {
            get
            {
                return this.appointmentItemEndDateFormat;
            }
            set
            {
                if (this.appointmentItemEndDateFormat != value)
                {
                    this.appointmentItemEndDateFormat = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string AppointmentItemTimeFormat
        {
            get
            {
                return this.appointmentItemTimeFormat;
            }
            set
            {
                if (this.appointmentItemTimeFormat != value)
                {
                    this.appointmentItemTimeFormat = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
