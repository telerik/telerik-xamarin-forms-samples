using System;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace ErpApp.ViewModels
{
    public class RootViewModel : MvxViewModel
    {
        public RootViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        private IMvxNavigationService navigationService;

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            MvxNotifyTask.Create(async () => await this.InitializeViewModels());
        }

        private async Task InitializeViewModels()
        {
            await navigationService.Navigate<DashboardPageViewModel>();
            if (Device.Idiom == TargetIdiom.Phone)
            {
                await navigationService.Navigate<CustomersListViewModel>();
                await navigationService.Navigate<OrderListViewModel>();
            }
            else
            {
                await navigationService.Navigate<CustomersRootViewModel>();
                await navigationService.Navigate<OrderRootViewModel>();
            }
            await navigationService.Navigate<ProductsViewModel>();
            await navigationService.Navigate<VendorsViewModel>();
            if (Device.Idiom != TargetIdiom.Phone)
            {
                await navigationService.Navigate<OrderScheduleViewModel>();
                await navigationService.Navigate<AboutPageViewModel>();
            }
        }
    }
}
