using System.Collections.ObjectModel;
using System.Xml.Serialization;
using QSF.ViewModels;

namespace QSF.Examples.TreeViewControl.CustomizationExample
{
    [XmlRoot("Continent")]
    public class ContinentViewModel : BindableBase
    {
        private string name;

        public ContinentViewModel()
        {
            this.Countries = new ObservableCollection<CountryViewModel>();
        }

        [XmlAttribute("Name")]
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [XmlArray("Countries")]
        [XmlArrayItem("Country")]
        public ObservableCollection<CountryViewModel> Countries { get; private set; }
    }
}
