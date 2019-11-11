using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QSF.Examples.ChartControl.BarSeriesExample
{
    public class BarSeriesViewModel : GalleryExampleViewModelBase
    {
        protected override IEnumerable<GalleryItemViewModelBase> GetGalleryItems()
        {
            var seriesData = new ObservableCollection<DataItem>()
            {
                new DataItem(){Category = "Greenings", Value = 85},
                new DataItem(){Category = "Perfecto", Value = 78},
                new DataItem(){Category = "NearBy", Value = 99},
                new DataItem(){Category = "FamilyStore", Value = 85},
                new DataItem(){Category = "Fresh&Green", Value = 57}
            };

            var secondSeriesData = new ObservableCollection<DataItem>()
            {
                new DataItem(){Category = "Greenings", Value = 75},
                new DataItem(){Category = "Perfecto", Value = 95},
                new DataItem(){Category = "NearBy", Value = 91},
                new DataItem(){Category = "FamilyStore", Value = 64},
                new DataItem(){Category = "Fresh&Green", Value = 67}
            };

            return new GalleryItemViewModelBase[]
            {
                new SeriesGalleryItemViewModel("Chart_Bar1_Header_Active.png", "Chart_Bar1_Header_Inactive.png", "bar", seriesData),
                new SeriesGalleryItemViewModel("Chart_Bar2_Header_Active.png", "Chart_Bar2_Header_Inactive.png", "cluster", seriesData, secondSeriesData),
                new SeriesGalleryItemViewModel("Chart_Bar3_Header_Active.png", "Chart_Bar3_Header_Inactive.png", "stacked", seriesData, secondSeriesData),
                new SeriesGalleryItemViewModel("Chart_Bar4_Header_Active.png", "Chart_Bar4_Header_Inactive.png", "stacked100", seriesData, secondSeriesData),
                new SeriesGalleryItemViewModel("Chart_Bar5_Header_Active.png", "Chart_Bar5_Header_Inactive.png", "hor-bar", seriesData),
                new SeriesGalleryItemViewModel("Chart_Bar6_Header_Active.png", "Chart_Bar6_Header_Inactive.png", "hor-cluster", seriesData, secondSeriesData),
                new SeriesGalleryItemViewModel("Chart_Bar7_Header_Active.png", "Chart_Bar7_Header_Inactive.png", "hor-stacked", seriesData, secondSeriesData),
                new SeriesGalleryItemViewModel("Chart_Bar8_Header_Active.png", "Chart_Bar8_Header_Inactive.png", "hor-stacked100", seriesData, secondSeriesData),
            };
        }
    }
}
