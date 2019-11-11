using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.AccordionControl.CustomizationExample
{
    public class CustomizationViewModel : ExampleViewModel
    {
        private ObservableCollection<DataItem> seriesData;

        public CustomizationViewModel()
        {
            this.SeriesData = new ObservableCollection<DataItem>
            {
                new DataItem() { Value = 12.5, Category = "Ecommerce" },
                new DataItem() { Value = 62.5, Category = "Personal" },
                new DataItem() { Value = 25, Category = "Direct" }
            };
        }

        public ObservableCollection<DataItem> SeriesData
        {
            get
            {
                return this.seriesData;
            }
            set
            {
                if (this.seriesData != value)
                {
                    this.seriesData = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
