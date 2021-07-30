using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;

namespace QSF.Examples.BadgeViewControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
        private string badgeBackgroundColor = "Red";
        private string badgeText = "2";
        private string badgeTextColor = "White";
        private BadgePosition badgeHorizontalPosition = BadgePosition.End;
        private BadgePosition badgeVerticalPosition = BadgePosition.Start;
        private BadgeAlignment badgeHorizontalAlignment = BadgeAlignment.End;
        private BadgeAlignment badgeVerticalAlignment = BadgeAlignment.Center;
        private double offSetX = 0;
        private double offSetY = 0;
        private double textMarginLeft = 5;
        private double textMarginTop = 0;
        private double textMarginRight = 5;
        private double textMarginBottom = 0;
        private BadgeAnimationType badgeAnimationType = BadgeAnimationType.Scale;
        private Easing badgeAnimationEasing = Easing.SinInOut;
        private double badgeAnimationDuration = 300;

        public string BadgeBackgroundColor
        {
            get
            {
                return this.badgeBackgroundColor;
            }
            set
            {
                if (this.badgeBackgroundColor != value)
                {
                    this.badgeBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string BadgeTextColor
        {
            get
            {
                return this.badgeTextColor;
            }
            set
            {
                if (this.badgeTextColor != value)
                {
                    this.badgeTextColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string BadgeText
        {
            get
            {
                return this.badgeText;
            }
            set
            {
                if (this.badgeText != value)
                {
                    this.badgeText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public BadgePosition BadgeHorizontalPosition
        {
            get
            {
                return this.badgeHorizontalPosition;
            }
            set
            {
                if (this.badgeHorizontalPosition != value)
                {

                    this.badgeHorizontalPosition = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public BadgePosition BadgeVerticalPosition
        {
            get
            {
                return this.badgeVerticalPosition;
            }
            set
            {
                if (this.badgeVerticalPosition != value)
                {

                    this.badgeVerticalPosition = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public BadgeAlignment BadgeHorizontalAlignment
        {
            get
            {
                return this.badgeHorizontalAlignment;
            }
            set
            {
                if (this.badgeHorizontalAlignment != value)
                {

                    this.badgeHorizontalAlignment = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public BadgeAlignment BadgeVerticalAlignment
        {
            get
            {
                return this.badgeVerticalAlignment;
            }
            set
            {
                if (this.badgeVerticalAlignment != value)
                {

                    this.badgeVerticalAlignment = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Array BadgePositions
        {
            get
            {
                return Enum.GetValues(typeof(BadgePosition));
            }
        }

        public Array BadgeAlignments
        {
            get
            {
                return Enum.GetValues(typeof(BadgeAlignment));
            }
        }

        public double OffSetX
        {
            get
            {
                return this.offSetX;
            }
            set
            {
                if (this.offSetX != value)
                {
                    this.offSetX = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double OffSetY
        {
            get
            {
                return this.offSetY;
            }
            set
            {
                if (this.offSetY != value)
                {
                    this.offSetY = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double TextMarginLeft
        {
            get
            {
                return this.textMarginLeft;
            }
            set
            {
                if (this.textMarginLeft != value)
                {
                    this.textMarginLeft = value;
                    this.OnPropertyChanged(nameof(BadgeTextMargin));
                }
            }
        }

        public double TextMarginTop
        {
            get
            {
                return this.textMarginTop;
            }
            set
            {
                if (this.textMarginTop != value)
                {
                    this.textMarginTop = value;
                    this.OnPropertyChanged(nameof(BadgeTextMargin));
                }
            }
        }

        public double TextMarginRight
        {
            get
            {
                return this.textMarginRight;
            }
            set
            {
                if (this.textMarginRight != value)
                {
                    this.textMarginRight = value;
                    this.OnPropertyChanged(nameof(BadgeTextMargin));
                }
            }
        }

        public double TextMarginBottom
        {
            get
            {
                return this.textMarginBottom;
            }
            set
            {
                if (this.textMarginBottom != value)
                {
                    this.textMarginBottom = value;
                    this.OnPropertyChanged(nameof(BadgeTextMargin));
                }
            }
        }

        public Thickness BadgeTextMargin
        {
            get
            {
                return new Thickness(this.TextMarginLeft, this.TextMarginTop, this.TextMarginRight, this.TextMarginBottom);
            }
        }

        public BadgeAnimationType BadgeAnimationType
        {
            get
            {
                return this.badgeAnimationType;
            }
            set
            {
                if (this.badgeAnimationType != value)
                {
                    this.badgeAnimationType = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Array BadgeAnimationTypes
        {
            get
            {
                return Enum.GetValues(typeof(BadgeAnimationType));
            }
        }

        public Easing BadgeAnimationEasing
        {
            get
            {
                return this.badgeAnimationEasing;
            }
            set
            {
                if (this.badgeAnimationEasing != value)
                {
                    this.badgeAnimationEasing = value;
                    this.BadgeStartAnimationCommand.Execute(null);
                    this.OnPropertyChanged();
                }
            }
        }

        public double BadgeAnimationDuration
        {
            get
            {
                return this.badgeAnimationDuration;
            }
            set
            {
                if (this.badgeAnimationDuration != value)
                {
                    this.badgeAnimationDuration = value;
                    this.BadgeStartAnimationCommand.Execute(null);
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand BadgeStartAnimationCommand { get; set; }
    }
}
