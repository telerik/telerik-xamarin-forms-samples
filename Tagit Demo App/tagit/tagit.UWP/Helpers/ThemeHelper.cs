using System;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace tagit.UWP.Helpers
{
    public static class ThemeHelper
    {
        public static async void InitializeUiAsync()
        {
            try
            {
                // If we have a phone contract, hide the status bar
                if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
                {
                    var statusBar = StatusBar.GetForCurrentView();
                    await statusBar.HideAsync();
                }
            }
            catch { }
            
        }

        public static void UpdateSystemAssets()
        {
            InitializeUiAsync();

            try
            {
                var accentColor = (Color) Application.Current.Resources["AppMainBlueColor"];

                UpdateTitleBar(accentColor);

                Application.Current.Resources["SystemAccentColor"] = accentColor;
                ((SolidColorBrush) Application.Current.Resources["SystemControlHighlightAccentBrush"]).Color =
                    accentColor;
                ((SolidColorBrush) Application.Current.Resources["SystemControlBackgroundAccentBrush"]).Color =
                    accentColor;
                ((SolidColorBrush) Application.Current.Resources["SystemControlDisabledAccentBrush"]).Color =
                    accentColor;
                ((SolidColorBrush) Application.Current.Resources["SystemControlForegroundAccentBrush"]).Color =
                    accentColor;
                ((SolidColorBrush) Application.Current.Resources["SystemControlHighlightAccentBrush"]).Color =
                    accentColor;
                ((SolidColorBrush) Application.Current.Resources["SystemControlHighlightAltAccentBrush"]).Color =
                    accentColor;
                ((SolidColorBrush) Application.Current.Resources["SystemControlHighlightAltListAccentHighBrush"])
                    .Color = accentColor;
                ((SolidColorBrush) Application.Current.Resources["SystemControlHighlightAltListAccentLowBrush"]).Color =
                    accentColor;
                ((SolidColorBrush) Application.Current.Resources["SystemControlHighlightAltListAccentMediumBrush"])
                    .Color = accentColor;
                ((SolidColorBrush) Application.Current.Resources["SystemControlHighlightListAccentHighBrush"]).Color =
                    accentColor;
                ((SolidColorBrush) Application.Current.Resources["SystemControlHighlightListAccentLowBrush"]).Color =
                    accentColor;
                ((SolidColorBrush) Application.Current.Resources["SystemControlHighlightListAccentMediumBrush"]).Color =
                    accentColor;
                ((SolidColorBrush) Application.Current.Resources["SystemControlHyperlinkTextBrush"]).Color =
                    accentColor;

                ((SolidColorBrush) Application.Current.Resources["ContentDialogBorderThemeBrush"]).Color = accentColor;
                ((SolidColorBrush) Application.Current.Resources["JumpListDefaultEnabledBackground"]).Color =
                    accentColor;
                ((SolidColorBrush) Application.Current.Resources["ContentDialogBorderThemeBrush"]).Color = accentColor;
            }
            catch
            {
            }
        }

        public static void UpdateTitleBar(Color accentColor)
        {
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = accentColor;
            titleBar.ForegroundColor = accentColor;
            titleBar.ButtonBackgroundColor = accentColor;
            titleBar.ButtonForegroundColor = Colors.White;
        }
    }
}