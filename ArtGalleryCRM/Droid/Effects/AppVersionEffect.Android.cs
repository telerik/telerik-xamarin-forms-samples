using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Application = Android.App.Application;

[assembly: ResolutionGroupName("Telerik")]
[assembly: ExportEffect(typeof(ArtGalleryCRM.Droid.Effects.AppVersionEffect), "AppVersionEffect")]
namespace ArtGalleryCRM.Droid.Effects
{
    public class AppVersionEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control is TextView tv)
            {
                tv.Text = $"v{Application.Context.ApplicationContext.PackageManager.GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionName}";
            }
        }

        protected override void OnDetached()
        {
        }
    }
}