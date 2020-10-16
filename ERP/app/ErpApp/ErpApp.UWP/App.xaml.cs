using MvvmCross.Forms.Platforms.Uap.Core;
using MvvmCross.Forms.Platforms.Uap.Views;
using MvvmCross.Forms.Presenters;
using System;
using System.Collections.Generic;
using System.Reflection;
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

        public override IEnumerable<Assembly> GetViewAssemblies()
        {
            var assemblies = (List<Assembly>)base.GetViewAssemblies();
            assemblies.Add(typeof(Telerik.XamarinForms.Input.RadButton).GetTypeInfo().Assembly);
            assemblies.Add(typeof(Telerik.XamarinForms.InputRenderer.UWP.ButtonRenderer).GetTypeInfo().Assembly);
            assemblies.Add(typeof(Telerik.XamarinForms.Primitives.RadBorder).GetTypeInfo().Assembly);
            assemblies.Add(typeof(Telerik.XamarinForms.PrimitivesRenderer.UWP.BorderRenderer).GetTypeInfo().Assembly);

            return assemblies;
        }
    }
}
