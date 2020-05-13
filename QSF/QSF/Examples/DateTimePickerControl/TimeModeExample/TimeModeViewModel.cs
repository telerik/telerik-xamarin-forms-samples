using QSF.Services.Toast;
using QSF.ViewModels;
using System;
using Xamarin.Forms;

namespace QSF.Examples.DateTimePickerControl.TimeModeExample
{
    public class TimeModeViewModel: ExampleViewModel
    {
        private const string displayStringFormatMask = "h:mm tt";
        private DateTime? startTime;
        private DateTime? endTime;

        public TimeModeViewModel()
        {
            this.StartTime = null;
            this.EndTime = null;
            this.SetHeatingTimeCommand = new Command(this.SetHeatingTime, this.CanSetHeatingTime);
        }

        public DateTime? StartTime
        {
            get
            {
                return this.startTime;
            }
            set
            {
                if (this.startTime != value)
                {
                    this.startTime = value;
                    this.OnPropertyChanged();
                    this.SetHeatingTimeCommand.ChangeCanExecute();
                }    
            }
        }

        public DateTime? EndTime
        {
            get
            {
                return this.endTime;
            }
            set
            {
                if (this.endTime != value)
                {
                    this.endTime = value;
                    this.OnPropertyChanged();
                    this.SetHeatingTimeCommand.ChangeCanExecute();
                }
            }
        }

        public Command SetHeatingTimeCommand { get; }

        private void SetHeatingTime()
        {
            string startTimeDisplayString = this.StartTime.Value.ToString(displayStringFormatMask);
            string endTimeDisplayString = this.EndTime.Value.ToString(displayStringFormatMask);

            var toastMessage = DependencyService.Get<IToastMessageService>();
            toastMessage.ShortAlert($"Heating time period: {startTimeDisplayString} - {endTimeDisplayString} set.");

            this.StartTime = null;
            this.EndTime = null;
        }

        private bool CanSetHeatingTime()
        {
            return this.StartTime != null && this.EndTime != null;
        }
    }
}
