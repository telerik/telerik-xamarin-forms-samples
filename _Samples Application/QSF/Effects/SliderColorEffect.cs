using System.Linq;
using Xamarin.Forms;

namespace QSF.Effects
{
    public class SliderColorEffect : RoutingEffect
    {
        public static readonly BindableProperty SliderColorProperty =
            BindableProperty.CreateAttached("SliderColor", typeof(Color),
                typeof(SliderColorEffect), Color.Default, propertyChanged: OnSliderColorChanged);

        public SliderColorEffect()
            : base("TelerikQSF.SliderColorEffect")
        {
        }

        public Color Color { get; set; }

        public static Color GetSliderColor(BindableObject bindable)
        {
            return (Color)bindable.GetValue(SliderColorProperty);
        }

        public static void SetSliderColor(BindableObject bindable, Color color)
        {
            bindable.SetValue(SliderColorProperty, color);
        }

        private static void OnSliderColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (View)bindable;
            var color = (Color)newValue;

            var effect = view.Effects.OfType<SliderColorEffect>().FirstOrDefault();
            if (effect == null)
            {
                effect = new SliderColorEffect();
                view.Effects.Add(effect);
            }

            effect.Color = color;
        }
    }
}
