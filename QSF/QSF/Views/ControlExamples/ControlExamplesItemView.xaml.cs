using Xamarin.Forms;

namespace QSF.Views
{
    public partial class ControlExamplesItemView : ContentView
    {
        public ControlExamplesItemView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            // In Android when the cell is disposed the BindingContext is set to null
            if (Device.RuntimePlatform == Device.Android && this.BindingContext == null)
            {
                this.stack.RemoveBinding(StackLayout.BackgroundColorProperty);
                this.grid.RemoveBinding(Grid.BackgroundColorProperty);
            }
        }
    }
}
