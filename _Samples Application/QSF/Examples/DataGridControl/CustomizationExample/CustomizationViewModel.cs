using QSF.Examples.DataGridControl.Common;
using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.DataGridControl.CustomizationExample
{
    public class CustomizationViewModel : ExampleViewModel
    {
        private ObservableCollection<SalesPerson> salesPeople;

        public CustomizationViewModel()
        {
            this.salesPeople = DataGenerator.GetItems<ObservableCollection<SalesPerson>>(ResourcePaths.PeoplePath);
        }

        public ObservableCollection<SalesPerson> SalesPeople
        {
            get
            {
                return this.salesPeople;
            }
        }
    }
}
