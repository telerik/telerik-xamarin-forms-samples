using ArtGalleryCRM.Forms.ViewModels;
using Telerik.XamarinForms.DataControls;

namespace ArtGalleryCRM.Forms.Views
{
    public partial class RootPageMaster
    {
        public RadListView ListView;

        public RootPageMaster()
        {
            this.InitializeComponent();

            this.BindingContext = new RootMasterViewModel();
            this.ListView = this.MenuItemsListView;
        }
    }
}