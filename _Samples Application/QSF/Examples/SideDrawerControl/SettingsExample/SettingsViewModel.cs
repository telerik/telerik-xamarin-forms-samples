using System;
using System.Collections.ObjectModel;
using QSF.ViewModels;
using Telerik.XamarinForms.Primitives;
using Telerik.XamarinForms.Primitives.SideDrawer;
using Xamarin.Forms;

namespace QSF.Examples.SideDrawerControl.SettingsExample
{
    public class SettingsViewModel : ViewModelBase
    {
        private SideDrawerLocation selectedLocation;
        private SideDrawerTransitionType selectedTransition;

        public SideDrawerLocation SelectedLocation
        {
            get
            {
                return this.selectedLocation;
            }
            set
            {
                if (this.selectedLocation != value)
                {
                    this.selectedLocation = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public SideDrawerTransitionType SelectedTransition
        {
            get
            {
                return this.selectedTransition;
            }
            set
            {
                if (this.selectedTransition != value)
                {
                    this.selectedTransition = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> Items { get; private set; }
        public ObservableCollection<SideDrawerTransitionType> Transitions { get; private set; }
        public Command LeftCommand { get; private set; }
        public Command TopCommand { get; private set; }
        public Command RightCommand { get; private set; }
        public Command BottomCommand { get; private set; }

        public SettingsViewModel()
        {
            this.Items = new ObservableCollection<string>
            {
                "Home",
                "About",
                "Settings",
                "User profile"
            };
            this.Transitions = new ObservableCollection<SideDrawerTransitionType>()
            {
                SideDrawerTransitionType.SlideAlong,
                SideDrawerTransitionType.SlideInOnTop,
                SideDrawerTransitionType.Push,
                SideDrawerTransitionType.Reveal,
                SideDrawerTransitionType.ReverseSlideOut,
                SideDrawerTransitionType.ScaleUp
            };
            this.LeftCommand = new Command(this.OnLeft);
            this.TopCommand = new Command(this.OnTop);
            this.RightCommand = new Command(this.OnRight);
            this.BottomCommand = new Command(this.OnBottom);
        }

        private void OnLeft()
        {
            this.SelectedLocation = SideDrawerLocation.Left;
        }

        private void OnTop()
        {
            this.SelectedLocation = SideDrawerLocation.Top;
        }

        private void OnRight()
        {
            this.SelectedLocation = SideDrawerLocation.Right;
        }

        private void OnBottom()
        {
            this.SelectedLocation = SideDrawerLocation.Bottom;
        }
    }
}
