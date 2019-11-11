using System.Collections.Generic;
using QSF.ViewModels;

namespace QSF.Examples.GaugeControl.VerticalGaugeGalleryExample
{
    public class VerticalGaugeGalleryViewModel : GalleryExampleViewModelBase
    {
        protected override IEnumerable<GalleryItemViewModelBase> GetGalleryItems()
        {
            return new GalleryItemViewModelBase[]
            {
                new GalleryItemViewModel("Gauge_VerticalGauge1_Active.png", "Gauge_VerticalGauge1_Inactive.png", "template9"),
                new GalleryItemViewModel("Gauge_VerticalGauge2_Active.png", "Gauge_VerticalGauge2_Inactive.png", "template10")
            };
        }
    }
}
