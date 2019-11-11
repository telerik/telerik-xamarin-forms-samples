using System.Collections.ObjectModel;
using System.Xml.Serialization;
using QSF.ViewModels;

namespace QSF.Examples.TreeViewControl.CustomizationExample
{
    [XmlRoot("Agencies")]
    public class AgenciesViewModel : BindableBase
    {
        public AgenciesViewModel()
        {
            this.Continents = new ObservableCollection<ContinentViewModel>();
        }

        [XmlArray("Continents")]
        [XmlArrayItem("Continent")]
        public ObservableCollection<ContinentViewModel> Continents { get; private set; }
    }
}
