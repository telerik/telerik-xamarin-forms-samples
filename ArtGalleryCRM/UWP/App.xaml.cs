using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Metadata;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ArtGalleryCRM.Uwp
{
    sealed partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }
        
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (!(Window.Current.Content is Frame rootFrame))
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                //FFImageLoading
                FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

                var config = new FFImageLoading.Config.Configuration
                {
                    VerboseLogging = false,
                    VerbosePerformanceLogging = false,
                    VerboseMemoryCacheLogging = false,
                    VerboseLoadingCancelledLogging = false,
                };
                FFImageLoading.ImageService.Instance.Initialize(config);
                
                var assembliesToInclude = new List<Assembly>
                {
                    typeof(FFImageLoading.Forms.CachedImage).GetTypeInfo().Assembly,
                    typeof(FFImageLoading.Forms.Platform.CachedImageRenderer).GetTypeInfo().Assembly,
                    typeof(ArtGalleryCRM.Forms.Common.CustomThemeStyles).GetTypeInfo().Assembly,
                    typeof(Telerik.XamarinForms.ConversationalUI.RadChat).GetTypeInfo().Assembly,
                    typeof(Telerik.XamarinForms.ConversationalUIRenderer.UWP.ChatListViewRenderer).GetTypeInfo().Assembly,
                    typeof(Telerik.XamarinForms.ConversationalUIRenderer.UWP.CardActionViewRenderer).GetTypeInfo().Assembly,
                    typeof(Telerik.XamarinForms.Input.RadButton).GetTypeInfo().Assembly,
                    typeof(Telerik.XamarinForms.InputRenderer.UWP.ButtonRenderer).GetTypeInfo().Assembly,
                    typeof(Telerik.XamarinForms.Primitives.RadBorder).GetTypeInfo().Assembly,
                    typeof(Telerik.XamarinForms.PrimitivesRenderer.UWP.BorderRenderer).GetTypeInfo().Assembly,
                };

                // Ensure FFImageLoading isn't optimized out by .NET Native toolchain
                Xamarin.Forms.Forms.Init(e, assembliesToInclude);

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }
                
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }

                Window.Current.Activate();
            }
            
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                await StatusBar.GetForCurrentView().HideAsync();
            }
        }
        
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }
        
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}
