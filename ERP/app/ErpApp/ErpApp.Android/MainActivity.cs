using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvmCross.Forms.Presenters;

namespace ErpApp.Droid
{
    [Activity(Label = "Telerik ERP", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : MvxFormsAppCompatActivity<ErpAndroidSetup, ErpApplication, App>
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            // The UserAppTheme should be set as well because for some unknown reason the Forms controls do not respect the ModeNightNo.
            // It could be connected with the MVVMCross, because everything is working as expected in other applications.
            Xamarin.Forms.Application.Current.UserAppTheme = Xamarin.Forms.OSAppTheme.Light;
            AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;

            Xamarin.Essentials.Platform.Init(this, bundle);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class ErpAndroidSetup : MvxFormsAndroidSetup<ErpApplication, App>
    {
        protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
        {
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(false);
            return new AppPagePresenter(viewPresenter);
        }
    }
}

