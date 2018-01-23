using System.Collections.Generic;
using QSF.ViewModels;

namespace QSF.Examples.GaugeControl.HorizontalGaugeGalleryExample
{
    public class HorizontalGaugeGalleryViewModel : GalleryExampleViewModelBase
    {
        protected override IEnumerable<GalleryItemViewModelBase> GetGalleryItems()
        {
            return new GalleryItemViewModelBase[]
            {
                new GalleryItemViewModel("Gauge_HorizontalGauge1_Active.png", "Gauge_HorizontalGauge1_Inactive.png", "template7"),
                new GalleryItemViewModel("Gauge_HorizontalGauge2_Active.png", "Gauge_HorizontalGauge2_Inactive.png", "template8")
            };
        }
    }
}
