using QSF.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;

namespace QSF.Examples.PopupControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
        private const string ConfigurationMessage = "To configure the Popup please use the icon in the navigation bar";

        private PlacementMode placement;
        private double horizontalOffset;
        private double verticalOffset;
        private bool isModal;
        private Color outsideBackgroundColor = Color.Transparent;
        private int animationDuration = 300;
        private Easing animationEasing = Easing.Linear;
        private PopupAnimationType animationType = PopupAnimationType.Fade;
        private PopupConfigurationViewModel configurationViewModel;
        private bool showErrorMessage;
        private string messageText;
        private bool isFirstStart = true;

        public event EventHandler<EventArgs> OpenPopup;
        public event EventHandler<EventArgs> ClosePopup;

        public ConfigurationViewModel()
        {
            this.configurationViewModel = new PopupConfigurationViewModel();
            this.MessageText = ConfigurationMessage;
            this.ShowPopupCommand = new Command(p => this.OpenPopup?.Invoke(this, EventArgs.Empty));
            this.ClosePopupCommand = new Command(p => this.ClosePopup?.Invoke(this, EventArgs.Empty));
        }

        public override bool HasConfiguration
        {
            get
            {
                return true;
            }
        }

        public bool ShowErrorMessage
        {
            get
            {
                return this.showErrorMessage;
            }
            set
            {
                if (this.showErrorMessage != value)
                {
                    this.showErrorMessage = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string MessageText
        {
            get
            {
                return this.messageText;
            }
            set
            {
                if (this.messageText != value)
                {
                    this.messageText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand ShowPopupCommand { get; private set; }

        public ICommand ClosePopupCommand { get; private set; }

        public PlacementMode Placement
        {
            get
            {
                return this.placement;
            }
            set
            {
                if (this.placement != value)
                {
                    this.placement = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double HorizontalOffset
        {
            get
            {
                return this.horizontalOffset;
            }
            set
            {
                if (this.horizontalOffset != value)
                {
                    this.horizontalOffset = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double VerticalOffset
        {
            get
            {
                return this.verticalOffset;
            }
            set
            {
                if (this.verticalOffset != value)
                {
                    this.verticalOffset = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsModal
        {
            get
            {
                return this.isModal;
            }
            set
            {
                if (this.isModal != value)
                {
                    this.isModal = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color OutsideBackgroundColor
        {
            get
            {
                return this.outsideBackgroundColor;
            }
            set
            {
                if (this.outsideBackgroundColor != value)
                {
                    this.outsideBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int AnimationDuration
        {
            get
            {
                return this.animationDuration;
            }
            set
            {
                if (this.animationDuration != value)
                {
                    this.animationDuration = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Easing AnimationEasing
        {
            get
            {
                return this.animationEasing;
            }
            set
            {
                if (this.animationEasing != value)
                {
                    this.animationEasing = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public PopupAnimationType AnimationType
        {
            get
            {
                return this.animationType;
            }
            set
            {
                if (this.animationType != value)
                {
                    this.animationType = value;
                    this.OnPropertyChanged();
                }
            }
        }

        internal override void OnAppearing()
        {
            base.OnAppearing();

            if (this.isFirstStart)
            {
                return;
            }

            this.Placement = this.configurationViewModel.Placement;
            this.HorizontalOffset = this.configurationViewModel.HorizontalOffset;
            this.VerticalOffset = this.configurationViewModel.VerticalOffset;
            this.IsModal = this.configurationViewModel.IsModal;
            this.OutsideBackgroundColor = this.configurationViewModel.OutsideBackgroundColor;
            this.AnimationDuration = this.configurationViewModel.AnimationDuration;
            this.AnimationEasing = this.configurationViewModel.AnimationEasing;
            this.AnimationType = this.configurationViewModel.AnimationType;
        }

        internal override void OnDisappearing()
        {
            base.OnDisappearing();

            this.isFirstStart = false;
        }

        protected override Task NavigateToConfigurationOverride()
        {
            this.ShowErrorMessage = false;
            this.MessageText = ConfigurationMessage;
            return this.NavigationService.NavigateToConfigurationAsync(this.configurationViewModel);
        }
    }
}
