using Android.App;
using Android.Content.PM;
using Android.OS;

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

