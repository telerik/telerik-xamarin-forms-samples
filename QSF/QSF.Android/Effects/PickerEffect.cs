using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using QSF.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(PickerEffect), nameof(PickerEffect))]
namespace QSF.Droid.Effects
{
    public class PickerEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                if (this.Control != null)
                {
                    (this.Control as EditText).Gravity = GravityFlags.CenterHorizontal;
                }
            }
            catch
            {

            }
        }

        protected override void OnDetached()
        {
        }
    }
}