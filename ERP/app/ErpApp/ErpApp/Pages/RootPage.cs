using ErpApp.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace ErpApp.Pages
{
    [MvxTabbedPagePresentation(TabbedPosition.Root, WrapInNavigationPage = false, NoHistory = false)]
    public class RootPage : MvxTabbedPage<RootViewModel>
    {
        public RootPage()
        {
            if (Device.Idiom == TargetIdiom.Phone)
            {
                // Maximum number of items supported by BottomNavigationView is 5.
                Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetToolbarPlacement(this,
                    Xamarin.Forms.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);
            }
        }
    }
}

