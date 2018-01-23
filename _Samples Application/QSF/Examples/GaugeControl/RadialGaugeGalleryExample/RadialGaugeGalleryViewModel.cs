using System.Collections.Generic;
using QSF.ViewModels;

namespace QSF.Examples.GaugeControl.RadialGaugeGalleryExample
{
    public class RadialGaugeGalleryViewModel : GalleryExampleViewModelBase
    {
        protected override IEnumerable<GalleryItemViewModelBase> GetGalleryItems()
        {
            return new GalleryItemViewModelBase[]
            {
                new GalleryItemViewModel("Gauge_RadialGauge1_Header_Active.png", "Gauge_RadialGauge1_Header_Inactive.png", "template1"),
                new GalleryItemViewModel("Gauge_RadialGauge2_Header_Active.png", "Gauge_RadialGauge2_Header_Inactive.png", "template2"),
                new GalleryItemViewModel("Gauge_RadialGauge3_Header_Active.png", "Gauge_RadialGauge3_Header_Inactive.png", "template3"),
                new GalleryItemViewModel("Gauge_RadialGauge4_Header_Active.png", "Gauge_RadialGauge4_Header_Inactive.png", "template4"),
                new GalleryItemViewModel("Gauge_RadialGauge5_Header_Active.png", "Gauge_RadialGauge5_Header_Inactive.png", "template5"),
                new GalleryItemViewModel("Gauge_RadialGauge6_Header_Active.png", "Gauge_RadialGauge6_Header_Inactive.png", "template6")
            };
        }
    }
}
