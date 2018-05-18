using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.BarcodeControl.Barcode1DGalleryExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Barcode1DGalleryView : ContentView
    {
        private double width;
        private double height;

        public Barcode1DGalleryView()
        {
            this.InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                this.intelligentMail.IsVisible = width > height;
            }
        }
    }
}