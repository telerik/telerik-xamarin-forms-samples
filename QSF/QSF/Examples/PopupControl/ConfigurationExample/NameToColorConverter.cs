using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace QSF.Examples.PopupControl.ConfigurationExample
{
    public class NameToColorConverter : IValueConverter
    {
        public static readonly Color LightGray = Color.FromHex("#6FD9D9D9");
        public static readonly Color DarkGray = Color.FromHex("#6F000000");
        public static readonly Color Pink = Color.FromHex("#6FEF2648");
        public static readonly Color Blue = Color.FromHex("#6F3d5AFE");
        public static readonly Color Transparent = Color.Transparent;

        private static readonly string LightGrayColorName = "Light Grey";
        private static readonly string TransparentColorName = "Transparent";
        private static readonly string DarkGrayColorName = "Dark Grey";
        private static readonly string PinkColorName = "Pink";
        private static readonly string BlueColorName = "Blue";

        private static readonly Dictionary<string, Color> nameToColor;
        private static readonly Dictionary<Color, string> colorToName;

        static NameToColorConverter()
        {
            nameToColor = new Dictionary<string, Color>();
            colorToName = new Dictionary<Color, string>();

            AddPair(TransparentColorName, Transparent);
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
