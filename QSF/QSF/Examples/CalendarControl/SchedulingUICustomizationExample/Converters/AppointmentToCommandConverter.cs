using System;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.SchedulingUICustomizationExample
{
    public class AppointmentToCommandConverter : IValueConverter
    {
        private CustomAppointment appointment;

        public AppointmentToCommandConverter()
        {
            this.SaveAppointmentCommand = new Command(this.OnSaveAppointmentCommandExecute);
        }

        public ICommand SaveAppointmentCommand { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            this.appointment = (CustomAppointment)value;
            return this.SaveAppointmentCommand;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.appointment;
        }

        private async void OnSaveAppointmentCommandExecute(object obj)
        {
            if (string.IsNullOrEmpty(this.appointment.Title) || this.appointment.Location == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Title and Location are mandatory fields.", "OK");
            }
            else
            {
                var defaultCommand = (Command)obj;
                defaultCommand.Execute(null);
            }
        }
    }
}
