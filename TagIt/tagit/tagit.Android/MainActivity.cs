using Android.App;
using Android.Content.PM;
using Android.OS;
using FFImageLoading.Forms.Platform;

namespace tagit.Droid
{
    [Activity(Label = "Telerik Tagit", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            tagit.Droid.Permissions.PermissionsHelper.Activity = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            base.OnCreate(bundle);

            CachedImageRenderer.Init(true);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            this.LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            tagit.Droid.Permissions.PermissionsHelper.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}