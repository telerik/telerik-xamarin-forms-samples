using ArtGalleryCRM.Forms.ViewModels;
using ArtGalleryCRM.Forms.ViewModels.CustomerViewModels;

namespace ArtGalleryCRM.Forms.Views.CustomerPages
{
    public partial class CustomerDetailPage
    {
        public CustomerDetailPage()
        {
            this.InitializeComponent();
        }

        public CustomerDetailPage(CustomerDetailViewModel viewModel)
        {
            this.InitializeComponent();
            this.BindingContext = viewModel;
        }
    }
}