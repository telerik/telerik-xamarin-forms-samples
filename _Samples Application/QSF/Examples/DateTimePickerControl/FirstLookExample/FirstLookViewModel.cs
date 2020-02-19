using QSF.ViewModels;
using System;
using System.Collections.ObjectModel;

namespace QSF.Examples.DateTimePickerControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private DateTime selectedDate;

        public FirstLookViewModel()
        {
            this.Data = new ObservableCollection<TripData>
            {
                new TripData{Date = new DateTime(2020,02,18), Time = new DateTime(2020,02,18,17,26,20), Cost = "$29"},
                new TripData{Date = new DateTime(2020,01,8), Time = new DateTime(2020,01,8,15,21,21), Cost = "$24"},
                new TripData{Date = new DateTime(2019,10,28), Time = new DateTime(2019,10,28,7,15,15), Cost = "$8"},
                new TripData{Date = new DateTime(2019,09,28), Time = new DateTime(2019,09,28,15,43,43), Cost = "$21"},
                new TripData{Date = new DateTime(2019,09,15), Time = new DateTime(2019,09,15,6,12,12), Cost = "$33"},
            };

            this.SelectedDate = DateTime.Today;
        }

        public DateTime SelectedDate
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
                }
            }
        }

        public ObservableCollection<TripData> Data { get; set; }
    }
}
