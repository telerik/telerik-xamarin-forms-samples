using QSF.Services.Toast;
using QSF.ViewModels;
using System;
using System.Windows.Input;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace QSF.Examples.PopupControl.ContextMenuExample
{
    [XmlType("File")]
    public class FileViewModel : BindableBase
    {
        string name;
        string icon;
        private bool isPopupOpen;
        private PopupViewModel popupViewModel;

        public FileViewModel()
        {
            this.OpenContextMenuCommand = new Command(() => this.IsPopupOpen = true);
            this.PopupViewModel = new PopupViewModel(() => this.IsPopupOpen = false);
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

        public bool IsPopupOpen
        {
            get
            {
                return this.isPopupOpen;
            }
            set
            {
                if (this.isPopupOpen != value)
                {
                    this.isPopupOpen = value;
                    var toastService = DependencyService.Get<IToastMessageService>();
                    this.OnPropertyChanged();
                }
            }
        }

        public PopupViewModel PopupViewModel
        {
            get
            {
                return this.popupViewModel;
            }
            private set
            {
                if (this.popupViewModel != value)
                {
                    this.popupViewModel = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand OpenContextMenuCommand { get; }
    }
}
