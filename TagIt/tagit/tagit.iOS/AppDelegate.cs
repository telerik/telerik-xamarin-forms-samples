using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Platform;
using Foundation;
using UIKit;

namespace tagit.iOS
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
            CachedImageRenderer.Init();
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            UIColor accentColor = UIColor.FromRGB(0, 151, 223);

            UISlider.Appearance.TintColor = accentColor;
            UISlider.Appearance.ThumbTintColor = accentColor;

            UISwitch.Appearance.TintColor = accentColor;
            UISwitch.Appearance.OnTintColor = accentColor;

            UITabBar.Appearance.TintColor = accentColor;
            UITabBar.Appearance.SelectedImageTintColor = accentColor;

            UINavigationBar.Appearance.BarTintColor = accentColor;
            UINavigationBar.Appearance.TintColor = UIColor.White;
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White });

            UIToolbar.Appearance.BackgroundColor = accentColor;
            UIToolbar.Appearance.TintColor = UIColor.White;

            return base.FinishedLaunching(app, options);
        }
    }
}
