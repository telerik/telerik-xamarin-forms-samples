using FFImageLoading.Forms.Touch;
using Foundation;
using QSF.Services.BackdoorService;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace QSF.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            AnalyticsHelper.Initialize(Xamarin.Forms.Device.iOS);

            global::Xamarin.Forms.Forms.Init();

            CachedImageRenderer.Init();

            LoadApplication(new App());

            UITextField.Appearance.TintColor = Xamarin.Forms.Color.FromHex("#2548D8").ToUIColor();
#if __TESTS__
            Xamarin.Calabash.Start();
#endif
            return base.FinishedLaunching(app, options);
        }

        [Export("NavigateToExample:")]
        public NSString NavigateToExample(NSString value)
        {
            string param = value.ToString();
            IBackdoorService backdoorService = DependencyService.Get<IBackdoorService>();
            backdoorService.NavigateToExample(param);

            return new NSString();
        }

        [Export("NavigateToHome:")]
        public NSString NavigateToHome(NSString value)
        {
            IBackdoorService backdoorService = DependencyService.Get<IBackdoorService>();
            backdoorService.NavigateToHome();

            return new NSString();
        }
    }
}
