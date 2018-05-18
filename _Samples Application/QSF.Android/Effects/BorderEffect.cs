using System;
using QSF.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;

[assembly: ResolutionGroupName("TelerikQSF")]
[assembly: ExportEffect(typeof(BorderEffect), nameof(BorderEffect))]
namespace QSF.Droid.Effects
{
    public class BorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var border = new GradientDrawable();
                var darkBackgroundColor = ((Color)App.Current.Resources["DarkBackgroundColor"]).ToAndroid();
                border.SetStroke((int)this.Control.Context.ToPixels(5), Color.Transparent.ToAndroid());
                border.SetCornerRadius(this.Control.Context.ToPixels((float)5));
                border.SetColor(darkBackgroundColor);
                this.Control.Background = border;

                int searchPlateId = this.Control.Context.Resources.GetIdentifier("android:id/search_plate", null, null);

                var searchEdit = this.Control.FindViewById(searchPlateId);
                searchEdit.SetBackgroundColor(Color.Transparent.ToAndroid());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}