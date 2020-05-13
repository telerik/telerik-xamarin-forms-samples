using QSF.Examples.TimePickerControl.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Telerik.XamarinForms.Common;
using Xamarin.Forms;

namespace QSF.Examples.TimePickerControl.CustomizationExample
{
    public class ViewModel : NotifyPropertyChangedBase
    {
        private ObservableCollection<Alarm> alarms;
        private ICommand alarmEditCanceled, alarmAccepted;

        public ViewModel()
        {
            this.Alarms = new ObservableCollection<Alarm>
            {
                new Alarm() {SelectedHour = new TimeSpan(6, 0, 0) },
                new Alarm() {SelectedHour = new TimeSpan(7, 0, 0) },
                new Alarm() {SelectedHour = new TimeSpan(8, 0, 0), IsEnabled = true },
            };
            this.AddAlarmCommand = new Command(this.AddAlarm);
            this.AlarmEditCanceled = new Command(this.OnAlarmEditCanceled);
            this.AlarmAccepted = new Command(this.OnAlarmAccepted);
        }

        public ICommand AlarmEditCanceled
        {
            get
            {
                return this.alarmEditCanceled;
            }
            set
            {
                if (this.alarmEditCanceled != value)
                {
                    this.alarmEditCanceled = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand AlarmAccepted
        {
            get
            {
                return this.alarmAccepted;
            }
            set
            {
                if (this.alarmAccepted != value)
                {
                    this.alarmAccepted = value;
                    this.OnPropertyChanged();
                }
            }
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
            Alarm lastAlarm = this.Alarms.Last();
            var nAlarm = new Alarm()
            {
                SelectedHour = lastAlarm.SelectedHour,
                Name = lastAlarm.Name,
                IsEnabled = lastAlarm.IsEnabled
            };
            this.Alarms.Add(nAlarm);
            nAlarm.IsPickerOpened = true;
        }

        private void OnAlarmAccepted()
        {
            this.Alarms.Last().IsCustomized = true;
        }

        private void OnAlarmEditCanceled()
        {
            if (!this.Alarms.Last().IsCustomized)
            {
                this.Alarms.Remove(this.Alarms.Last());
            }
        }
    }
}
