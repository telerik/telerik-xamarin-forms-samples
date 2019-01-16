using QSF.Services.Toast;
using QSF.ViewModels;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace QSF.Examples.PopupControl.ContextMenuExample
{
    public class PopupViewModel :ViewModelBase
    {
        private ICommand addPeople;
        private ICommand linkShare;
        private ICommand copyLink;
        private ICommand rename;
        private ICommand remove;
        private IToastMessageService toastService;

        public PopupViewModel(Action closePopupAction)
        {
            this.toastService = DependencyService.Get<IToastMessageService>();

            Command closePopupAndShowToastCommand = new Command(p =>
            {
                closePopupAction();
                toastService.ShortAlert((string)p);
            });

            this.AddPeople = closePopupAndShowToastCommand;
            this.LinkShare = closePopupAndShowToastCommand;
            this.CopyLink = closePopupAndShowToastCommand;
            this.Rename = closePopupAndShowToastCommand;
            this.Remove = closePopupAndShowToastCommand;
        }

        public ICommand AddPeople
        {
            get
            {
                return this.addPeople;
            }
            private set
            {
                if (this.addPeople != value)
                {
                    this.addPeople = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand LinkShare
        {
            get
            {
                return this.linkShare;
            }
            private set
            {
                if (this.linkShare != value)
                {
                    this.linkShare = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand CopyLink
        {
            get
            {
                return this.copyLink;
            }
            private set
            {
                if (this.copyLink != value)
                {
                    this.copyLink = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand Rename
        {
            get
            {
                return this.rename;
            }
            private set
            {
                if (this.rename != value)
                {
                    this.rename = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand Remove
        {
            get
            {
                return this.remove;
            }
            private set
            {
                if (this.remove != value)
                {
                    this.remove = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
