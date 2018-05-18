using System.Collections.ObjectModel;
using System.Xml.Serialization;
using QSF.ViewModels;

namespace QSF.Examples.TreeViewControl.FirstLookExample
{
    [XmlRoot("Account")]
    public class AccountViewModel : BindableBase
    {
        private string name;
        private string icon;
        private string address;

        public AccountViewModel()
        {
            this.Folders = new ObservableCollection<FolderViewModel>();
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

        [XmlAttribute("Address")]
        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                if (this.address != value)
                {
                    this.address = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [XmlArray("Folders")]
        [XmlArrayItem("Folder")]
        public ObservableCollection<FolderViewModel> Folders { get; private set; }
    }
}
