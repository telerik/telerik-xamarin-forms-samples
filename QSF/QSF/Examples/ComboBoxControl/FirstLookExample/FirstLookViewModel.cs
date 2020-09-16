using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.ComboBoxControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private int selectedSkillIndex = -1;
        private int selectedJobTypeIndex = -1;
        private int selectedTimeIndex = -1;
        private bool isJobSearchNotificationOpen;
        private CancellationTokenSource cancellation;

        public FirstLookViewModel()
        {
            this.Skills = new ObservableCollection<string>()
            {
                "Xamarin",
                "Blazor",
                "WPF",
                "UWP",
                "Fiddler",
                "Sitefinity",
                "Test Studio",
                "Open Edge",
                "Kinvey",
                "DataRPM",
                "Corticon"
            };

            this.JobTypes = new ObservableCollection<string>()
            {
                "Full-time",
                "Part-time",
                "Internship",
                "Temporary"
            };

            this.Times = new ObservableCollection<string>()
            {
                "Posted Any Time",
                "Last Day",
                "Last 3 Days",
                "Last Week",
                "Last 2 Weeks"
            };

            this.SearchJobButtonCommand = new Command(this.OnSearchJobButtonCommandExecuted, this.OnSearchJobButtonCommandCanExecute);

            this.cancellation = new CancellationTokenSource();
        }

        public ObservableCollection<string> Skills { get; set; }
        public ObservableCollection<string> JobTypes { get; set; }
        public ObservableCollection<string> Times { get; set; }
        public ICommand SearchJobButtonCommand { get; set; }

        public int SelectedSkillIndex
        {
            get
            {
                return this.selectedSkillIndex;
            }
            set
            {
                if (this.selectedSkillIndex != value)
                {
                    this.selectedSkillIndex = value;
                    ((Command)this.SearchJobButtonCommand).ChangeCanExecute();
                    this.OnPropertyChanged();
                }
            }
        }

        public int SelectedJobTypeIndex
        {
            get
            {
                return this.selectedJobTypeIndex;
            }
            set
            {
                if (this.selectedJobTypeIndex != value)
                {
                    this.selectedJobTypeIndex = value;
                    ((Command)this.SearchJobButtonCommand).ChangeCanExecute();
                    this.OnPropertyChanged();
                }
            }
        }

        public int SelectedTimeIndex
        {
            get
            {
                return this.selectedTimeIndex;
            }
            set
            {
                if (this.selectedTimeIndex != value)
                {
                    this.selectedTimeIndex = value;
                    ((Command)this.SearchJobButtonCommand).ChangeCanExecute();
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsJobSearchNotificationOpen
        {
            get
            {
                return this.isJobSearchNotificationOpen;
            }
            set
            {
                if (this.isJobSearchNotificationOpen != value)
                {
                    this.isJobSearchNotificationOpen = value;
                    if (!this.isJobSearchNotificationOpen)
                    {
                        Interlocked.Exchange(ref this.cancellation, new CancellationTokenSource()).Cancel();
                    }

                    this.OnPropertyChanged();
                }
            }
        }

        private void OnSearchJobButtonCommandExecuted(object obj)
        {
            if (this.isJobSearchNotificationOpen)
            {
                return;
            }

            CancellationTokenSource cts = this.cancellation;
            this.IsJobSearchNotificationOpen = true;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                if (cts.IsCancellationRequested)
                {
                    return false;
                }

                this.IsJobSearchNotificationOpen = false;
                return false;
            });
        }

        private bool OnSearchJobButtonCommandCanExecute(object arg)
        {
            return this.selectedSkillIndex != -1
                && this.selectedJobTypeIndex != -1
                && this.selectedTimeIndex != -1;
        }
    }
}
