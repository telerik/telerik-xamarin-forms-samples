using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QSF.Examples.ChartControl.ScatterSeriesExample
{
    public class ScatterSeriesViewModel : GalleryExampleViewModelBase
    {
        protected override IEnumerable<GalleryItemViewModelBase> GetGalleryItems()
        {
            var seriesData = new ObservableCollection<DataItem>()
            {
                new DataItem(){Category = "Greenings", Value = 20, ValueY = 300},
                new DataItem(){Category = "Perfecto", Value = 35, ValueY = 700},
                new DataItem(){Category = "NearBy", Value = 60, ValueY = 950},
                new DataItem(){Category = "Store", Value = 85, ValueY = 1000},
                new DataItem(){Category = "Fresh&Green", Value = 90, ValueY = 1050}
            };

            var secondSeriesData = new ObservableCollection<DataItem>()
            {
                new DataItem(){Category = "Greenings", Value = 20, ValueY = 100},
                new DataItem(){Category = "Perfecto", Value = 40, ValueY = 700},
                new DataItem(){Category = "NearBy", Value = 50, ValueY = 850},
                new DataItem(){Category = "Store", Value = 95, ValueY = 900},
                new DataItem(){Category = "Fresh&Green", Value = 100, ValueY = 1250}
            };

            return new GalleryItemViewModelBase[]
            {
                new SeriesGalleryItemViewModel("Chart_Scattered1_Header_Active.png", "Chart_Scattered1_Header_Inactive.png", "ScatterPointSeries", seriesData, secondSeriesData),
                new SeriesGalleryItemViewModel("Chart_Scattered2_Header_Active.png", "Chart_Scattered2_Header_Inactive.png", "ScatterLineSeries", seriesData),
                new SeriesGalleryItemViewModel("Chart_Scattered3_Header_Active.png", "Chart_Scattered3_Header_Inactive.png", "ScatterSplineAreaSeries", seriesData),
                new SeriesGalleryItemViewModel("Chart_Scattered4_Header_Active.png", "Chart_Scattered4_Header_Inactive.png", "ScatterSplineSeries", seriesData),
                new SeriesGalleryItemViewModel("Chart_Scattered5_Header_Active.png", "Chart_Scattered5_Header_Inactive.png", "ScatterAreaSeries", seriesData),
            };
        }
    }
}