using System.Linq;
using Xamarin.Forms;

namespace QSF.Effects
{
    public class StrikeThroughEffect : RoutingEffect
    {
        public static readonly BindableProperty IsStrikeThroughProperty =
            BindableProperty.CreateAttached("IsStrikeThrough", typeof(bool),
                typeof(StrikeThroughEffect), false, propertyChanged: OnStrikeThroughChanged);

        public StrikeThroughEffect()
            : base("TelerikQSF.StrikeThroughEffect")
        {
        }

        public static bool GetIsStrikeThrough(BindableObject bindable)
        {
            return (bool)bindable.GetValue(IsStrikeThroughProperty);
        }

        public static void SetIsStrikeThrough(BindableObject bindable, bool value)
        {
            bindable.SetValue(IsStrikeThroughProperty, value);
        }

        private static void OnStrikeThroughChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;

            if (view != null)
            {
                if ((bool)newValue)
                {
                    var effect = new StrikeThroughEffect();

                    view.Effects.Add(effect);
                }
                else
                {
                    var effect = view.Effects
                        .OfType<StrikeThroughEffect>()
                        .FirstOrDefault();

                    if (effect != null)
                    {
                        view.Effects.Remove(effect);
                    }
                }
            }
        }
    }
}
