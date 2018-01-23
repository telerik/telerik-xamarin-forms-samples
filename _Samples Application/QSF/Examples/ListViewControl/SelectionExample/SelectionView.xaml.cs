using System;
using Telerik.XamarinForms.DataControls.ListView;
using Xamarin.Forms;

namespace QSF.Examples.ListViewControl.SelectionExample
{
    public partial class SelectionView : ContentView
    {
        public SelectionView()
        {
            this.InitializeComponent();
        }

        private void OnButtonClicked(object sender, EventArgs eventArgs)
        {
            this.list.EndItemSwipe();
        }

        private void OnItemTapped(object sender, ItemTapEventArgs eventArgs)
        {
            var viewModel = (SelectionViewModel)this.BindingContext;

            if (viewModel.ViewMode == ViewMode.Read)
            {
                viewModel.DetailsCommand.Execute(eventArgs.Item);
            }
        }
    }
}
