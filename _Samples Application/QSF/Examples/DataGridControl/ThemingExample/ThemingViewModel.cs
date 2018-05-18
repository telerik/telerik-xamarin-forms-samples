using QSF.Examples.DataGridControl.Common;
using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.DataGridControl.ThemingExample
{
    public class ThemingViewModel : ExampleViewModel
    {
        public ObservableCollection<Order> OrderDetails { get; private set; }

        public ThemingViewModel()
        {
            this.OrderDetails = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
        }
    }
}
