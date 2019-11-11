using FreshMvvm;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace TodoApp.PageModels
{
    public class DateTimeInputPageModel : FreshBasePageModelEx
    {
        public DateTimeInputPageModel()
        {
            _acceptCommand = new Command(OnAccept);
        }

        private Command _acceptCommand;
        private bool _isAllDay;

        public ICommand AcceptCommand => _acceptCommand;

        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool IsAllDay
        {
            get => _isAllDay;
            set
            {
                if (SetProperty(ref _isAllDay, value))
                    RaisePropertyChanged(nameof(CanSetEndDateTime));
            }
        }
        public bool CanSetEndDateTime => !_isAllDay;

        public override void Init(object initData)
        {
            if (initData is Model model)
            {
                this.StartDate = model.StartDate.Date;
                this.StartTime = model.StartDate.TimeOfDay;

                DateTime endDate = model.StartDate.Add(model.Duration);
                this.EndDate = endDate.Date;
                this.EndTime = endDate.TimeOfDay;
                this.IsAllDay = model.AllDay;
            }
        }

        private async void OnAccept()
        {
            DateTime start = this.StartDate.Add(this.StartTime);
            DateTime end = this.EndDate.Add(this.EndTime);
            if (!this.IsAllDay && start > end)
            {
                await CoreMethods.DisplayAlert("Validation error", "End date and time cannot be before start date and time", "OK");
                return;
            }

            Model model = this.IsAllDay ? Model.CreateAllDay(this.StartDate) :
                Model.CreateDuration(this.StartDate, this.StartTime, this.EndDate, this.EndTime);
            await CoreMethods.PopPageModel(model);
        }

        public class Model
        {
            public Model(DateTime startDate, TimeSpan duration, bool allDay)
            {
                this.StartDate = startDate;
                this.Duration = duration;
                this.AllDay = allDay;
            }

            public bool AllDay { get; set; }
            public DateTime StartDate { get; private set; }
            public TimeSpan Duration { get; private set; }

            public static Model CreateAllDay(DateTime start)
            {
                return new Model(start.Date, TimeSpan.FromDays(1), true);
            }

            public static Model CreateDuration(DateTime startDate, TimeSpan startTime, DateTime endDate, TimeSpan endTime)
            {
                DateTime start = startDate.Add(startTime);
                DateTime end = endDate.Add(endTime);
                TimeSpan duration = end - start;
                return new Model(start, duration, false);
            }
        }
    }
}
