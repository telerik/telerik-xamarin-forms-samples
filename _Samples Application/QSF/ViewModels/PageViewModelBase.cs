using System.Windows.Input;

namespace QSF.ViewModels
{
    public abstract class PageViewModelBase : ViewModelBase
    {
        private string title;
        private string titleIcon;
        private ICommand appBarLeftButtonCommand;
        private ICommand appBarMiddleButtonCommand;
        private ICommand appBarRightButtonCommand;

        public string Title
        {
            get
            {
                return this.title;
            }
            protected set
            {
                if (this.title != value)
                {
                    this.title = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string TitleIcon
        {
            get
            {
                return this.titleIcon;
            }
            protected set
            {
                if (this.titleIcon != value)
                {
                    this.titleIcon = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand AppBarLeftButtonCommand
        {
            get
            {
                return this.appBarLeftButtonCommand;
            }
            protected set
            {
                if (this.appBarLeftButtonCommand != value)
                {
                    this.appBarLeftButtonCommand = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand AppBarMiddleButtonCommand
        {
            get
            {
                return this.appBarMiddleButtonCommand;
            }
            protected set
            {
                if (this.appBarMiddleButtonCommand != value)
                {
                    this.appBarMiddleButtonCommand = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand AppBarRightButtonCommand
        {
            get
            {
                return this.appBarRightButtonCommand;
            }
            protected set
            {
                if (this.appBarRightButtonCommand != value)
                {
                    this.appBarRightButtonCommand = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
