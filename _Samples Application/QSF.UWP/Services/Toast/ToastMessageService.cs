using QSF.Services.Toast;
using QSF.UWP.Services.Toast;
using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

[assembly: Xamarin.Forms.Dependency(typeof(ToastMessageService))]
namespace QSF.UWP.Services.Toast
{
    public class ToastMessageService : IToastMessageService
    {
        private const string DefaultBackgroundColor = "#1d1d1e";

        private const double LONG_DELAY = 3.5;
        private const double SHORT_DELAY = 2.0;

        public void LongAlert(string message)
        {
            this.ShowMessage(message, LONG_DELAY);
        }

        public void ShortAlert(string message)
        {
            this.ShowMessage(message, SHORT_DELAY);
        }

        private void ShowMessage(string message, double duration)
        {
            var label = new TextBlock
            {
                Text = message,
                Foreground = new SolidColorBrush(Windows.UI.Colors.White),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            var style = new Style { TargetType = typeof(FlyoutPresenter) };
            style.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(ToWindowsColor(DefaultBackgroundColor))));
            style.Setters.Add(new Setter(FrameworkElement.MaxHeightProperty, 1));
            style.Setters.Add(new Setter(FrameworkElement.MarginProperty, new Thickness(0, -30, 0, 30)));
            style.Setters.Add(new Setter(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Hidden));

            var flyout = new Flyout
            {
                Content = label,
                Placement = FlyoutPlacementMode.Bottom,
                FlyoutPresenterStyle = style,
            };

            flyout.ShowAt(Window.Current.Content as FrameworkElement);

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(duration) };
            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                flyout.Hide();
            };
            timer.Start();
        }

        private Color ToWindowsColor(string colorStr)
        {
            var r = (byte)System.Convert.ToUInt32(colorStr.Substring(1, 2), 16);
            var g = (byte)System.Convert.ToUInt32(colorStr.Substring(3, 2), 16);
            var b = (byte)System.Convert.ToUInt32(colorStr.Substring(5, 2), 16);
            //get the color
            Color color = Color.FromArgb(255, r, g, b);

            return color;
        }
    }
}
