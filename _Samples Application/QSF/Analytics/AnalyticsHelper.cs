using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QSF
{
    public static class AnalyticsHelper
    {
        private const string Condition = "Release";
        private const string NavigateToExampleKey = "Navigate To Example";
        private const string SearchResultSelectedKey = "Search Result Selected";
        private const string ThemeChangedKey = "Theme Changed";
        private const string NavigateToCodeKey = "Navigate To Code";
        private const string NavigateToDocumentationKey = "Navigate To Documentation";
        private const string NavigateToProductPageKey = "Navigate To ProductPage";
        private const string NavigateToWhatsNewPageKey = "Navigate To Whats New Page";
        private const string PlatformInformationKey = "Platform Information";
        private const string IPAddress = "IP Address";
        private const string DeviceType = "Device Type";
        private const string RuntimePlatform = "Runtime Platform";
        private const string ControlName = "Control";
        private const string ExampleName = "Example";
        private const string SearchPhaseToResult = "Search phrase to Result";
        private const string SearchPhrase = "Search Phrase";
        private const string ThemeName = "Theme";

        [Conditional(Condition)]
        public static void Initialize(string platform)
        {
            switch (platform)
            {
                case Xamarin.Forms.Device.Android:
                    {
                        AppCenter.Start(AnalyticsKeys.AndroidSecret, typeof(Analytics), typeof(Crashes));
                    }
                    break;
                case Xamarin.Forms.Device.iOS:
                    {
                        AppCenter.Start(AnalyticsKeys.IOSSecret, typeof(Analytics), typeof(Crashes));
                    }
                    break;
                case Xamarin.Forms.Device.UWP:
                    {
                        AppCenter.Start(AnalyticsKeys.UWPSecret, typeof(Analytics), typeof(Crashes));
                    }
                    break;
                default:
                    {
                        throw new NotSupportedException("Platform is not supported: " + platform);
                    }
            }
        }

        [Conditional(Condition)]
        public static void TraceDeviceInfo()
        {
            Analytics.TrackEvent(PlatformInformationKey, new Dictionary<string, string> {
                { DeviceType, Xamarin.Forms.Device.Idiom.ToString() },
                { RuntimePlatform, Xamarin.Forms.Device.RuntimePlatform }
            });
        }

        [Conditional(Condition)]
        public static void TraceNavigateToExample(ExampleInfo exampleInfo)
        {
            var exampleName = exampleInfo.ExampleName;
            if (string.IsNullOrEmpty(exampleName))
            {
                exampleName = "NoExample";
            }

            Analytics.TrackEvent(NavigateToExampleKey, new Dictionary<string, string> {
                { ControlName, exampleInfo.ControlName },
                { ExampleName, string.Format("{0}.{1}", exampleInfo.ControlName, exampleInfo.ExampleName) }
            });
        }

        [Conditional(Condition)]
        public static void TraceSelectSearchResult(string searchText, ExampleNameSearchResultViewModel searchResult)
        {
            Analytics.TrackEvent(
                    SearchResultSelectedKey,
                    new Dictionary<string, string>
                    {
                        { SearchPhrase, searchText },
                        { ControlName, searchResult.ControlName },
                        { ExampleName, string.Format("{0}.{1}", searchResult.ControlName, searchResult.ExampleName) },
                        { SearchPhaseToResult, string.Format("{0} -> {1}.{2}", searchText, searchResult.ControlName, searchResult.ExampleName) }
                    });
        }

        [Conditional(Condition)]
        public static void TraceSelectSearchResult(string searchText, ControlNameSearchResultViewModel searchResult)
        {
            Analytics.TrackEvent(
                     SearchResultSelectedKey,
                     new Dictionary<string, string>
                     {
                        { SearchPhrase, searchText },
                        { ControlName, searchResult.ControlName },
                        { SearchPhaseToResult, string.Format("{0} -> {1}", searchText, searchResult.ControlName) }
                     });
        }

        [Conditional(Condition)]
        public static void TraceThemeChanged(string themeName)
        {
            Analytics.TrackEvent(ThemeChangedKey, new Dictionary<string, string>
            {
                { ThemeName, themeName }
            });
        }

        [Conditional(Condition)]
        public static void TraceNavigateToDocumentation()
        {
            Analytics.TrackEvent(NavigateToDocumentationKey);
        }

        [Conditional(Condition)]
        public static void TraceNavigateToDocumentation(string controlName)
        {
            Analytics.TrackEvent(
                     NavigateToDocumentationKey,
                     new Dictionary<string, string>
                     {
                        { ControlName, controlName }
                     });
        }

        [Conditional(Condition)]
        public static void TraceNavigateToCode()
        {
            Analytics.TrackEvent(NavigateToCodeKey);
        }

        [Conditional(Condition)]
        public static void TraceNavigateToCode(string controlName, string exampleName)
        {
            Analytics.TrackEvent(
                     NavigateToCodeKey,
                     new Dictionary<string, string>
                     {
                        { ControlName, controlName },
                        { ExampleName, string.Format("{0}.{1}", controlName, exampleName) }
                     });
        }

        [Conditional(Condition)]
        public static void TraceNavigateToProductPage()
        {
            Analytics.TrackEvent(NavigateToProductPageKey);
        }

        [Conditional(Condition)]
        public static void TraceNavigateToWhatsNewPage()
        {
            Analytics.TrackEvent(NavigateToWhatsNewPageKey);
        }
    }
}
