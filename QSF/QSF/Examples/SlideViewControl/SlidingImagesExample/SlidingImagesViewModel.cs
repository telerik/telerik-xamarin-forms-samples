using System.Collections.ObjectModel;
using QSF.ViewModels;

namespace QSF.Examples.SlideViewControl.SlidingImagesExample
{
    public class SlidingImagesViewModel : ExampleViewModel
    {
        public ObservableCollection<string> Images { get; private set; }

        public SlidingImagesViewModel()
        {
            this.Images = new ObservableCollection<string>
            {
                "ERP_App_Logo.png",
                "CRM_App_Logo.png",
                "Tagit_App_Logo.png",
                "ToDo_App_Logo.png"
            };
        }
    }
}
