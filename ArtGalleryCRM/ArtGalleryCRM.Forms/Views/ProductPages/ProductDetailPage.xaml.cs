using ArtGalleryCRM.Forms.ViewModels;
using ArtGalleryCRM.Forms.ViewModels.ProductViewModels;

namespace ArtGalleryCRM.Forms.Views.ProductPages
{
    public partial class ProductDetailPage
    {
        public ProductDetailPage()
        {
            this.InitializeComponent();
        }

        public ProductDetailPage(ProductDetailViewModel viewModel)
        {
            this.InitializeComponent();
            this.BindingContext = viewModel;
        }
    }
}