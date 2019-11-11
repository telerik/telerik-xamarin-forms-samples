using MvvmCross.Forms.Platforms.Uap.Core;
using MvvmCross.Forms.Platforms.Uap.Views;
using MvvmCross.Forms.Presenters;
using System;
using Windows.ApplicationModel.Activation;

namespace ErpApp.UWP
{
    public sealed partial class App : UwpErpApp
    {
        public App()
        { 
            this.InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs activationArgs)
        {
            base.OnLaunched(activationArgs);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
        }
    }

    public abstract class UwpErpApp : MvxWindowsApplication<ErpWindowsSetup, ErpApplication, ErpApp.App>
    {
        protected override Type HostWindowsPageType()
        {
            return typeof(MainPage);
        }
    }

    public class ErpWindowsSetup : MvxFormsWindowsSetup<ErpApplication, ErpApp.App>
    {
        protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
        {
            return new AppPagePresenter(viewPresenter);
        }
    }
}
