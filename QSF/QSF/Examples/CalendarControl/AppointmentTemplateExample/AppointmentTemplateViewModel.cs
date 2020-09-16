using QSF.Examples.CalendarControl.SchedulingExample;
using QSF.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.AppointmentTemplateExample
{
    public class AppointmentTemplateViewModel : ExampleViewModel
    {
        private ViewMode selectedMode;

        public AppointmentTemplateViewModel()
        {
            this.CalendarViewModes = new ObservableCollection<ViewMode>();
            this.CalendarViewModes.Add(new ViewMode(char.ConvertFromUtf32(0xe861), CalendarViewMode.Day));
            this.CalendarViewModes.Add(new ViewMode(char.ConvertFromUtf32(0xe862), CalendarViewMode.MultiDay));

            this.Appointments = new ObservableCollection<Appointment>();
            this.LoadAppointments();

            this.SelectedMode = this.CalendarViewModes[0];
        }

        public ObservableCollection<Appointment> Appointments { get; set; }
        public ObservableCollection<ViewMode> CalendarViewModes { get; set; }
        public ViewMode SelectedMode
        {
            get
            {
                return this.selectedMode;
            }
            set
            {
                if (this.selectedMode != value)
                {
                    if (value == null)
                    {
                        return;
                    }

                    this.selectedMode = value;

                    if (selectedMode.CalendarMode == CalendarViewMode.Day)
                    {
                        var imageAppointments = this.Appointments.Where(a => a is ImageAppointment).Cast<ImageAppointment>();
                        foreach (var app in imageAppointments)
                        {
                            app.IsInMultiDayViewMode = false;
                        }
                    }
                    else
                    {
                        var imageAppointments = this.Appointments.Where(a => a is ImageAppointment).Cast<ImageAppointment>();
                        foreach (var app in imageAppointments)
                        {
                            app.IsInMultiDayViewMode = true;
                        }
                    }

                    this.OnPropertyChanged();
                }
            }
        }

        private static ImageSource ConvertImagePathForPlatform(string imagePath)
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                return "Assets/" + imagePath;
            }

            return imagePath;
        }

        private void LoadAppointments()
        {
            var date = DateTime.Today;
            string appointmentDetail = "Details for the event come here - for example place, participants and add.information ...";

            this.Appointments.Add(new IconAppointment
            {
                Title = "Security Training",
                Detail = appointmentDetail,
                StartDate = date.AddHours(12),
                EndDate = date.AddHours(12).AddMinutes(59),
                Color = Color.FromHex("#C1D8FF"),
                TitleTextColor = Color.Black,
                DetailTextColor = Color.FromHex("#5B5D5F"),
                IconSource = char.ConvertFromUtf32(0xe86f)
            });

            this.Appointments.Add(new TextColorAppointment
            {
                Title = "One to One Meeting",
                Detail = appointmentDetail,
                StartDate = date.AddHours(13),
                EndDate = date.AddHours(13).AddMinutes(59),
                Color = Color.FromHex("#FFF1F9"),
                TitleTextColor = Color.Black,
                DetailTextColor = Color.FromHex("#5B5D5F")
            });

            this.Appointments.Add(new IconAppointment
            {
                Title = "Marketing Research Results",
                Detail = appointmentDetail,
                StartDate = date.AddHours(14),
                EndDate = date.AddHours(14).AddMinutes(59),
                Color = Color.FromHex("#3268C3"),
                TitleTextColor = Device.RuntimePlatform == Device.iOS ? Color.Black : Color.White,
                DetailTextColor = Device.RuntimePlatform == Device.iOS ? Color.Black : Color.White,
                IconSource = char.ConvertFromUtf32(0xe870)
            });

            this.Appointments.Add(new ImageAppointment
            {
                Title = "User Interview with VR Team's Client",
                Detail = appointmentDetail,
                StartDate = date.AddHours(15),
                EndDate = date.AddHours(15).AddMinutes(59),
                Color = Color.FromHex("#EDFDE3"),
                TitleTextColor = Color.Black,
                IsRightAlign = false,
                Source = ConvertImagePathForPlatform("DataGrid_SalesPerson_1.png")
            });

            this.Appointments.Add(new ImageAppointment
            {
                Title = "Cake Time with Xamarin Team",
                Detail = "Location: 601",
                StartDate = date.AddHours(16),
                EndDate = date.AddHours(16).AddMinutes(59),
                Color = Color.FromHex("#FDE2AC"),
                TitleTextColor = Color.Black,
                Source = ConvertImagePathForPlatform("coffee.png"),
                IsRightAlign = true
            });

            this.Appointments.Add(new TextColorAppointment
            {
                Title = "Meeting - R1 Key Points",
                StartDate = date.AddHours(17),
                EndDate = date.AddHours(17).AddMinutes(30),
                Color = Color.FromHex("#154391"),
                TitleTextColor = Device.RuntimePlatform == Device.iOS ? Color.Black : Color.White
            });

            this.Appointments.Add(new IconAppointment
            {
                Title = "Career Talk",
                Detail = "Discuss career goals",
                StartDate = date.AddDays(1).AddHours(14),
                EndDate = date.AddDays(1).AddHours(14).AddMinutes(59),
                Color = Color.FromHex("#D9DFFF"),
                TitleTextColor = Color.Black,
                DetailTextColor = Color.FromHex("#5B5D5F"),
                IconSource = char.ConvertFromUtf32(0xe871)
            });

            this.Appointments.Add(new TextColorAppointment
            {
                Title = "PDF Viewer Demo Discussion",
                Detail = "Features check with PM",
                StartDate = date.AddDays(1).AddHours(15),
                EndDate = date.AddDays(1).AddHours(15).AddMinutes(59),
                Color = Color.FromHex("#C1D8FF"),
                TitleTextColor = Color.Black,
                DetailTextColor = Color.FromHex("#5B5D5F")
            });

            this.Appointments.Add(new TextColorAppointment
            {
                Title = "Conference Call",
                StartDate = date.AddDays(2).AddHours(13),
                EndDate = date.AddDays(2).AddHours(13).AddMinutes(59),
                Color = Color.FromHex("#EDFDE3"),
                TitleTextColor = Color.Black
            });

            this.Appointments.Add(new TextColorAppointment
            {
                Title = "Iteration Planning",
                StartDate = date.AddDays(2).AddHours(16),
                EndDate = date.AddDays(2).AddHours(16).AddMinutes(59),
                Color = Color.FromHex("#FFF1F9"),
                TitleTextColor = Color.Black
            });
        }
    }
}