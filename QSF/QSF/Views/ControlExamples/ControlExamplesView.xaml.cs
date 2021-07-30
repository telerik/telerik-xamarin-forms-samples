using QSF.ViewModels;

namespace QSF.Views
{
    public partial class ControlExamplesView : ExamplesViewBase
    {
        public ControlExamplesView()
        {
            this.InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            this.examplesList.UpdateListViewLayoutDefinition(160, 6, 10, 12);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as PageViewModelBase;
            if (viewModel != null)
            {
                viewModel.OnAppearing();
            }
        }

        protected override void OnDisappearing()
        {
            var viewModel = this.BindingContext as PageViewModelBase;
            if (viewModel != null)
            {
                viewModel.OnDisappearing();
            }

            base.OnDisappearing();
        }
    }
}