using ArtGalleryCRM.Forms.Interfaces;
using Telerik.XamarinForms.Common.Data;

namespace ArtGalleryCRM.Forms.Views.OrderPages
{
    public partial class OrdersPage : IDataGridView
    {
        public OrdersPage()
        {
            this.InitializeComponent();
            this.ViewModel.DataGridView = this;
        }

        public void SetGroupDescriptor(GroupDescriptorBase groupDescriptor)
        {
            this.DataGrid.GroupDescriptors.Add(groupDescriptor);
        }

        public void ClearGroupDescriptors()
        {
            this.DataGrid.GroupDescriptors.Clear();
        }
    }
}