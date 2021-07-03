using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace ArtGalleryCRM.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
		protected override void OnCreate (Bundle bundle)
		{
            ToolbarResource = Resource.Layout.Toolbar;
		    TabLayoutResource = Resource.Layout.Tabbar;

            base.OnCreate(bundle);
	    
	    if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    Window.DecorView.SystemUiVisibility = 0;
                    var statusBarHeightInfo = typeof(FormsAppCompatActivity).GetField("_statusBarHeight", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    if (statusBarHeightInfo != null)
                    {
                        statusBarHeightInfo.SetValue(this, 50);
                    }
                }
            
			// Initialize Azure Mobile Apps
			CurrentPlatform.Init();

            // Initializing Xamarin Essentials
            Xamarin.Essentials.Platform.Init(this, bundle);

            //FFImageLoading
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);

            // Initialize Xamarin Forms
            Xamarin.Forms.Forms.Init(this, bundle);

			// Load the main application
			LoadApplication(new ArtGalleryCRM.Forms.App());
		App.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
		    this.Window.AddFlags (WindowManagerFlags.Fullscreen);
		}

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
