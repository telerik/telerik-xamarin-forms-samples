using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using QSF.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(PickerEffect), nameof(PickerEffect))]
namespace QSF.iOS.Effects
{
    public class PickerEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            ((UITextField)this.Control).TextAlignment = UITextAlignment.Center;
            ((UITextField)this.Control).BorderStyle = UITextBorderStyle.None;
        }

        protected override void OnDetached()
        {
        }
    }
}