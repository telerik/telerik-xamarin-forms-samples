using System.Linq;
using FFImageLoading.Forms.Platform;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using QSF.Services.BackdoorService;

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

#if __TESTS__
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

        [Export("ChangeToday:")]
        public void ChangeDateTimeToday(string dateString)
        {
            var date = System.DateTime.Parse(dateString).ToNSDate();
            TelerikUI.TKTestUtilities.SetDateTimeNow(date);
        }

        [Export("RestoreToday")]
        public void RestoreToday()
        {
            TelerikUI.TKTestUtilities.RestoreDateTimeNow();
        }

        [Export("SetCalendarDate:")]
        public void SetCalendarDate(string dateString)
        {
            var calendarView = TelerikUI.VisualTreeHelper.FindVisualDescendants
                                        <Telerik.XamarinForms.InputRenderer.iOS.TKExtendedCalendar>(UIApplication.SharedApplication.KeyWindow).FirstOrDefault();
            if (calendarView != null)
            {
                calendarView.NavigateToDate(System.DateTime.Parse(dateString).ToNSDate(), false);
            }
        }
#endif
    }
}
