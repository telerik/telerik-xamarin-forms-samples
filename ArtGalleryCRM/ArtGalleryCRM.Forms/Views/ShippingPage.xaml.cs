namespace ArtGalleryCRM.Forms.Views
{
    public partial class ShippingPage
    {
        public ShippingPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await this.ViewModel.LoadShippingDataAsync();
        }
    }
}