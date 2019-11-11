using System;
using System.ComponentModel;
using System.Diagnostics;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using tagit.Controls;
using tagit.UWP.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using FFImageLoading.Forms;

[assembly: ExportRenderer(typeof(CircleImage), typeof(CircleImageRenderer))]
namespace tagit.UWP.Renderers
{
    public class CircleImageRenderer : ViewRenderer<CachedImage, Ellipse>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CachedImage> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            var ellipse = new Ellipse();

            SetNativeControl(ellipse);
        }
 
        private async Task<BitmapImage> GetImageAsync(byte[] imageBuffer)
        {
            BitmapImage image = null;

            using (InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream())
            {
                // Writes the image byte array in an InMemoryRandomAccessStream
                // that is needed to set the source of BitmapImage.
                using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(imageBuffer);
                    await writer.StoreAsync();
                }

                image = new BitmapImage();

                await image.SetSourceAsync(ms);
            }

            return image;
        }

        protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control != null)
            {
                var min = Math.Min(Element.Width, Element.Height);

                if (min / 2.0f <= 0)
                    return;

                try
                {
                    Control.Width = min;
                    Control.Height = min;

                    Control.Stroke = new SolidColorBrush(Colors.Gray);
                    Control.StrokeThickness = 2.0;

                    BitmapImage bitmapImage = null;

                    if (Element.Source is UriImageSource)
                    {
                        bitmapImage = new BitmapImage(((UriImageSource) Element.Source).Uri);
                    }
                    else if (Element.Source is StreamImageSource)
                    {
                        var imageBytes =
                            ((Element as tagit.Controls.CircleImage)?.BindingContext as tagit.Models.ImageInformation)?.File;
 
                        bitmapImage = await GetImageAsync(imageBytes);
                    }
                    
                    if (bitmapImage != null)
                    {
                        Control.Fill = new ImageBrush {ImageSource = bitmapImage, Stretch = Stretch.UniformToFill};
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"CircleRenderer.CreateCircle Exception: {ex}");
                }
            }
        }
    }
}