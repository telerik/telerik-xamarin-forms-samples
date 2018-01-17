using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;

namespace QSF.Droid
{
    [Activity(Theme = "@style/Theme.Splash", Icon = "@drawable/Logo", MainLauncher = true, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var intent = new Intent(this, typeof(MainActivity));
            this.StartActivity(intent);
            this.Finish();
        }
    }
}