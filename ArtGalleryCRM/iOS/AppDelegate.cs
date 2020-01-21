using System;
using System.Runtime.InteropServices;
using Foundation;
using ObjCRuntime;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace ArtGalleryCRM.iOS
{
    [Register ("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
            // Initialize Azure Mobile Apps
		    Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

		    //FFImageLoading
		    FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

            // Initialize Xamarin Forms
            Xamarin.Forms.Forms.Init();

			LoadApplication(new ArtGalleryCRM.Forms.App());

		    UIApplication.SharedApplication.StatusBarHidden = true;
            
            return base.FinishedLaunching(app, options);
		}
    }
}