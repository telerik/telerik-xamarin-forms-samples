using QSF.Examples.TreeViewControl.Common;
using QSF.ViewModels;

namespace QSF.Examples.TreeViewControl.CustomizationExample
{
    public class CustomizationViewModel : ExampleViewModel
    {
        private AgenciesViewModel agencies;

        public CustomizationViewModel()
        {
            this.Agencies = DataProvider.GetData<AgenciesViewModel>(ResourcePaths.AgenciesPath);
        }

        public AgenciesViewModel Agencies
        {
            get
            {
                return this.agencies;
            }
            private set
            {
                if (this.agencies != value)
                {
                    this.agencies = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
