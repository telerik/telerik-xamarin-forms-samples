using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml.Serialization;
using QSF.ViewModels;

namespace QSF.Examples.TreeViewControl.FirstLookExample
{
    [XmlRoot("Folder")]
    public class FolderViewModel : BindableBase
    {
        private FolderViewModel parent;
        private string name;
        private string icon;
        private bool isActive;

        public FolderViewModel()
        {
            this.Folders = new ObservableCollection<FolderViewModel>();
            this.Folders.CollectionChanged += this.OnFoldersChanged;
            this.Emails = new ObservableCollection<EmailViewModel>();
            this.Emails.CollectionChanged += this.OnEmailsChanged;
        }

        [XmlIgnore]
        public FolderViewModel Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                if (this.parent != value)
                {
                    this.parent = value;
                    this.OnPropertyChanged();
                }
            }
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

        [XmlIgnore]
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                if (this.isActive != value)
                {
                    this.isActive = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [XmlArray("Folders")]
        [XmlArrayItem("Folder")]
        public ObservableCollection<FolderViewModel> Folders { get; private set; }

        [XmlArray("Emails")]
        [XmlArrayItem("Email")]
        public ObservableCollection<EmailViewModel> Emails { get; private set; }

        private void OnFoldersChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            if (eventArgs.NewItems != null)
            {
                foreach (FolderViewModel folder in eventArgs.NewItems)
                {
                    folder.Parent = this;
                }
            }
        }

        private void OnEmailsChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            if (eventArgs.NewItems != null)
            {
                foreach (EmailViewModel email in eventArgs.NewItems)
                {
                    email.Parent = this;
                }
            }
        }
    }
}
