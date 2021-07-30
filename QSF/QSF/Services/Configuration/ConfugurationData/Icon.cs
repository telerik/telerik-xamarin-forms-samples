using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Services.Configuration
{
    public class Icon : BindableBase
    {
        private string lightColor;
        private string darkColor;
        private Color themeColor = Color.Default;

        public Icon()
        {
            if (Device.RuntimePlatform != Device.Android)
            {
                Application.Current.RequestedThemeChanged += (sender, args) => this.UpdateThemeColor();
            }
        }

        public string Text { get; set; }

        public string LightColor
        {
            get
            {
                return this.lightColor;
            }
            set
            {
                if (this.lightColor != value)
                {
                    this.lightColor = value;
                    this.UpdateThemeColor();
                }
            }
        }

        public string DarkColor
        {
            get
            {
                return this.darkColor;
            }
            set
            {
                if (this.darkColor != value)
                {
                    this.darkColor = value;
                    this.UpdateThemeColor();
                }
            }
        }

        public Color ThemeColor
        {
            get
            {
                return this.themeColor;
            }
            set
            {
                if (this.themeColor != value)
                {
                    this.themeColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private void UpdateThemeColor()
        {
            if (this.lightColor == null || this.darkColor == null)
            {
                return;
            }

            this.ThemeColor = Application.Current.RequestedTheme != OSAppTheme.Dark
               ? Color.FromHex(this.lightColor)
               : Color.FromHex(this.darkColor);
        }
    }
}
