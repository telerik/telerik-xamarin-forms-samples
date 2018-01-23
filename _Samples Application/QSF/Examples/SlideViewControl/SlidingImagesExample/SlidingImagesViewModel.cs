using System.Collections.ObjectModel;
using QSF.ViewModels;

namespace QSF.Examples.SlideViewControl.SlidingImagesExample
{
    public class SlidingImagesViewModel : ViewModelBase
    {
        public ObservableCollection<string> Images { get; private set; }

        public SlidingImagesViewModel()
        {
            this.Images = new ObservableCollection<string>
            {
                "Chart_Bar_Series.png",
                "BusyIndicator_Theming.png",
                "Chart_Pie.png"
            };
        }
    }
}
