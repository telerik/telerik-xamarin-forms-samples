namespace ErpApp.Serialization
{
    public static class Utils
    {
        public static System.Drawing.Color ToColor(Xamarin.Forms.Color color)
        {
            return System.Drawing.Color.FromArgb(((int)color.A * 255),
                                                 ((int)color.R * 255),
                                                 ((int)color.G * 255),
                                                 ((int)color.B * 255));
        }

        public static Xamarin.Forms.Color ToColor(System.Drawing.Color color)
        {
            return Xamarin.Forms.Color.FromRgba(color.R, color.G, color.B, color.A);
        }

        public static Xamarin.Forms.Color ToColor(int argb)
        {
            System.Drawing.Color sdcolor = System.Drawing.Color.FromArgb(argb);
            return ToColor(sdcolor);
        }
    }
}
