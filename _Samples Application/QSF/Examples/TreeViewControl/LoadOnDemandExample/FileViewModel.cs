using QSF.ViewModels;
using System.Xml.Serialization;

namespace QSF.Examples.TreeViewControl.LoadOnDemandExample
{
    [XmlType("File")]
    public class FileViewModel : BindableBase
    {
        string name;
        string icon;

        public FileViewModel()
        {

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
    }
}