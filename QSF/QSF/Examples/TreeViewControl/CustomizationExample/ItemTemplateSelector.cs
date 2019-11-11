using Xamarin.Forms;

namespace QSF.Examples.TreeViewControl.CustomizationExample
{
    public class ItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ContinentTemplate { get; set; }
        public DataTemplate CountryTemplate { get; set; }
        public DataTemplate CityTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ContinentViewModel)
            {
                return this.ContinentTemplate;
            }

            if (item is CountryViewModel)
            {
                return this.CountryTemplate;
            }

            if (item is CityViewModel)
            {
                return this.CityTemplate;
            }

            return null;
        }
    }
}
