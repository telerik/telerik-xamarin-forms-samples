using Telerik.XamarinForms.Input;

namespace QSF.Examples.CalendarControl.SchedulingUICustomizationExample
{
    public class CustomAppointment : Appointment
    {
        private Priority priority;
        private OfficeRoom location;
        private Alert alert;

        public CustomAppointment()
        {
            this.Priority = Priority.Low;
            this.Alert = Alert.None;
        }

        public Priority Priority
        {
            get
            {
                return this.priority;
            }
            set
            {
                if (value != this.priority)
                {
                    this.priority = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public OfficeRoom Location
        {
            get
            {
                return this.location;
            }
            set
            {
                if (!value.Equals(this.location))
                {
                    this.location = value;
                    this.Color = this.location.Color;
                    this.OnPropertyChanged();
                }
            }
        }

        public Alert Alert
        {
            get
            {
                return this.alert;
            }
            set
            {
                if (value != this.alert)
                {
                    this.alert = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public override Appointment Copy()
        {
            CustomAppointment newApp = new CustomAppointment();
            newApp.CopyFrom(this);
            return newApp;
        }

        public override void CopyFrom(Appointment other)
        {
            if (other is CustomAppointment customAppointment)
            {
                this.Alert = customAppointment.Alert;
                this.Priority = customAppointment.Priority;
                this.Location = customAppointment.Location;
            }

            base.CopyFrom(other);
        }
    }
}
