using QSF.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace QSF.Examples.SignaturePadControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
        private Color selectedStrokeColor;
        private Color selectedBorderColor;

        private double strokeThickness = 2.0d;
        private Thickness borderThickness = new Thickness(2);

        public ConfigurationViewModel()
        {
            var colors = new List<Color>()
            {
                Color.FromHex("#0E88F2"),
                Color.FromHex("#CE3A6D"),
                Color.FromHex("#000000"),
                Color.FromHex("#EE6C4D"),
                Color.FromHex("#5F5F5F")
            };

            this.BorderColors = this.StrokeColors = colors;

            this.SelectedBorderColor = this.BorderColors.Last();
            this.SelectedStrokeColor = this.StrokeColors.First();
        }

        public IList<Color> StrokeColors { get; }
        public IList<Color> BorderColors { get; }

        public Color SelectedStrokeColor
        {
            get
            {
                return this.selectedStrokeColor;
            }
            set
            {
                if (this.selectedStrokeColor != value)
                {
                    this.selectedStrokeColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Color SelectedBorderColor
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

        public double StrokeThickness
        {
            get
            {
                return this.strokeThickness;
            }
            set
            {
                if (this.strokeThickness != value)
                {
                    this.strokeThickness = value;
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
