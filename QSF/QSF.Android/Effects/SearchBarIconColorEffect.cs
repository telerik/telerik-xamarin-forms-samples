using Android.Widget;
using QSF.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(SearchBarIconColorEffect), nameof(SearchBarIconColorEffect))]
namespace QSF.Droid.Effects
{
    public class SearchBarIconColorEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Android.OS.Build.VERSION.SdkInt == Android.OS.BuildVersionCodes.LollipopMr1)
            {
                var searchView = this.Control as SearchView;
                if (searchView != null)
                {
                    var searchIconId = searchView.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                    if (searchIconId > 0)
                    {
                        var searchPlateIcon = searchView.FindViewById(searchIconId);
                        ((ImageView)searchPlateIcon).SetColorFilter(Color.FromHex("767676").ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcIn);
                    }
                    var searchClearButtonId = searchView.Resources.GetIdentifier("android:id/search_close_btn", null, null);
                    if (searchClearButtonId > 0)
                    {
                        var searchClearIcon = searchView.FindViewById(searchClearButtonId);
                        ((ImageView)searchClearIcon).SetColorFilter(Color.FromHex("767676").ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcIn);
                    }
                }
            }
        }

        protected override void OnDetached() { }
    }
}