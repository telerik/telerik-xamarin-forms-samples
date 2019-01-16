using System.Collections.Generic;
using System.Xml.Serialization;

namespace QSF.Examples.PopupControl.ContextMenuExample
{
    [XmlType("Folder")]
    public class FolderViewModel : FileViewModel
    {
        private List<FileViewModel> children;
        private bool hasUnrealizedChildren = true;
        private string path;

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
