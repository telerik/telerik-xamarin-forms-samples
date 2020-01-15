using System;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;
using QSF.Converters;

namespace QSF.Examples.AccordionControl.ConfigurationExample
{
    public class AccordionConfigurationViewModel : ViewModels.ConfigurationViewModel
    {
        private Color borderColor = NameToColorConverter.LightGray;
        private double borderThickness = 1;
        private bool isAnimationEnabled = true;
        private bool canCollapseAllItems;
        private int animationDuration = 500;
        private Easing animationEasing = Easing.SinIn;
        private ExpandCollapseIndicatorLocation indicatorLocation;
        private Color indicatorColor = NameToColorConverter.Blue;
        private string indicatorText = NameToIndicatorTextConverter.Triangle;
        private double indicatorMargin = 10;
        private double indicatorFontSize = 18;
        private int spacing = 0;

        public Color BorderColor
        {
            get
            {
                return this.borderColor;
            }
            set
            {
                if (this.borderColor != value)
                {
                    this.borderColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double BorderThickness
        {
            get
            {
                return this.borderThickness;
            }
            set
            {
                if (this.borderThickness != value)
                {
                    this.borderThickness = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsAnimationEnabled
        {
            get
            {
                return this.isAnimationEnabled;
            }
            set
            {
                if (this.isAnimationEnabled != value)
                {
                    this.isAnimationEnabled = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool CanCollapseAllItems
        {
            get
            {
                return this.canCollapseAllItems;
            }
            set
            {
                if (this.canCollapseAllItems != value)
                {
                    this.canCollapseAllItems = value;
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

        public ExpandCollapseIndicatorLocation IndicatorLocation
        {
            get
            {
                return this.indicatorLocation;
            }
            set
            {
                if (this.indicatorLocation != value)
                {
                    this.indicatorLocation = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color IndicatorColor
        {
            get
            {
                return this.indicatorColor;
            }
            set
            {
                if (this.indicatorColor != value)
                {
                    this.indicatorColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string IndicatorText
        {
            get
            {
                return this.indicatorText;
            }
            set
            {
                if (this.indicatorText != value)
                {
                    this.indicatorText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double IndicatorFontSize
        {
            get
            {
                return this.indicatorFontSize;
            }
            set
            {
                if (this.indicatorFontSize != value)
                {
                    this.indicatorFontSize = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double IndicatorMargin
        {
            get
            {
                return this.indicatorMargin;
            }
            set
            {
                if (this.indicatorMargin != value)
                {
                    this.indicatorMargin = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int Spacing
        {
            get
            {
                return this.spacing;
            }
            set
            {
                if (this.spacing != value)
                {
                    this.spacing = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
