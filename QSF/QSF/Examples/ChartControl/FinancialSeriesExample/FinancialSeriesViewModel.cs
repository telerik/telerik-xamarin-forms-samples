using Newtonsoft.Json;
using QSF.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace QSF.Examples.ChartControl.FinancialSeriesExample
{
    public class FinancialSeriesViewModel : GalleryExampleViewModelBase
    {
        protected override IEnumerable<GalleryItemViewModelBase> GetGalleryItems()
        {
            var seriesData = LoadDataFromJsonFile();

            return new GalleryItemViewModelBase[]
            {
                new FinancialSeriesGalleryItemViewModel("Chart_Financial1_Header_Active.png", "Chart_Financial1_Header_Inactive.png", "OhlcSeries", seriesData),
                new FinancialSeriesGalleryItemViewModel("Chart_Financial2_Header_Active.png", "Chart_Financial2_Header_Inactive.png", "CandlestickSeries", seriesData),
                new FinancialSeriesGalleryItemViewModel("Chart_Financial3_Header_Active.png", "Chart_Financial3_Header_Inactive.png", "FinancialIndicators", seriesData)
            };
        }

        private FinancialDataItem[] LoadDataFromJsonFile()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(FinancialSeriesViewModel)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("QSF.Examples.ChartControl.FinancialSeriesExample.AppleStockPrices.json");
            FinancialDataItem[] financialData;

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                financialData = JsonConvert.DeserializeObject<FinancialDataItem[]>(json);
            }
            return financialData.Take(32).ToArray();
        }
    }
}