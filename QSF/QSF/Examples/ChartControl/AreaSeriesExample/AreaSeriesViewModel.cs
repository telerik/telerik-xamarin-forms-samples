using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QSF.Examples.ChartControl.AreaSeriesExample
{
    public class AreaSeriesViewModel : GalleryExampleViewModelBase
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
                new SeriesGalleryItemViewModel("Chart_Area1_Header_Active.png", "Chart_Area1_Header_Inactive.png", "lineArea", seriesData),
                new SeriesGalleryItemViewModel("Chart_Area2_Header_Active.png", "Chart_Area2_Header_Inactive.png", "stacked-lineArea", seriesData, secondSeriesData),
                new SeriesGalleryItemViewModel("Chart_Area3_Header_Active.png", "Chart_Area3_Header_Inactive.png", "splineArea", seriesData),
                new SeriesGalleryItemViewModel("Chart_Area4_Header_Active.png", "Chart_Area4_Header_Inactive.png", "stacked-splineArea", seriesData, secondSeriesData),
            };
        }
    }
}
