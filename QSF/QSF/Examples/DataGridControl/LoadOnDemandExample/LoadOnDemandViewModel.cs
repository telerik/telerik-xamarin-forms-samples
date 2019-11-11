using QSF.Examples.DataGridControl.Common;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.XamarinForms.Common;

namespace QSF.Examples.DataGridControl.LoadOnDemandExample
{
    public class LoadOnDemandViewModel : ExampleViewModel
    {
        private const int TakeItemsCount = 10;
        private const int SkipItemsStep = 10;
        public LoadOnDemandCollection<SalesPerson> Items { get; set; }
        private int skip = 0;

        public LoadOnDemandViewModel()
        {
            this.Items = new LoadOnDemandCollection<SalesPerson>((cancelationToken) =>
            {
                Task.Delay(1500, cancelationToken).Wait();

                var items = GetItems(this.skip, TakeItemsCount);
                this.skip += SkipItemsStep;
                return items;
            });

            var initialItems = GetItems(this.skip, TakeItemsCount);
            foreach (var item in initialItems)
            {
                this.Items.Add(item);
            }
        }

        private static IEnumerable<SalesPerson> GetItems(int skip, int take)
        {
            return DataGenerator.GetItems<ObservableItemCollection<SalesPerson>>(ResourcePaths.PeoplePath).Skip(skip).Take(take);
        }
    }
}

