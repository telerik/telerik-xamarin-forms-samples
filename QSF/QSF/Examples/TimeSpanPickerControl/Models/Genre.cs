using Xamarin.Forms;

namespace QSF.Examples.TimeSpanPickerControl.Models
{
    public class Genre
    {
        public Genre(string name, string color) : this(name, Color.FromHex(color))
        {

        }

        public Genre(string name, Color color)
        {
            this.Name = name;
            this.Color = color;
        }

        public string Name { get; }

        public Color Color { get; }
    }
}
