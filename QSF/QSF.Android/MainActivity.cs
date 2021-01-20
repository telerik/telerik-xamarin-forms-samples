using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;
using Java.Interop;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using QSF.Services.BackdoorService;
using Xamarin.Forms;

namespace QSF.Droid
{
    [Activity(Label = "QSF", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            // If these are not used the ToString for DateTime throws an exception in some cultures
            // For more information: https://forums.xamarin.com/discussion/42899/datetime-tostring-throws-argumentoutofrangeexception-in-thai-locale
            var c1 = new System.Globalization.ChineseLunisolarCalendar();
            var c2 = new System.Globalization.GregorianCalendar();
            var c3 = new System.Globalization.HebrewCalendar();
            var c4 = new System.Globalization.HijriCalendar();
            var c5 = new System.Globalization.JapaneseCalendar();
            var c6 = new System.Globalization.JapaneseLunisolarCalendar();
            var c7 = new System.Globalization.JulianCalendar();
            var c8 = new System.Globalization.KoreanCalendar();
            var c9 = new System.Globalization.KoreanLunisolarCalendar();
            var c10 = new System.Globalization.PersianCalendar();
            var c11 = new System.Globalization.TaiwanCalendar();
            var c12 = new System.Globalization.TaiwanLunisolarCalendar();
            var c13 = new System.Globalization.ThaiBuddhistCalendar();
            var c14 = new System.Globalization.UmAlQuraCalendar();

            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder.Build());

            base.OnCreate(bundle);

            Xamarin.Essentials.Platform.Init(this, bundle);
            CrossCurrentActivity.Current.Init(this, bundle);

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(false);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            this.LoadApplication(new App());

            Xamarin.Forms.Application.Current.RequestedThemeChanged += this.OnRequestedThemeChanged;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
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

        private void OnRequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            Xamarin.Forms.Application.Current.RequestedThemeChanged -= this.OnRequestedThemeChanged;

            var theme = Xamarin.Forms.Application.Current.UserAppTheme;
            if (theme == OSAppTheme.Unspecified)
            {
                AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightUnspecified;
            }
            else
            {
                AppCompatDelegate.DefaultNightMode = theme == OSAppTheme.Dark ? AppCompatDelegate.ModeNightYes : AppCompatDelegate.ModeNightNo;
            }
        }
    }
}

