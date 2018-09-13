using Xamarin.Forms;

namespace QSF.Examples.AutoCompleteViewControl.RemoteSearchExample
{
    public class Genre
    {
        public Genre(Color color, string name)
        {
            this.Color = color;
            this.Name = name;
        }

        public Color Color { get; set; }

        public string Name { get; set;}
    }
}
