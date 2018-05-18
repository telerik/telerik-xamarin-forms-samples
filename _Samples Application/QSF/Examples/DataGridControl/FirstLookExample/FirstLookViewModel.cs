using QSF.ViewModels;
using System.Collections.ObjectModel;
using QSF.Examples.DataGridControl.Common;

namespace QSF.Examples.DataGridControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        public ObservableCollection<Order> OrderDetails { get; private set; }

        public FirstLookViewModel()
        {
            this.OrderDetails = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
        }
    }
}
