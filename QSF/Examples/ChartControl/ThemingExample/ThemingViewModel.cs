using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QSF.Examples.ChartControl.ThemingExample
{
    public class ThemingViewModel : GalleryExampleViewModelBase
    {
        protected override IEnumerable<GalleryItemViewModelBase> GetGalleryItems()
        {
            var seriesData = new ObservableCollection<DataItem>()
            {
                new DataItem(){Category = "2017", Value = 250},
                new DataItem(){Category = "2018", Value = 319},
                new DataItem(){Category = "2019", Value = 398},
                new DataItem(){Category = "2020", Value = 420}
            };

            var seriesData2 = new ObservableCollection<DataItem>()
            {
                new DataItem(){Category = "2017", Value = 315},
                new DataItem(){Category = "2018", Value = 280},
                new DataItem(){Category = "2019", Value = 397},
                new DataItem(){Category = "2020", Value = 462}
            };

            var seriesData3 = new ObservableCollection<DataItem>()
            {
                new DataItem(){Category = "2017", Value = 275},
                new DataItem(){Category = "2018", Value = 299},
                new DataItem(){Category = "2019", Value = 264},
                new DataItem(){Category = "2020", Value = 186}
            };

            var pieData = new ObservableCollection<DataItem>()
            {
                new DataItem() { Category = "2018", Value = .82 },
                new DataItem() { Category = "2019", Value = .38 },
                new DataItem() { Category = "2020", Value = .26 }
            };

            return new GalleryItemViewModelBase[]
            {
                new SeriesGalleryItemViewModel("Chart_Theming_Bar1_Header_Active.png", "Chart_Theming_Bar1_Header_Inactive.png", "bar", seriesData, seriesData2, seriesData3),
                new SeriesGalleryItemViewModel("Chart_Theming_Pie_Header_Active.png", "Chart_Theming_Pie_Header_Inactive.png", "pieChart", pieData),
            };
        }
    }
}