using System;
using System.IO;
using System.Threading.Tasks;
using Telerik.XamarinForms.Input;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.SignaturePadControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : ContentView
    {
        private FirstLookViewModel viewModel;
        private View popupContent;
        RadPopup popup;

        public FirstLookView()
        {
            InitializeComponent();
            this.viewModel = new FirstLookViewModel();
            this.BindingContext = this.viewModel;
            this.popup = new RadPopup();
            this.popup.Placement = PlacementMode.Center;
        }

        private void signaturePad_StrokeStarted(object sender, EventArgs e)
        {
            this.viewModel.IsSigned = true;
        }

        private void signaturePad_Cleared(object sender, EventArgs e)
        {
            this.viewModel.IsSigned = false;
        }

        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            await this.SaveImage(ImageFormat.Png);
        }

        private async Task SaveImage(ImageFormat format)
        {
            byte[] array;
            ImageSource imageSource;

            using (var stream = new MemoryStream())
            {
                var settings = new SaveImageSettings() 
                { 
                    ImageFormat = format, 
                    BackgroundColor = Color.Transparent,
                };
                await this.signaturePad.SaveImageAsync(stream, settings);
                array = stream.ToArray();

                imageSource = ImageSource.FromStream(() => new MemoryStream(array));
            }

            this.popupContent = (View)((ControlTemplate)this.Resources["PopupControlTemplate"]).CreateContent();
            Image image = (Image)this.popupContent.FindByName("signatureImage");
            image.Source = imageSource;
            this.popup.Content = popupContent;
            this.popup.OutsideBackgroundColor = Color.FromHex("#BF4B4C4C");
            this.popup.IsModal = true;
            this.popup.IsOpen = true;
        }

        private void clearButton_Clicked(object sender, EventArgs e)
        {
            this.popup.IsOpen = false;
        }
    }
}