using QSF.Examples.DataGridControl.Common;
using QSF.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QSF.Examples.DataGridControl.CRUDOperationsExample
{
    public class EditItemViewModel : ConfigurationViewModel
    {
        public const string SaveItemMessage = "Save item";

        public EditItemViewModel(Order order)
        {
            this.NavigateBackCommand = new Command(this.GoBack);
            this.CancelCommand = new Command(this.GoBack);
            this.SaveCommand = new Command(this.Save);
            this.Order = order;
            this.Title = "Edit Item";
        }

        public ICommand NavigateBackCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SaveCommand { get; }
        public Order Order { get; }

        private void Save()
        {
            MessagingCenter.Send(this, SaveItemMessage);
            this.GoBack();
        }

        private void GoBack()
        {
            Task t = this.NavigateBack();
        }
    }
}
