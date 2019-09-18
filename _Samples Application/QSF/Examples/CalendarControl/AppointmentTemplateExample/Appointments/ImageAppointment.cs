using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.AppointmentTemplateExample
{
    public class ImageAppointment : TextColorAppointment
    {
        public ImageSource Source { get; set; }

        public bool IsRightAlign { get; set; }

        public bool IsInMultiDayViewMode { get; set; }
    }
}
