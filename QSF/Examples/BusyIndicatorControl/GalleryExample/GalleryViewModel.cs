using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.XamarinForms.Primitives;

namespace QSF.Examples.BusyIndicatorControl.GalleryExample
{
    public class GalleryViewModel : GalleryExampleViewModelBase
    {
        protected override IEnumerable<GalleryItemViewModelBase> GetGalleryItems()
        {
            IEnumerable<AnimationType> enumValues = Enum.GetValues(typeof(AnimationType)).Cast<AnimationType>();
            IEnumerable<AnimationType> animationTypesExceptCustom = enumValues.Except(new AnimationType[] { AnimationType.Custom });

            int i = 1;
            foreach (var animationType in animationTypesExceptCustom)
            {
                var activeIcon = string.Format("BusyIndicator_Theming_Gallery{0}_Header_Active.png", i.ToString());
                var inactiveIcon = string.Format("BusyIndicator_Theming_Gallery{0}_Header_Inactive.png", i.ToString());
                i++;

                yield return new GalleryItemViewModel(activeIcon, inactiveIcon, "busyIndicator", animationType);
            }
        }
    }
}
