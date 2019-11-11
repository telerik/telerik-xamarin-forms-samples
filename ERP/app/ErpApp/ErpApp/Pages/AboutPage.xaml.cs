using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace ErpApp.Pages
{
	public partial class AboutPage : MvxContentPage<ViewModels.AboutPageViewModel>, IMvxOverridePresentationAttribute
    {
		public AboutPage ()
		{
			InitializeComponent ();

            if (Device.Idiom != TargetIdiom.Phone)
            {
                NavigationPage.SetHasNavigationBar(this, false);
                this.IconImageSource = new FileImageSource() { File = "About" };
                this.BackgroundColor = Color.FromHex("#f1f3f7");
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
                return new MvxTabbedPagePresentationAttribute(TabbedPosition.Tab) { WrapInNavigationPage = false };
            }
        }
    }
}