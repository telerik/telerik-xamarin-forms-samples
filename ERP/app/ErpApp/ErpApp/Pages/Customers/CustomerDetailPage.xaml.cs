using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace ErpApp.Pages.Customers
{
    public partial class CustomerDetailPage : MvxContentPage<ViewModels.CustomerDetailViewModel>, IMvxOverridePresentationAttribute
    {
        public CustomerDetailPage()
        {
            InitializeComponent();

            if (Device.Idiom == TargetIdiom.Phone)
            {
                this.SetBinding(TitleProperty, new Binding("Title"));

                this.detailView.PropertyChanged += this.HandleCustomerDetailViewPropertyChanged;

                this.editView = new CustomerEditViewPhone();
                this.editView.PropertyChanged += this.HandleCustomerEditViewPropertyChanged;

                optionsToolbarItem = new ToolbarItem();
                optionsToolbarItem.Text = "Layout";
                optionsToolbarItem.Icon = new FileImageSource() { File = "ellipsis" };
                optionsToolbarItem.Clicked += this.OptionsToolbarItem_Clicked;

                checkToolbarItem = new ToolbarItem();
                checkToolbarItem.Text = "Save";
                checkToolbarItem.Icon = new FileImageSource() { File = "check" };
                checkToolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding("CommitCommand"));

                this.LayoutRoot.Children.Remove(this.searchBar);
                this.LayoutRoot.RowDefinitions.Clear();
                this.ContentRoot.ClearValue(Grid.RowProperty);

                this.LayoutRoot.Children.Remove(this.EmptySearchResults);
            }
            else
            {
                this.Title = "Customer's Details";
                NavigationPage.SetHasNavigationBar(this, false);
                this.editView = new CustomerEditViewTablet();
            }

            this.editView.IsVisible = false;
            var trigger = new DataTrigger(editView.GetType());
            trigger.Binding = new Binding("IsEditing");
            trigger.Value = bool.TrueString;
            trigger.Setters.Add(new Setter() { Property = ContentView.IsVisibleProperty, Value = true });
            this.editView.Triggers.Add(trigger);
            this.ContentRoot.Children.Add(editView);
        }

        private ToolbarItem optionsToolbarItem;
        private ToolbarItem checkToolbarItem;
        private ContentView editView;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Device.Idiom != TargetIdiom.Phone)
                return;

            if (this.detailView.IsVisible)
            {
                if (!this.ToolbarItems.Contains(optionsToolbarItem))
                    this.ToolbarItems.Add(optionsToolbarItem);
            }

            if (this.editView.IsVisible)
            {
                if (!this.ToolbarItems.Contains(checkToolbarItem))
                    this.ToolbarItems.Add(checkToolbarItem);
            }
        }

        private void OptionsToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            if (this.detailView.IsVisible)
                this.detailView.OpenPopup();
        }

        private void HandleCustomerDetailViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == IsVisibleProperty.PropertyName)
            {
                if (this.detailView.IsVisible)
                {
                    if (!this.ToolbarItems.Contains(optionsToolbarItem))
                        this.ToolbarItems.Add(optionsToolbarItem);
                }
                else
                {
                    if (this.ToolbarItems.Contains(optionsToolbarItem))
                        this.ToolbarItems.Remove(optionsToolbarItem);
                }
            }
        }

        private void HandleCustomerEditViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == IsVisibleProperty.PropertyName)
            {
                if (this.editView?.IsVisible == true)
                {
                    this.detailView.ClosePopup();
                    if (!this.ToolbarItems.Contains(checkToolbarItem))
                        this.ToolbarItems.Add(checkToolbarItem);
                }
                else
                {
                    if (this.ToolbarItems.Contains(checkToolbarItem))
                        this.ToolbarItems.Remove(checkToolbarItem);
                }
            }
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            if (Device.Idiom == TargetIdiom.Phone)
            {
                return new MvxContentPagePresentationAttribute() { WrapInNavigationPage = true };
            }
            else
            {
                return new MvxCustomMasterDetailPagePresentationAttribute(MasterDetailPosition.Detail) { NoHistory = true, MasterHostViewType = typeof(CustomersPage) };
            }
        }
    }
}
