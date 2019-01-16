using QSF.Services.Toast;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Telerik.XamarinForms.DataControls.ListView;
using Xamarin.Forms;

namespace QSF.Examples.PopupControl.ModalExample
{
    public class ModalViewModel : ExampleViewModel
    {
        private static readonly string[] Names = new string[] { "Abbie Hunter", "Archie Wilson", "Blake Richardson", "Bree Conner", "Cody Fleming", "Dallas Ruiz", "Lola Hall", "Madeleine Haynes" };
        private static readonly string[] ImagePaths = new string[] { "DataGrid_SalesPerson_1.png", "DataGrid_SalesPerson_2.png", "DataGrid_SalesPerson_3.png", "DataGrid_SalesPerson_4.png", "DataGrid_SalesPerson_5.png", "DataGrid_SalesPerson_6.png", "DataGrid_SalesPerson_7.png", "DataGrid_SalesPerson_8.png" };
        private ObservableCollection<ContactViewModel> contacts;
        private ICommand itemTapCommand;
        private ICommand sendInvitesCommand;
        private ICommand goBackCommand;
        private ICommand closePopupCommand;
        private ImageSource firstSelectedPersonImage;
        private ImageSource secondSelectedPersonImage;
        private bool isSecondSelectedPersonVisible;
        private bool isAdditionalSelectedPersonsCountVisible;
        private int additionalSelectedPersonsCount;

        public ModalViewModel()
        {
            this.Contacts = new ObservableCollection<ContactViewModel>(this.GetSampleFiles());
            this.ItemTapCommand = new Command(this.ItemTap);
            this.SendInvitesCommand = new Command(this.SendInvites);
            this.ClosePopupCommand = new Command(this.ClosePopup);
            this.GoBackCommand = new Command(this.GoBack);
        }

        public ObservableCollection<ContactViewModel> Contacts
        {
            get
            {
                return this.contacts;
            }
            private set
            {
                if (this.contacts != value)
                {
                    this.contacts = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand ItemTapCommand
        {
            get
            {
                return this.itemTapCommand;
            }
            private set
            {
                if (this.itemTapCommand != value)
                {
                    this.itemTapCommand = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand SendInvitesCommand
        {
            get
            {
                return this.sendInvitesCommand;
            }
            private set
            {
                if (this.sendInvitesCommand != value)
                {
                    this.sendInvitesCommand = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand ClosePopupCommand
        {
            get
            {
                return this.closePopupCommand;
            }
            private set
            {
                if (this.closePopupCommand != value)
                {
                    this.closePopupCommand = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand GoBackCommand
        {
            get
            {
                return this.goBackCommand;
            }
            private set
            {
                if (this.goBackCommand != value)
                {
                    this.goBackCommand = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ImageSource FirstSelectedPersonImage
        {
            get
            {
                return this.firstSelectedPersonImage;
            }
            private set
            {
                if (this.firstSelectedPersonImage != value)
                {
                    this.firstSelectedPersonImage = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ImageSource SecondSelectedPersonImage
        {
            get
            {
                return this.secondSelectedPersonImage;
            }
            private set
            {
                if (this.secondSelectedPersonImage != value)
                {
                    this.secondSelectedPersonImage = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsSecondSelectedPersonVisible
        {
            get
            {
                return this.isSecondSelectedPersonVisible;
            }
            private set
            {
                if (this.isSecondSelectedPersonVisible != value)
                {
                    this.isSecondSelectedPersonVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsAdditionalSelectedPersonsCountVisible
        {
            get
            {
                return this.isAdditionalSelectedPersonsCountVisible;
            }
            private set
            {
                if (this.isAdditionalSelectedPersonsCountVisible != value)
                {
                    this.isAdditionalSelectedPersonsCountVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int AdditionalSelectedPersonsCount
        {
            get
            {
                return this.additionalSelectedPersonsCount;
            }
            private set
            {
                if (this.additionalSelectedPersonsCount != value)
                {
                    this.additionalSelectedPersonsCount = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private IEnumerable<ContactViewModel> GetSampleFiles()
        {
            Random rnd = new Random();

            for (int i = 0; i < 20; i++)
            {
                int nameIndex = rnd.Next(0, Names.Length);
                int imageIndex = rnd.Next(0, ImagePaths.Length);

                yield return new ContactViewModel(Names[nameIndex], ImagePaths[imageIndex]);
            }
        }

        private void ItemTap(object obj)
        {
            ItemTapEventArgs commandContext = (ItemTapEventArgs)obj;

            var contact = (ContactViewModel)commandContext.Item;

            contact.IsSelected = !contact.IsSelected;
        }

        private void SendInvites(object obj)
        {
            var selectedItems = this.Contacts.Where(p => p.IsSelected);

            if (!selectedItems.Any())
            {
                IToastMessageService toastService = DependencyService.Get<IToastMessageService>();
                toastService.ShortAlert("Please select at least one person");

                return;
            }

            var selectedItemsCount = selectedItems.Count();
            this.FirstSelectedPersonImage = selectedItems.First().Image;
            this.IsSecondSelectedPersonVisible = selectedItemsCount > 1;
            this.IsAdditionalSelectedPersonsCountVisible = false;
            if (this.IsSecondSelectedPersonVisible)
            {
                this.SecondSelectedPersonImage = selectedItems.Skip(1).First().Image;
                this.IsAdditionalSelectedPersonsCountVisible = selectedItemsCount > 2;
                this.AdditionalSelectedPersonsCount = selectedItemsCount - 2;
            }

            MessagingCenter.Send(this, "ShowModal");
        }

        private void ClosePopup(object obj)
        {
            MessagingCenter.Send(this, "CloseModal");
        }

        private void GoBack(object obj)
        {
            MessagingCenter.Send(this, Messages.NavigateBack);
        }
    }
}