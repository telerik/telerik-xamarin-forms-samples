using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.ShadowControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
        private string color = "Black";
        private double shadowOpacity = 0.17;
        private double offsetX;
        private double offsetY = 4;
        private double blurRadius = Device.RuntimePlatform == Device.iOS ? 2 : 6;
        private double cornerRadius = 20;

        public string Color
        {
            get
            {
                return this.color;
            }
            set
            {
                if (this.color != value)
                {
                    this.color = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double ShadowOpacity
        {
            get
            {
                return this.shadowOpacity;
            }
            set
            {
                if (this.shadowOpacity != value)
                {
                    this.shadowOpacity = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double OffsetX
        {
            get
            {
                return this.offsetX;
            }
            set
            {
                if (this.offsetX != value)
                {
                    this.offsetX = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double OffsetY
        {
            get
            {
                return this.offsetY;
            }
            set
            {
                if (this.offsetY != value)
                {
                    this.offsetY = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double BlurRadius
        {
            get
            {
                return this.blurRadius;
            }
            set
            {
                if (this.blurRadius != value)
                {
                    this.blurRadius = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double CornerRadius
        {
            get
            {
                return this.cornerRadius;
            }
            set
            {
                if (this.cornerRadius != value)
                {
                    this.cornerRadius = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
