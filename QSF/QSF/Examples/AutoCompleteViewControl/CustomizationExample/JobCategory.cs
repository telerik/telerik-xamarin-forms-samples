using Xamarin.Forms;

namespace QSF.Examples.AutoCompleteViewControl.CustomizationExample
{
    public class JobCategory
    {
        public JobCategory(Color borderFill, Color textColor, string icon, string category)
        {
            this.BorderFill = borderFill;
            this.TextColor = textColor;
            this.Icon = icon;
            this.Category = category;
        }

        public Color BorderFill { get; set; }

        public Color TextColor { get; set; }

        public string Icon { get; set; }

        public string Category { get; set; }
    }
}