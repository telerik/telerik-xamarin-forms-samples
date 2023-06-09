using QSF.ViewModels;
using System.Linq;

namespace QSF.Examples.ImageEditorControl.ProgrammaticControlExample
{
    public class ProgrammaticControlViewModel : ExampleViewModel
    {
        private string image;

        public ProgrammaticControlViewModel()
        {
            this.LoadImage();
        }

        public string Image
        {
            get
            {
                return this.image;
            }
            private set
            {
                if (this.image != value)
                {
                    this.image = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private async void LoadImage()
        {
            var imagePaths = await StorageHelper.ExtractResourcesAsync("ImageEditor", "FirstLook");
            var imagePath = imagePaths.ElementAt(3);

            this.Image = imagePath;
        }
    }
}
