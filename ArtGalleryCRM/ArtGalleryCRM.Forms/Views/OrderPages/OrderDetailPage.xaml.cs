using ArtGalleryCRM.Forms.ViewModels;
using ArtGalleryCRM.Forms.ViewModels.OrderViewModels;

namespace ArtGalleryCRM.Forms.Views.OrderPages
{
    public partial class OrderDetailPage
    {
        public OrderDetailPage()
        {
            this.InitializeComponent();
        }

        public OrderDetailPage(OrderDetailViewModel viewModel)
        {
            this.InitializeComponent();
            this.BindingContext = viewModel;
        }
    }
}