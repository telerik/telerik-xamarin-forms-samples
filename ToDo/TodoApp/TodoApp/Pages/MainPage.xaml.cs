using System.Collections.Specialized;
using Telerik.XamarinForms.DataControls.TreeView;
using TodoApp.PageModels;
using Xamarin.Forms;

namespace TodoApp.Pages
{
    public partial class MainPage : ContentPage
	{
        public MainPage()
        {
            InitializeComponent();

            treeView.CheckedItems.CollectionChanged += CheckedItems_CollectionChanged;
            MessagingCenter.Subscribe<PageModels.MainPageModel, Models.Category[]>(this, PageModels.MainPageModel.Uncheck_Categories_Action, this.UncheckCategories);
            MessagingCenter.Subscribe<PageModels.MainPageModel, bool>(this, PageModels.MainPageModel.Collapse_Categories_Action, this.CollapseCategories);
        }
        
        private void CheckedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            MessagingCenter.Send<CheckedItemsCollection, NotifyCollectionChangedAction>(sender as CheckedItemsCollection, "", e.Action);
        }

        protected override bool OnBackButtonPressed()
        {
            if (this.BindingContext is PageModels.MainPageModel vm)
            {
                if (vm.BackButtonPressed())
                    return true;
            }

            return base.OnBackButtonPressed();
        }

        private void UncheckCategories(PageModels.MainPageModel vm, Models.Category[] categories)
        {
            foreach (var item in categories)
            {
                this.treeView.UncheckItem(item);
            }
        }

        private void CollapseCategories(PageModels.MainPageModel vm, bool categoryEdit)
        {
            this.treeView.CollapseAll();
        }
    }
}
