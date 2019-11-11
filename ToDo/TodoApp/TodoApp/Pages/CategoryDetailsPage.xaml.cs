using Telerik.XamarinForms.DataControls;
using Telerik.XamarinForms.DataControls.ListView;
using Xamarin.Forms;

namespace TodoApp.Pages
{
    public partial class CategoryDetailsPage : ContentPage
	{
		public CategoryDetailsPage()
		{
			InitializeComponent();
		}

        private void OnItemSwipeCompleted(object sender, ItemSwipeCompletedEventArgs e)
        {
            var listView = sender as RadListView;
            var item = e.Item as Models.TodoItem;
            var vm = listView.BindingContext as PageModels.CategoryDetailsPageModel;

            listView.EndItemSwipe();

            if (e.Offset >= 70)
            {
                vm.CompleteItemCommand?.Execute(item);
            }
            else if (e.Offset <= -70)
            {
                vm.DeleteItemCommand?.Execute(item);
            }
        }
    }
}
