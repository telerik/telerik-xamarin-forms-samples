using System.Collections.ObjectModel;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.MultiDayViewPeopleExample
{
    public class PersonViewModel : BindableBase
    {
        private string name;
        private Color color;
        private bool isSelected;

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

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<AppointmentViewModel> Appointments { get; private set; }

        public PersonViewModel()
        {
            this.Appointments = new ObservableCollection<AppointmentViewModel>();
        }
    }
}
