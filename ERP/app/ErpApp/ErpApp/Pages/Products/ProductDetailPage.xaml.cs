using System;
using System.Globalization;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace ErpApp.Pages.Products
{
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class ProductDetailPage : MvxContentPage<ViewModels.ProductDetailViewModel>
    {
        public ProductDetailPage()
        {
            InitializeComponent();

            if (Device.Idiom == TargetIdiom.Phone)
            {
                this.detailView.PropertyChanged += this.HandleProductDetailViewPropertyChanged;
                this.editView = new ProductEditViewPhone();
                this.editView.PropertyChanged += this.HandleProductEditViewPropertyChanged;

                optionsToolbarItem = new ToolbarItem();
                optionsToolbarItem.Text = "Layout";
                optionsToolbarItem.IconImageSource = new FileImageSource() { File = "ellipsis" };
                optionsToolbarItem.Clicked += this.OptionsToolbarItem_Clicked;

                checkToolbarItem = new ToolbarItem();
                checkToolbarItem.Text = "Save";
                checkToolbarItem.IconImageSource = new FileImageSource() { File = "check" };
                checkToolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding("CommitCommand"));
            }
            else
            {
                this.editView = new ProductEditViewTablet();

                NavigationPage.SetHasNavigationBar(this, false);
            }

            this.editView.IsVisible = false;
            var trigger = new DataTrigger(editView.GetType());
            trigger.Binding = new Binding("IsEditing");
            trigger.Value = bool.TrueString;
            trigger.Setters.Add(new Setter() { Property = ContentView.IsVisibleProperty, Value = true });
            this.editView.Triggers.Add(trigger);
            this.LayoutRoot.Children.Add(editView);
        }

        private ContentView editView;
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

        private void OptionsToolbarItem_Clicked(object sender, EventArgs e)
        {
            this.detailView.OpenPopup();
        }

        private void HandleProductDetailViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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

        private void HandleProductEditViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
    }

    public class ProductBrushEqualityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return object.Equals(value, System.Convert.ToInt32(parameter));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}