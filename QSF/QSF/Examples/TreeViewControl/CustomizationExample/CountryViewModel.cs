using System.Collections.ObjectModel;
using System.Xml.Serialization;
using QSF.ViewModels;

namespace QSF.Examples.TreeViewControl.CustomizationExample
{
    [XmlRoot("Country")]
    public class CountryViewModel : BindableBase
    {
        private string name;
        private string icon;

        public CountryViewModel()
        {
            this.Cities = new ObservableCollection<CityViewModel>();
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

        [XmlAttribute("Icon")]
        public string Icon
        {
            get
            {
                return this.icon;
            }
            set
            {
                if (this.icon != value)
                {
                    this.icon = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [XmlArray("Cities")]
        [XmlArrayItem("City")]
        public ObservableCollection<CityViewModel> Cities { get; private set; }
    }
}
