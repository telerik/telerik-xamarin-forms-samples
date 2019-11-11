using System.Collections.Generic;
using System.Linq;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.BorderControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
		private string selectedBorderColor;

        private double leftCornerRadius = 75;
        private double topCornerRadius = 75;
        private double rightCornerRadius = 75;
        private double bottomCornerRadius = 75;

        private string leftCornerRadiusLabelText;
        private string topCornerRadiusLabelText;
        private string rightCornerRadiusLabelText;
        private string bottomCornerRadiusLabelText;

        private Thickness borderCornerRadius;
        private Thickness borderThickness = new Thickness(15);

		public ConfigurationViewModel()
        {
            this.BorderColors = new List<string>
                {
                    "Light Grey",
                    "Dark Grey",
                    "Pink",
                    "Blue"
                };

            this.AvatarImage = "Border_Configuration_Avatar.png";
            this.SelectedBorderColor = this.BorderColors.First();
        }

        public string AvatarImage { get; set; }

        public IList<string> BorderColors { get; set; }

        public string SelectedBorderColor
        {
            get
            {
                return this.selectedBorderColor;
            }
            set
            {
                if (this.selectedBorderColor != value)
                {
                    this.selectedBorderColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double LeftCornerRadius
        {
            get
            {
                return this.leftCornerRadius;
            }
            set
            {
                if (this.leftCornerRadius != value)
                {
                    this.leftCornerRadius = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double TopCornerRadius
        {
            get
            {
                return this.topCornerRadius;
            }
            set
            {
                if (this.topCornerRadius != value)
                {
                    this.topCornerRadius = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double RightCornerRadius
        {
            get
            {
                return this.rightCornerRadius;
            }
            set
            {
                if (this.rightCornerRadius != value)
                {
                    this.rightCornerRadius = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double BottomCornerRadius
        {
            get
            {
                return this.bottomCornerRadius;
            }
            set
            {
                if (this.bottomCornerRadius != value)
                {
                    this.bottomCornerRadius = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string LeftCornerRadiusLabelText
        {
            get
            {
                return this.leftCornerRadiusLabelText;
            }
            set
            {
                if (this.leftCornerRadiusLabelText != value)
                {
                    this.leftCornerRadiusLabelText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string TopCornerRadiusLabelText
        {
            get
            {
                return this.topCornerRadiusLabelText;
            }
            set
            {
                if (this.topCornerRadiusLabelText != value)
                {
                    this.topCornerRadiusLabelText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string RightCornerRadiusLabelText
        {
            get
            {
                return this.rightCornerRadiusLabelText;
            }
            set
            {
                if (this.rightCornerRadiusLabelText != value)
                {
                    this.rightCornerRadiusLabelText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string BottomCornerRadiusLabelText
        {
            get
            {
                return this.bottomCornerRadiusLabelText;
            }
            set
            {
                if (this.bottomCornerRadiusLabelText != value)
                {
                    this.bottomCornerRadiusLabelText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Thickness BorderCornerRadius
        {
            get
            {
                return this.borderCornerRadius;
            }
            set
            {
                if (this.borderCornerRadius != value)
                {
                    this.borderCornerRadius = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Thickness BorderThickness
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
    }
}
