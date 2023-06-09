using QSF.Examples.DataGridControl.Common;
using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.DataGridControl.AggregatesExample
{
    public class AggregatesViewModel : ExampleViewModel
    {
        public ObservableCollection<Order> Orders { get; private set; }
        public AggregatesViewModel()
        {
            this.Orders = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
        }
    }
}
