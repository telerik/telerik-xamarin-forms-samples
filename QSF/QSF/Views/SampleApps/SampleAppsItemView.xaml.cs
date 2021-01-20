using Xamarin.Forms;

namespace QSF.Views
{
    public partial class SampleAppsItemView : ContentView
    {
        public SampleAppsItemView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            // In Android when the cell is disposed the BindingContext is set to null
            if (Device.RuntimePlatform == Device.Android && this.BindingContext == null)
            {
                this.grid.RemoveBinding(Grid.BackgroundColorProperty);
            }
        }
    }
}
