
using Xamarin.Forms;

namespace QSF.Examples.RichTextEditorControl.ImportExportExample
{
    public partial class FileSaveView : ContentPage
    {
        public FileSaveView()
        {
            this.InitializeComponent();

            // The PageRenderer by default sets BackgroundColor for the NavigationPage to White: https://github.com/xamarin/Xamarin.Forms/blob/ec380e4e24b213500a01992b3e10c5e8a1af3b3a/Xamarin.Forms.Platform.iOS/Renderers/PageRenderer.cs#L537
            // Because of that a Background should be set in order to make the StatusBar in iOS be visible when the mode is either dark or light.
            this.SetAppThemeColor(ContentPage.BackgroundColorProperty, (Color)App.Current.Resources["DarkBackgroundColorLight"], (Color)App.Current.Resources["DarkBackgroundColorDark"]);
        }
    }
}