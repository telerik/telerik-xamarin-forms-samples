using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.AppointmentTemplateExample
{
    public class AppointmentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate AppointmentTemplate { get; set; }
        public DataTemplate IconTemplate { get; set; }
        public DataTemplate LeftAlingImageTemplate { get; set; }
        public DataTemplate RightAlignImageTemplate { get; set; }
        public DataTemplate ImageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is IconAppointment)
            {
                return this.IconTemplate;
            }

            if (item is ImageAppointment imageAppointment)
            {
                if (imageAppointment.IsInMultiDayViewMode)
                {
                    return this.ImageTemplate;
                }

                if (imageAppointment.IsRightAlign)
                {
                    return this.RightAlignImageTemplate;
                }

                return this.LeftAlingImageTemplate;
            }

            if (item is TextColorAppointment)
            {
                return this.AppointmentTemplate;
            }

            return null;
        }
    }
}
