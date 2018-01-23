using QSF.Services;
using System.Collections.Generic;
using Xamarin.Forms;
using System;

namespace QSF.Views
{
    public static class ThemesBehavior
    {
        public static readonly BindableProperty StyleClassProperty =
            BindableProperty.CreateAttached("StyleClass", typeof(string), typeof(ThemesBehavior), string.Empty, propertyChanged: OnPropertyChanged);

        public static string GetStyleClass(BindableObject view)
        {
            return (string)view.GetValue(StyleClassProperty);
        }

        public static void SetStyleClass(BindableObject view, string value)
        {
            view.SetValue(StyleClassProperty, value);
        }

        private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            IThemesService themesService = DependencyService.Get<IThemesService>();
            var resourceTypeName = themesService.CurrentTheme.ResourceTypeName;

            var xfView = ((View)bindable);
            if (!string.IsNullOrEmpty(resourceTypeName))
            {
                xfView.StyleClass = new List<string>() { (string)newValue };
            }
            else
            {
                xfView.StyleClass = new List<string>();
            }
        }
    }
}
