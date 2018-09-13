using System.Collections.Generic;
using System.Xml.Serialization;

namespace QSF.Examples.TreeViewControl.LoadOnDemandExample
{
    [XmlType("Folder")]
    public class FolderViewModel : FileViewModel
    {
        List<FileViewModel> children;

        public FolderViewModel()
        {

        }

        [XmlArray]
        public List<FileViewModel> Children
        {
            get
            {
                return this.children;
            }
            set
            {
                if (this.children != value)
                {
                    this.children = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool hasUnrealizedChildren = true;
        public bool HasUnrealizedChildren
        {
            get
            {
                return this.hasUnrealizedChildren;
            }
            set
            {
                if (this.hasUnrealizedChildren != value)
                {
                    this.hasUnrealizedChildren = value;
                    this.OnPropertyChanged();
                }
            }
        }

        string path;
        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                if (this.path != value)
                {
                    this.path = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}