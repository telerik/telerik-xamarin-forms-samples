using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.ExpanderControl.ConfigurationExample
{
    public class NameToColorConverter : IValueConverter
    {
        public static readonly Color LightGray = Color.FromHex("#D9D9D9");
        public static readonly Color DarkGray = Color.FromHex("#A8AAAD");
        public static readonly Color Pink = Color.FromHex("#EF2648");
        public static readonly Color Blue = Color.FromHex("#3d5AFE");

        private static readonly string LightGrayColorName = "Light Grey";
        private static readonly string DarkGrayColorName = "Dark Grey";
        private static readonly string PinkColorName = "Pink";
        private static readonly string BlueColorName = "Blue";

        private static readonly Dictionary<string, Color> nameToColor;
        private static readonly Dictionary<Color, string> colorToName;

        static NameToColorConverter()
        {
            nameToColor = new Dictionary<string, Color>();
            colorToName = new Dictionary<Color, string>();

            AddPair(LightGrayColorName, LightGray);
            AddPair(DarkGrayColorName, DarkGray);
            AddPair(PinkColorName, Pink);
            AddPair(BlueColorName, Blue);
        }

        private static void AddPair(string name, Color color)
        {
            nameToColor.Add(name, color);
            colorToName.Add(color, name);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = (Color)value;

            return colorToName[color];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string name = (string)value;

            return nameToColor[name];
        }
    }
}
