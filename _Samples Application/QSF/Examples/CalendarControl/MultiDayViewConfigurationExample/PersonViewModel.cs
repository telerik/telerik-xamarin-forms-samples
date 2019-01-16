using System.Collections.ObjectModel;
using QSF.ViewModels;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.MultiDayViewConfigurationExample
{
    public class PersonViewModel : BindableBase
    {
        private string name;
        private Color color;

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

        public Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                if (this.color != value)
                {
                    this.color = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Appointment> Appointments { get; private set; }

        public PersonViewModel()
        {
            this.Appointments = new ObservableCollection<Appointment>();
        }
    }
}
