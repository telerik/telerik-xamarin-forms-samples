using Xamarin.Forms;

namespace QSF.Views
{
    public abstract class ExamplesViewBase : ContentPage
    {
        public static readonly BindableProperty DrawerLengthProperty =
            BindableProperty.Create(nameof(DrawerLength), typeof(double), typeof(ExamplesViewBase), 120.0);

        public ExamplesViewBase()
        {
            this.BackgroundColor = (Color)App.Current.Resources["LightBackgroundColor"];

            var controlTemplate = (ControlTemplate)App.Current.Resources["ExamplesViewBase"];

            this.ControlTemplate = controlTemplate;
        }

        public double DrawerLength
        {
            get { return (double)this.GetValue(DrawerLengthProperty); }
            set { this.SetValue(DrawerLengthProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var viewModel = (ViewModels.ExamplesViewModelBase)this.BindingContext;
            if (viewModel.HasConfiguration)
            {
                this.DrawerLength = 180;
            }
            else
            {
                this.DrawerLength = 120;
            }
        }
    }
}
