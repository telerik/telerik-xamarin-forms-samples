using QSF.Services.Toast;
using QSF.ViewModels;
using System;
using Xamarin.Forms;

namespace QSF.Examples.TimePickerControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private DateTime? selectedDate;
        private TimeSpan? selectedTime;

        public FirstLookViewModel()
        {
            this.SendOrderCommand = new Command(this.SendOrder, this.CanSendOrder);
            this.SelectedDate = null;
            this.SelectedTime = null;
        } 
        
        public DateTime? SelectedDate
        {
            get
            {
                return this.selectedDate;
            }
            set
            {
                if (this.selectedDate != value)
                {
                    this.selectedDate = value;
                    this.OnPropertyChanged();
                    this.SendOrderCommand.ChangeCanExecute();
                }
            }
        }

        public TimeSpan? SelectedTime
        {
            get
            {
                return this.selectedTime;
            }
            set
            {
                if (this.selectedTime != value)
                {
                    this.selectedTime = value;
                    this.OnPropertyChanged();
                    this.SendOrderCommand.ChangeCanExecute();
                }
            }
        }

        public Command SendOrderCommand { get; }

        public void SendOrder()
        {
            var toastMessage = DependencyService.Get<IToastMessageService>();
            toastMessage.ShortAlert("Order sent");
            this.SelectedDate = null;
            this.SelectedTime = null;
        }

        private bool CanSendOrder()
        {
            return this.SelectedDate != null && this.SelectedTime != null;
        }
    }
}
