using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.SchedulingUICustomizationExample
{
    public class AppointmentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var appointment = item as IAppointment;
            if (appointment.IsAllDay)
            {
                return null;
            }

            return this.DefaultTemplate;
        }
    }
}
