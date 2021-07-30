using System;
using System.ComponentModel;
using QSF.iOS.Effects;
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

            var textField = GetSubViewOfType<UITextField>(view);
            if (textField != null)
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                {
                    textField.BackgroundColor = UIColor.FromDynamicProvider((traitCollection) =>
                    {
                        return traitCollection.UserInterfaceStyle == UIUserInterfaceStyle.Dark
                        ? ((Color)App.Current.Resources["LightBackgroundColorDark"]).ToUIColor()
                        : ((Color)App.Current.Resources["DarkBackgroundColorLight"]).ToUIColor();
                    });
                }
                else
                {
                    textField.BackgroundColor = ((Color)App.Current.Resources["DarkBackgroundColorLight"]).ToUIColor();
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

        private static T GetSubViewOfType<T>(UIView view) where T : class
        {
            if (view != null)
            {
                foreach (var subView in view.Subviews)
                {
                    var child = subView as T;
                    if (child != null)
                    {
                        return child;
                    }

                    child = GetSubViewOfType<T>(subView);
                    if (child != null)
                    {
                        return child;
                    }
                }
            }

            return null;
        }
    }
}