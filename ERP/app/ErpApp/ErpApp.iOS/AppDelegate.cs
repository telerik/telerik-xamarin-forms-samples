using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.Forms.Presenters;
using UIKit;

namespace ErpApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate<ErpIosSetup, ErpApplication, App>
    {
    }

    public class ErpIosSetup : MvxFormsIosSetup<ErpApplication, App>
    {
        protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
        {
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            return new AppPagePresenter(viewPresenter);
        }
    }
}
