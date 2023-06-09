using QSF.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Telerik.XamarinForms.Primitives.ProgressBar;
using Xamarin.Forms;

namespace QSF.Examples.ProgressBarControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
        private string trackColor;
        private string progressColor;
        private Easing animationEasing = Easing.Linear;
        private ValueDisplayMode valueDisplayMode = ValueDisplayMode.Percent;
        private string textColor = "Black";
        private bool isVisible;
        private List<string> trackProgressColors;

        public ConfigurationViewModel()
        {
            this.trackProgressColors = new List<string>
            {
                "Orange",
                "Green",
                "Purple",
                "Light Green",
                "Red",
                "Blue",
                "Light Grey",
            };

            this.trackColor = this.trackProgressColors.Last();
            this.progressColor = this.trackProgressColors.First();
        }

        public string TrackColor
        {
            get
            {
                return this.trackColor;
            }
            set
            {
                if (this.trackColor != value)
                {
                    this.trackColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string ProgressColor
        {
            get
            {
                return this.progressColor;
            }
            set
            {
                if (this.progressColor != value)
                {
                    this.progressColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public List<string> TrackProgressColors
        {
            get
            {
                return this.trackProgressColors;
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

        public ValueDisplayMode ValueDisplayMode
        {
            get
            {
                return this.valueDisplayMode;
            }
            set
            {
                if (this.valueDisplayMode != value)
                {
                    this.valueDisplayMode = value;
                    if (this.valueDisplayMode == ValueDisplayMode.Text)
                    {
                        this.IsVisible = true;
                    }
                    else this.IsVisible = false;
                    this.OnPropertyChanged();
                }
            }
        }

        public string TextColor
        {
            get
            {
                return this.textColor;
            }
            set
            {
                if (this.textColor != value)
                {
                    this.textColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }
            set
            {
                if (this.isVisible != value)
                {
                    this.isVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
