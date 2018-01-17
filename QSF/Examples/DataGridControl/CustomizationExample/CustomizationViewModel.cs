using System.Collections.ObjectModel;
using QSF.Examples.DataGridControl.Common;

namespace QSF.Examples.DataGridControl.CustomizationExample
{
    public class CustomizationViewModel
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
