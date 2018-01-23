using Xamarin.Forms;

namespace QSF.Views
{
    public abstract class ExamplesViewBase : ContentPage
    {
        public ExamplesViewBase()
        {
            this.BackgroundColor = (Color)App.Current.Resources["LightBackgroundColor"];

            var controlTemplate = (ControlTemplate)App.Current.Resources["ExamplesViewBase"];

            this.ControlTemplate = controlTemplate;
        }
    }
}
