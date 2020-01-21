using Xamarin.Forms;

// Resolves Apple App Store warning for usage of deprecated UIWebView control by switching to the recommended WkWebView
[assembly: ExportRenderer(typeof(Xamarin.Forms.WebView), typeof(Xamarin.Forms.Platform.iOS.WkWebViewRenderer))]
