using QSF.ViewModels;
using Telerik.XamarinForms.Primitives;

namespace QSF.Examples.BusyIndicatorControl.GalleryExample
{
    public class GalleryItemViewModel : GalleryItemViewModelBase
    {
        public GalleryItemViewModel(string icon, string inactiveIcon, string dataTemplateResourceKey, AnimationType animationType)
            : base(icon, inactiveIcon, dataTemplateResourceKey)
        {
            this.AnimationType = animationType;
        }

        public AnimationType AnimationType { get; }
    }
}
