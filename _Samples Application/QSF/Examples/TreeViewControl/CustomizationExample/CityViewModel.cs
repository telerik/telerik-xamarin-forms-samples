using System.Xml.Serialization;
using QSF.ViewModels;

namespace QSF.Examples.TreeViewControl.CustomizationExample
{
    [XmlRoot("Country")]
    public class CityViewModel : BindableBase
    {
        private string name;

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
    }
}
