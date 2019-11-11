using ErpApp.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ErpApp.ViewModels
{
    public class OrderCompletionViewModel : MvxViewModel<string, OrderCompletionViewModel.SigningResult>
    {
        public OrderCompletionViewModel(Services.IErpService erpService, IMvxNavigationService navigationService)
        {
            this.erpService = erpService;
            this.navigationService = navigationService;
            this.DoneCommand = new MvxAsyncCommand(OnDone);
            this.CancelCommand = new MvxAsyncCommand(OnCancel);
        }

        private Order targetOrder;
        private string targetOrderId;
        private Services.IErpService erpService;
        private IMvxNavigationService navigationService;

        public Order Order
        {
            get => this.targetOrder;
            private set => SetProperty(ref this.targetOrder, value);
        }

        public ICommand DoneCommand { get; }
        public ICommand CancelCommand { get; }

        public override void Prepare(string parameter)
        {
            this.targetOrderId = parameter;
        }

        public async override Task Initialize()
        {
            await base.Initialize();

            if (string.IsNullOrEmpty(this.targetOrderId))
                return;

            this.Order = await this.erpService.GetOrderAsync(this.targetOrderId);
        }

        private async Task OnCancel()
        {
            await this.navigationService.Close(this, new SigningResult(false));
            await this.navigationService.ChangePresentation(new MvvmCross.Presenters.Hints.MvxPopPresentationHint(typeof(OrderScheduleViewModel)));
        }

        private async Task OnDone()
        {
            await this.navigationService.Close(this, new SigningResult(true));
            await this.navigationService.ChangePresentation(new MvvmCross.Presenters.Hints.MvxPopPresentationHint(typeof(OrderScheduleViewModel)));
        }

        public class SigningResult
        {
            public SigningResult(bool result)
            {
                this.IsSigned = result;
            }

            public bool IsSigned { get; }
        }
    }
}
