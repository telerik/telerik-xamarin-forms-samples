using System;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;

namespace QSF.Examples.PopupControl.ConfigurationExample
{
    public class PopupConfigurationViewModel : ViewModels.ConfigurationViewModel
    {
        private PlacementMode placement;
        private double horizontalOffset;
        private double verticalOffset;
        private bool isModal;
        private Color outsideBackgroundColor = Color.Transparent;
        private int animationDuration = 300;
        private Easing animationEasing = Easing.Linear;
        private PopupAnimationType animationType = PopupAnimationType.Fade;
        private PlacementMode oldPlacement;

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
                    this.OnIsModalChanged();
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

        private void OnIsModalChanged()
        {
            if (this.IsModal)
            {
                this.oldPlacement = this.Placement;
                this.Placement = PlacementMode.Center;
            }
            else
            {
                this.Placement = this.oldPlacement;
            }
        }
    }
}
