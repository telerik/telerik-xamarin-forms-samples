using Android.App;
using Android.Content.PM;
using Android.OS;
using FFImageLoading.Forms.Droid;
using Java.Interop;
using QSF.Droid.Permissions;
using QSF.Services.BackdoorService;
using Xamarin.Forms;

namespace QSF.Droid
{
    [Activity(Label = "QSF", Theme = "@style/Theme.AppCompat.NoActionBar", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder.Build());

            base.OnCreate(bundle);

            PermissionsHelper.Activity = this;

            this.SetTheme(Resource.Style.Theme_Design_Light);

            CachedImageRenderer.Init(false);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsHelper.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        [Export("NavigateToExample")]
        public void NavigateToExample(string examplePath)
        {
            IBackdoorService backdoorService = DependencyService.Get<IBackdoorService>();
            backdoorService.NavigateToExample(examplePath);
        }

        [Export("NavigateToHome")]
        public void NavigateToHome()
        {
            IBackdoorService backdoorService = DependencyService.Get<IBackdoorService>();
            backdoorService.NavigateToHome();
        }
    }
}

