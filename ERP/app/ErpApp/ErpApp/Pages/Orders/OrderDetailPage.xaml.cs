using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace ErpApp.Pages.Orders
{
    public partial class OrderDetailPage : MvxContentPage<ViewModels.OrderDetailViewModel>, IMvxOverridePresentationAttribute
    {
        public OrderDetailPage()
        {
            InitializeComponent();

            this.editView = new OrderEditView();
            if (Device.Idiom == TargetIdiom.Phone)
            {
                this.editView.PropertyChanged += this.HandleOrderEditViewPropertyChanged;

                this.detailView = new PhoneOrderDetail();
                this.detailView.PropertyChanged += this.HandleOrderDetailViewPropertyChanged;

                optionsToolbarItem = new ToolbarItem();
                optionsToolbarItem.Text = "Layout";
                optionsToolbarItem.Icon = new FileImageSource() { File = "ellipsis" };
                optionsToolbarItem.Clicked += this.OptionsToolbarItem_Clicked;

                checkToolbarItem = new ToolbarItem();
                checkToolbarItem.Text = "Save";
                checkToolbarItem.Icon = new FileImageSource() { File = "check" };
                checkToolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding("CommitCommand"));

                this.SetBinding(TitleProperty, new Binding("Title"));
                this.BackgroundColor = Color.White;

                this.LayoutRoot.Children.Remove(this.searchBar);
                this.LayoutRoot.RowDefinitions.Clear();
                this.ContentRoot.ClearValue(Grid.RowProperty);

                this.LayoutRoot.Children.Remove(this.EmptySearchResults);
            }
            else
            {
                this.detailView = new TabletOrderDetail();

                this.Title = "Order's Details";
                NavigationPage.SetHasNavigationBar(this, false);
                BackgroundColor = Color.FromHex("#f1f3f7");
            }

            this.ContentRoot.Children.Add(detailView);
            this.detailView.IsVisible = false;
            var trigger = new DataTrigger(detailView.GetType());
            trigger.Binding = new Binding("IsReading");
            trigger.Value = bool.TrueString;
            trigger.Setters.Add(new Setter() { Property = ContentView.IsVisibleProperty, Value = true });
            this.detailView.Triggers.Add(trigger);
            this.ContentRoot.Children.Add(detailView);

            this.editView.IsVisible = false;
            trigger = new DataTrigger(editView.GetType());
            trigger.Binding = new Binding("IsEditing");
            trigger.Value = bool.TrueString;
            trigger.Setters.Add(new Setter() { Property = ContentView.IsVisibleProperty, Value = true });
            this.editView.Triggers.Add(trigger);
            this.ContentRoot.Children.Add(editView);
        }

        private ContentView detailView, editView;
        private ToolbarItem optionsToolbarItem, checkToolbarItem;

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
            if (this.detailView.IsVisible && this.detailView is IPopupHost popupHost)
                popupHost.OpenPopup();
        }

        private void HandleOrderDetailViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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

        private void HandleOrderEditViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == IsVisibleProperty.PropertyName)
            {
                if (this.editView?.IsVisible == true)
                {
                    if (this.detailView is IPopupHost popupHost)
                        popupHost.ClosePopup();
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
                return new MvxCustomMasterDetailPagePresentationAttribute(MasterDetailPosition.Detail) { NoHistory = true, MasterHostViewType = typeof(OrdersPage) };
            }
        }
    }
}
