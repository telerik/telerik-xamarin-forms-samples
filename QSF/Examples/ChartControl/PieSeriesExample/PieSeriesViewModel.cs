using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QSF.Examples.ChartControl.PieSeriesExample
{
    public class PieSeriesViewModel : GalleryExampleViewModelBase
    {
        protected override IEnumerable<GalleryItemViewModelBase> GetGalleryItems()
        {
            var seriesData = new ObservableCollection<DataItem>()
            {
                new DataItem() { Value = 60, Category = "France - 60%" },
                new DataItem() { Value = 40, Category = "Belguim - 40%" }
            };

            var secondSeriesData = new ObservableCollection<DataItem>()
            {
                new DataItem() { Value = 30, Category = "France - 30%" },
                new DataItem() { Value = 30, Category = "Germany - 30%" },
                new DataItem() { Value = 40, Category = "Belguim - 40%" }
            };

            return new GalleryItemViewModelBase[]
            {
                new SeriesGalleryItemViewModel("Chart_Pie1_Header_Active.png", "Chart_Pie1_Header_Inactive.png", "Pie", seriesData),
                new SeriesGalleryItemViewModel("Chart_Pie2_Header_Active.png", "Chart_Pie2_Header_Inactive.png", "Pie2", secondSeriesData),
            };
        }
    }
}