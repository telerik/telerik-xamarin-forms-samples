using QSF.Examples.DataGridControl.Common;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QSF.Examples.DataGridControl.CRUDOperationsExample
{
    public class AddItemViewModel : ConfigurationViewModel
    {
        private ICollection<Order> collection;

        public AddItemViewModel(ICollection<Order> collection)
        {
            this.NavigateBackCommand = new Command(this.GoBack);
            this.CancelCommand = new Command(this.GoBack);
            this.AddCommand = new Command(this.Add);
            this.collection = collection;
            this.Order = new Order();
            this.Title = "Add Item";
        }

        public ICommand NavigateBackCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand AddCommand { get; }
        public Order Order { get; }

        private void Add()
        {
            this.collection.Add(this.Order);
            this.GoBack();
        }

        private void GoBack()
        {
            Task t = this.NavigateBack();
        }
    }
}
