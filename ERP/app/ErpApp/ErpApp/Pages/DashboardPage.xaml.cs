using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace ErpApp.Pages
{
    public partial class DashboardPage : MvxContentPage<ViewModels.DashboardPageViewModel>, IMvxOverridePresentationAttribute
    {
        public DashboardPage()
        {
            InitializeComponent();
            if (Device.Idiom == TargetIdiom.Phone)
            {
                var aboutItem = new ToolbarItem();
                aboutItem.Text = "About";
                aboutItem.Icon = new FileImageSource() { File = "About" };
                aboutItem.SetBinding(ToolbarItem.CommandProperty, new Binding("AboutCommand"));
                this.ToolbarItems.Add(aboutItem);
            }
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            if (Device.Idiom == TargetIdiom.Phone)
            {
                return new MvxTabbedPagePresentationAttribute(TabbedPosition.Tab) { WrapInNavigationPage = true };
            }
            else
            {
                return new MvxTabbedPagePresentationAttribute(TabbedPosition.Tab) { WrapInNavigationPage = false };
            }
        }
    }
}
