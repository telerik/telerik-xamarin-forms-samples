using QSF.iOS.Effects;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("TelerikQSF")]
[assembly: ExportEffect(typeof(BorderEffect), nameof(BorderEffect))]
namespace QSF.iOS.Effects
{
    public class BorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                this.UpdateBackgorundColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        private void UpdateBackgorundColor()
        {
            var view = (UISearchBar)Control;
            view.BackgroundColor = Color.Transparent.ToUIColor();
            view.TintColor = Color.Transparent.ToUIColor();
            view.SetBackgroundImage(new UIImage(), UIBarPosition.Any, UIBarMetrics.Default);

            foreach (var subview in view.Subviews)
            {
                foreach (var subview1 in subview.Subviews)
                {
                    var textField = subview1 as UITextField;
                    if (textField != null)
                    {
                        if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                        {
                            textField.BackgroundColor = UIColor.FromDynamicProvider((traitCollection) =>
                            {
                                return traitCollection.UserInterfaceStyle == UIUserInterfaceStyle.Dark
                                ? ((Color)App.Current.Resources["DarkBackgroundColorDark"]).ToUIColor()
                                : ((Color)App.Current.Resources["DarkBackgroundColorLight"]).ToUIColor();
                            });
                        }
                        else
                        {
                            textField.BackgroundColor = ((Color)App.Current.Resources["DarkBackgroundColorLight"]).ToUIColor();
                        }
                    }
                }
            }
        }

        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            try
            {
                if (args.PropertyName == View.BackgroundColorProperty.PropertyName)
                {
                    this.UpdateBackgorundColor();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }
    }
}