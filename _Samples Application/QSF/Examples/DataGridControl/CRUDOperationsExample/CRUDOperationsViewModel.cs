using QSF.Examples.DataGridControl.Common;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace QSF.Examples.DataGridControl.CRUDOperationsExample
{
    public class CRUDOperationsViewModel : ExampleViewModel
    {
        private ObservableCollection<object> selectedItems;

        public CRUDOperationsViewModel()
        {
            this.OrderDetails = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
            this.GoToAddItemCommand = new Command(this.GoToAddItem);
            this.GoToEditItemCommand = new Command(this.GoToEditItem, this.CanExecuteGoToEditItemCommand);
            this.DeleteItemsCommand = new Command(this.DeleteItems, this.CanExecuteDeleteItemsCommand);
        }

        public ObservableCollection<Order> OrderDetails { get; }
        public ICommand GoToAddItemCommand { get; }
        public Command GoToEditItemCommand { get; }
        public Command DeleteItemsCommand { get; }

        public ObservableCollection<object> SelectedItems
        {
            get
            {
                return this.selectedItems;
            }
            set
            {
                if (this.selectedItems != value)
                {
                    ObservableCollection<object> oldValue = this.selectedItems;
                    this.selectedItems = value;
                    this.OnSelectedItems(oldValue);
                    this.OnPropertyChanged();
                }
            }
        }

        private void GoToAddItem()
        {
            AddItemViewModel addItemViewModel = new AddItemViewModel(this.OrderDetails);
            this.NavigationService.NavigateToConfigurationAsync(addItemViewModel);
        }

        private void OnSelectedItems(ObservableCollection<object> oldValue)
        {
            if (oldValue != null)
            {
                oldValue.CollectionChanged -= this.SelectedItems_CollectionChanged;
            }

            if (this.selectedItems != null)
            {
                this.selectedItems.CollectionChanged += this.SelectedItems_CollectionChanged;
            }

            this.UpdateCanExecuteGoToEditItemCommand();
            this.UpdateCanExecuteDeleteItemsCommand();
        }

        private void SelectedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.UpdateCanExecuteGoToEditItemCommand();
            this.UpdateCanExecuteDeleteItemsCommand();
        }

        private void UpdateCanExecuteGoToEditItemCommand()
        {
            this.GoToEditItemCommand.ChangeCanExecute();
        }

        private void GoToEditItem(object arg)
        {
            EditItemViewModel editItemViewModel = new EditItemViewModel((Order)this.selectedItems[0]);
            this.NavigationService.NavigateToConfigurationAsync(editItemViewModel);
        }

        private bool CanExecuteGoToEditItemCommand(object arg)
        {
            bool canExecute = this.selectedItems != null && this.selectedItems.Count == 1;
            return canExecute;
        }

        private void UpdateCanExecuteDeleteItemsCommand()
        {
            this.DeleteItemsCommand.ChangeCanExecute();
        }

        private void DeleteItems(object arg)
        {
            List<object> selectedItemsCopy = new List<object>(this.selectedItems);
            foreach (Order item in selectedItemsCopy)
            {
                this.OrderDetails.Remove(item);
            }
        }

        private bool CanExecuteDeleteItemsCommand(object arg)
        {
            bool canExecute = this.selectedItems != null && this.selectedItems.Count > 0;
            return canExecute;
        }
    }
}
