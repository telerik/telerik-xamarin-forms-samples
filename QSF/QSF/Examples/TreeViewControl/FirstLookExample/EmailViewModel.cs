using System;
using System.Xml.Serialization;
using QSF.ViewModels;

namespace QSF.Examples.TreeViewControl.FirstLookExample
{
    [XmlRoot("Folder")]
    public class EmailViewModel : BindableBase
    {
        private FolderViewModel parent;
        private string sender;
        private string subject;
        private DateTime received;
        private string message;

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

        [XmlAttribute("Sender")]
        public string Sender
        {
            get
            {
                return this.sender;
            }
            set
            {
                if (this.sender != value)
                {
                    this.sender = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [XmlAttribute("Subject")]
        public string Subject
        {
            get
            {
                return this.subject;
            }
            set
            {
                if (this.subject != value)
                {
                    this.subject = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [XmlAttribute("Received")]
        public DateTime Received
        {
            get
            {
                return this.received;
            }
            set
            {
                if (this.received != value)
                {
                    this.received = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [XmlAttribute("Message")]
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                if (this.message != value)
                {
                    this.message = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
