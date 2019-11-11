using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace ErpApp.Pages.Orders
{
    public partial class OrderSchedulePage : MvxContentPage<ViewModels.OrderScheduleViewModel>, IMvxOverridePresentationAttribute
    {
        public OrderSchedulePage()
        {
            InitializeComponent();

            if (Device.Idiom != TargetIdiom.Phone)
            {
                NavigationPage.SetHasNavigationBar(this, false);
                this.IconImageSource = new FileImageSource() { File = "Schedule" };
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
                return new MvxTabbedPagePresentationAttribute(TabbedPosition.Tab) { WrapInNavigationPage = true };
            }
        }
    }

    public class OrderStatusTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ComplatedTemplate { get; set; }
        public DataTemplate ActionTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is Models.Order order)
            {
                return order.IsCompleted ? ComplatedTemplate : ActionTemplate;
            }
            return null;
        }
    }
}
