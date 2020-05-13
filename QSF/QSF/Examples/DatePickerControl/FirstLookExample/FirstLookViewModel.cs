using QSF.Services.Toast;
using QSF.ViewModels;
using System;
using Xamarin.Forms;

namespace QSF.Examples.DatePickerControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private DateTime? startDate;
        private DateTime? endDate;

        public FirstLookViewModel()
        {
            this.SendRequestCommand = new Command(this.SendRequest, this.CanSendRequest);
            this.StartDate = null;
            this.EndDate = null;
        }

        public DateTime? StartDate
        {
            get
            {
                return this.startDate;
            }
            set
            {
                if (this.startDate != value)
                {
                    this.startDate = value;
                    this.OnPropertyChanged();
                    this.OnStartDateChanged();
                }
            }
        }

        public DateTime? EndDate
        {
            get
            {
                return this.endDate;
            }
            set
            {
                if (this.endDate != value)
                {
                    this.endDate = value;
                    this.OnPropertyChanged();
                    this.OnEndDateChanged();
                }
            }
        }

        public Command SendRequestCommand { get; }

        private void OnStartDateChanged()
        {
            if (this.EndDate < this.StartDate)
            {
                this.EndDate = this.StartDate;
            }
            this.SendRequestCommand.ChangeCanExecute();
        }

        private void OnEndDateChanged()
        {
            if (this.StartDate > this.EndDate)
            {
                this.StartDate = this.EndDate;
            }
            this.SendRequestCommand.ChangeCanExecute();
        }

        private void SendRequest()
        {
            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert("Request sent");
            this.StartDate = null;
            this.EndDate = null;
        }

        private bool CanSendRequest()
        {
            return this.StartDate != null && this.EndDate != null;
        }
    }
}
