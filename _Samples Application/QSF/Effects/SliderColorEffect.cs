using Xamarin.Forms;

namespace QSF.Effects
{
    public class SliderColorEffect : RoutingEffect
    {
        public SliderColorEffect()
            : base("TelerikQSF.SliderColorEffect")
        {
        }

        public Color Color { get; set; }
    }
}
