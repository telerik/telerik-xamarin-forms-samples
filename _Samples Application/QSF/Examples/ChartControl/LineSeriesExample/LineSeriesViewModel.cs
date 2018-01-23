using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QSF.Examples.ChartControl.LineSeriesExample
{
    public class LineSeriesViewModel : GalleryExampleViewModelBase
    {
        protected override IEnumerable<GalleryItemViewModelBase> GetGalleryItems()
        {
            var seriesData = new ObservableCollection<DataItem>()
            {
                new DataItem(){Category = "Greenings", Value = 5},
                new DataItem(){Category = "Perfecto", Value = 15},
                new DataItem(){Category = "NearBy", Value = 4},
                new DataItem(){Category = "FamilyStore", Value = 45},
                new DataItem(){Category = "Fresh&Green", Value = 10}
            };

            var secondSeriesData = new ObservableCollection<DataItem>()
            {
                new DataItem(){Category = "Greenings", Value = 60},
                new DataItem(){Category = "Perfecto", Value = 47},
                new DataItem(){Category = "NearBy", Value = 51},
                new DataItem(){Category = "FamilyStore", Value = 26},
                new DataItem(){Category = "Fresh&Green", Value = 82}
            };

            return new GalleryItemViewModelBase[]
            {
                new SeriesGalleryItemViewModel("Chart_Line1_Header_Active.png", "Chart_Line1_Header_Inactive.png", "line", seriesData),
                new SeriesGalleryItemViewModel("Chart_Line2_Header_Active.png", "Chart_Line2_Header_Inactive.png", "stacked-line", seriesData, secondSeriesData),
                new SeriesGalleryItemViewModel("Chart_Line3_Header_Active.png", "Chart_Line3_Header_Inactive.png", "spline", seriesData),
                new SeriesGalleryItemViewModel("Chart_Line4_Header_Active.png", "Chart_Line4_Header_Inactive.png", "stacked-spline", seriesData, secondSeriesData),
            };
        }
    }
}