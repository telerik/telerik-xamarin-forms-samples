using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using QSF.ViewModels;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.AgendaViewConfigurationExample
{
    public class AgendaViewConfigurationViewModel : ExampleViewModel
    {
        private ConfigurationViewModel configurationViewModel;
        private string monthItemFormat;
        private string weekItemStartDateFormat;
        private string weekItemEndDateFormat;
        private string dayItemFormat;
        private string appointmentItemStartDateFormat;
        private string appointmentItemEndDateFormat;
        private string appointmentItemTimeFormat;
        private DateTime displayDate;
        private List<Color> appColors = new List<Color>();

        public AgendaViewConfigurationViewModel()
        {
            this.configurationViewModel = new ConfigurationViewModel();
            this.appointmentItemStartDateFormat = "dd MMMM";
            this.appointmentItemEndDateFormat = "dd MMMM";
            this.weekItemEndDateFormat = "d";
            this.monthItemFormat = "MMMM yyyy";
            this.weekItemStartDateFormat = "MMMM d";
            this.dayItemFormat = "EEEE, d MMMM";

            this.DisplayDate = DateTime.Now;

            if (Device.RuntimePlatform == Device.iOS)
            {
                this.appointmentItemTimeFormat = "HH:mm";
                this.appColors.Add(Color.FromHex("FF9F55"));
                this.appColors.Add(Color.FromHex("ABEBF5"));
                this.appColors.Add(Color.FromHex("007AFF"));
                this.appColors.Add(Color.FromHex("A2BDF8"));
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                this.appointmentItemTimeFormat = "h:mm a";
                this.appColors.Add(Color.FromHex("4DFF9F55"));
                this.appColors.Add(Color.FromHex("4DABEBF5"));
                this.appColors.Add(Color.FromHex("4D007AFF"));
                this.appColors.Add(Color.FromHex("4DA2BDF8"));
            }

            this.Appointments = this.GenerateAppointments();
            this.NavigateToTodayCommand = new Command(this.OnNavigateToTodayCommandExecute);
        }

        public ObservableCollection<IAppointment> Appointments { get; set; }
        public ICommand NavigateToTodayCommand { get; set; }

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

        public DateTime DisplayDate
        {
            get
            {
                return this.displayDate;
            }
            set
            {
                if (this.displayDate != value)
                {
                    this.displayDate = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public override bool HasConfiguration
        {
            get
            {
                return true;
            }
        }

        public override bool IsPopupHintOpen => true;

        private ObservableCollection<IAppointment> GenerateAppointments()
        {
            var appointments = new ObservableCollection<IAppointment>()
            {
                new Appointment()
                {
                    StartDate = DateTime.Today.AddHours(11),
                    EndDate = DateTime.Today.AddHours(11).AddMinutes(15),
                    Title = "Daily SCRUM",
                    Color = this.appColors[0],
                    RecurrenceRule = new RecurrenceRule(new RecurrencePattern()
                    {
                        Frequency = RecurrenceFrequency.Weekly,
                        MaxOccurrences = 60,
                        DaysOfWeekMask = RecurrenceDays.WeekDays
                    })
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddDays(-7).AddHours(10),
                    EndDate = DateTime.Today.AddDays(-7).AddHours(11),
                    Title = "Tokyo Deall call",
                    Color = this.appColors[1]
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddDays(-7).AddHours(16) ,
                    EndDate = DateTime.Today.AddDays(-7).AddHours(17).AddMinutes(30),
                    Title = "Dinner with the Morgans",
                    Color = this.appColors[2]
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddDays(-4).AddHours(15),
                    EndDate = DateTime.Today.AddDays(-4).AddHours(16).AddMinutes(30),
                    Title = "Theater evening",
                    Color = this.appColors[3]
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddDays(-3).AddHours(9),
                    EndDate = DateTime.Today.AddDays(-3).AddHours(10),
                    Title = "Conference call with HQ2",
                    Color = this.appColors[0]
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddDays(-4),
                    EndDate = DateTime.Today.AddDays(-2).AddSeconds(1),
                    Title = "Weekend barbecue",
                    Color = this.appColors[1],
                    IsAllDay= true,
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddDays(-3),
                    EndDate = DateTime.Today.AddDays(-1).AddSeconds(1),
                    Title = "Mountain biking",
                    Color = this.appColors[3],
                    IsAllDay = true
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddHours(9),
                    EndDate = DateTime.Today.AddHours(10),
                    Title = "Job Interview",
                    Color = this.appColors[2]

                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddHours(10),
                    EndDate = DateTime.Today.AddHours(11),
                    Title = "Tokyo deal call",
                    Color = this.appColors[1]
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddHours(14),
                    EndDate = DateTime.Today.AddHours(15).AddMinutes(30),
                    Title = "Yachting",
                    Color = this.appColors[2],
                    IsAllDay = true
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddHours(16),
                    EndDate = DateTime.Today.AddHours(17).AddMinutes(30),
                    Title = "Dinner with the Morgans",
                    Color = this.appColors[3]
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddHours(18),
                    EndDate = DateTime.Today.AddHours(19).AddMinutes(30),
                    Title = "Fitness",
                    Color = this.appColors[0]
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddHours(20),
                    EndDate = DateTime.Today.AddHours(22),
                    Title = "Watch a movie",
                    Color = this.appColors[3],
                    IsAllDay = true
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddHours(20),
                    EndDate = DateTime.Today.AddHours(22),
                    Title = "Date with Candice",
                    Color = this.appColors[1]
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddDays(2).AddHours(18),
                    EndDate = DateTime.Today.AddDays(2).AddHours(19).AddMinutes(30),
                    Title = "Watch your favourite show",
                    Color = this.appColors[0],
                    IsAllDay = true,
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddDays(2).AddHours(19),
                    EndDate = DateTime.Today.AddDays(2).AddHours(20).AddMinutes(30),
                    Title = "Football",
                    Color = this.appColors[2]
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddDays(3).AddHours(10),
                    EndDate = DateTime.Today.AddDays(3).AddHours(11),
                    Title = "Coordination meeting",
                    Color = this.appColors[1]
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddDays(3).AddHours(15),
                    EndDate = DateTime.Today.AddDays(3).AddHours(16).AddMinutes(30),
                    Title = "Theater evening",
                    Color = this.appColors[2],
                    IsAllDay = true
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddDays(3).AddHours(18),
                    EndDate = DateTime.Today.AddDays(3).AddHours(19).AddMinutes(30),
                    Title = "Table tennis",
                    Color = this.appColors[0]
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddDays(4).AddHours(9),
                    EndDate = DateTime.Today.AddDays(4).AddHours(10),
                    Title = "Conference call with HQ2",
                    Color = this.appColors[3]
                },
                new Appointment()
                {
                    StartDate = DateTime.Today.AddDays(5).AddHours(21),
                    EndDate = DateTime.Today.AddDays(5).AddHours(23),
                    Title = "Birthday party",
                    Color = this.appColors[0]
                }
            };

            return appointments;
        }

        internal override void OnAppearing()
        {
            base.OnAppearing();

            this.MonthItemFormat = this.configurationViewModel.MonthItemFormat;
            this.WeekItemStartDateFormat = this.configurationViewModel.WeekItemStartDateFormat;
            this.WeekItemEndDateFormat = this.configurationViewModel.WeekItemEndDateFormat;
            this.DayItemFormat = this.configurationViewModel.DayItemFormat;
            this.AppointmentItemStartDateFormat = this.configurationViewModel.AppointmentItemStartDateFormat;
            this.AppointmentItemEndDateFormat = this.configurationViewModel.AppointmentItemEndDateFormat;
            this.AppointmentItemTimeFormat = this.configurationViewModel.AppointmentItemTimeFormat;
        }

        protected override Task NavigateToConfigurationOverride()
        {
            return this.NavigationService.NavigateToConfigurationAsync(this.configurationViewModel);
        }


        private void OnNavigateToTodayCommandExecute(object obj)
        {
            this.DisplayDate = DateTime.Now.Date;
        }
    }
}
