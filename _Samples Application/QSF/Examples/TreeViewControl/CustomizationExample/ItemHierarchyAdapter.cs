using System.Collections.Generic;
using System.Linq;
using Telerik.XamarinForms.Common;

namespace QSF.Examples.TreeViewControl.CustomizationExample
{
    public class ItemHierarchyAdapter : IHierarchyAdapter
    {
        public IEnumerable<object> GetItems(object item)
        {
            var continent = item as ContinentViewModel;

            if (continent != null)
            {
                return continent.Countries;
            }

            var country = item as CountryViewModel;

            if (country != null)
            {
                return country.Cities;
            }

            return Enumerable.Empty<object>();
        }

        public object GetItemAt(object item, int index)
        {
            var continent = item as ContinentViewModel;

            if (continent != null)
            {
                return continent.Countries[index];
            }

            var country = item as CountryViewModel;

            if (country != null)
            {
                return country.Cities[index];
            }

            return null;
        }
    }
}
