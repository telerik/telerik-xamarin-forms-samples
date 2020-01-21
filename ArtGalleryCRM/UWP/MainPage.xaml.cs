using Xamarin.Forms.Platform.UWP;

namespace ArtGalleryCRM.Uwp
{
    public sealed partial class MainPage : WindowsPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new ArtGalleryCRM.Forms.App());
        }
    }
}
