using QSF.ViewModels;
using System.Collections.ObjectModel;
using QSF.Examples.DataGridControl.Common;

namespace QSF.Examples.DataGridControl.ThemingExample
{
    public class ThemingViewModel : ViewModelBase
    {
        public ObservableCollection<Order> OrderDetails { get; private set; }

        public ThemingViewModel()
        {
            this.OrderDetails = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
        }
    }
}
