using Xamarin.Forms;

namespace QSF.Examples.ListPickerControl.FirstLookExample
{
    public class Music
    {
        public Music(string artist, string song, Color iconColor)
        {
            this.Artist = artist;
            this.Song = song;
            this.IconColor = iconColor;
        }

        public string Artist { get; set; }

        public string Song { get; set; }

        public Color IconColor { get; set; }
    }

}
