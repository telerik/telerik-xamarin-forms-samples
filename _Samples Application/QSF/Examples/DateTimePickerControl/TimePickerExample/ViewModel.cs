using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.XamarinForms.Common;
using Xamarin.Forms;

namespace QSF.Examples.DateTimePickerControl.TimePickerExample
{
    public class ViewModel : NotifyPropertyChangedBase
    {
        private ObservableCollection<Alarm> alarms;

        public ViewModel()
        {
            DateTime date = DateTime.Today;
            this.Alarms = new ObservableCollection<Alarm>
            {
                new Alarm() { SelectedHour = new DateTime(date.Year, date.Month, date.Day, 6, 0, 0) },
                new Alarm() { SelectedHour = new DateTime(date.Year, date.Month, date.Day, 7, 0, 0) },
                new Alarm() { SelectedHour = new DateTime(date.Year, date.Month, date.Day, 8, 0, 0), IsEnabled = true }
            };
            this.AddAlarmCommand = new Command(this.AddAlarm);
        }

        public ObservableCollection<Alarm> Alarms
        {
            get
            {
                return this.alarms;
            }
            set
            {
                if (this.alarms != value)
                {
                    this.alarms = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand AddAlarmCommand { get; set; }

        private void AddAlarm()
        {
            Alarm lastAlarm = this.Alarms[this.Alarms.Count - 1];
            var nAlarm = new Alarm()
            {
                SelectedHour = lastAlarm.SelectedHour,
                Name = lastAlarm.Name,
                IsEnabled = lastAlarm.IsEnabled
            };
            this.Alarms.Add(nAlarm);
            nAlarm.IsPickerOpened = true;
        }
    }
}
