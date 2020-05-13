using QSF.Services.Toast;
using QSF.ViewModels;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using XF = Xamarin.Forms;

namespace QSF.Examples.DateTimePickerControl.DateModeExample
{
    public class DateModeViewModel : ExampleViewModel
    {
        private readonly string formatMask = "MMM d, yyyy";
        private DateTime? selectedDate;
        private string displaySessionsDate;

        public DateModeViewModel()
        {
            this.SelectedDate = null;
            this.DisplaySessionsDate = "Apr 6, 2020";
            this.Sessions = new ObservableCollection<Session>()
            {
                new Session("9.00 - 9.30 GMT | S6308:", "Keynote: Creativity is the Great Enabler", XF.Color.FromHex("#985DC4")),
                new Session("10.00 - 11.30 GMT | S6618:", "How to Enter the Field of Motion Design", XF.Color.FromHex("#FF6F00")),
                new Session("12:00 - 13.30 GMT | S6324:", "Animation for UX: Using motion with Meaning in UI Design", XF.Color.FromHex("#0E88F2")),
                new Session("11:00 - 12.30 GMT | S6508:", "Design systems made more doable", XF.Color.FromHex("#985DC4")),
                new Session("15:00 - 16.30 GMT | S6723:", "7 Ways to Analyze a Customer-Journey Map", XF.Color.FromHex("#FF6F00"))
            };
            this.ShowSessionsCommand = new Command(this.ShowSessions, this.CanShowSessions);
        }

        public string FormatMask
        {
            get
            {
                return this.formatMask;
            }
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
                    this.ShowSessionsCommand.ChangeCanExecute();
                }
            }
        }

        public string DisplaySessionsDate
        {
            get
            {
                return this.displaySessionsDate;
            }
            set
            {
                if (this.displaySessionsDate != value)
                {
                    this.displaySessionsDate = value;
                    this.OnPropertyChanged();
                }
            }

        }

        public ObservableCollection<Session> Sessions { get; }

        public Command ShowSessionsCommand { get; }

        private void ShowSessions()
        {
            if (this.Sessions.Count == 5)
            {
                for (int i = 0; i < 3; i++)
                {
                    this.Sessions.RemoveAt(0);
                }
            }
            string selectedDateDisplayString = this.SelectedDate.Value.ToString(this.FormatMask);
            var toastMessage = DependencyService.Get<IToastMessageService>();
            toastMessage.ShortAlert($"Sessions for: {selectedDateDisplayString} displayed.");
            this.DisplaySessionsDate = selectedDateDisplayString;
        }

        private bool CanShowSessions()
        {
            return this.SelectedDate != null;
        }
    }
}
