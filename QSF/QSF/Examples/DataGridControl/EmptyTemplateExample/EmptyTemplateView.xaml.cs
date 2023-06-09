using Xamarin.Forms;

namespace QSF.Examples.DataGridControl.EmptyTemplateExample
{
    public partial class EmptyTemplateView : ContentView
    {
        public EmptyTemplateView()
        {
            this.InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var viewModel = this.BindingContext as EmptyTemplateViewModel;

            if (viewModel != null)
            {
                viewModel.Columns = this.dataGrid.Columns;
            }
        }
    }
}
