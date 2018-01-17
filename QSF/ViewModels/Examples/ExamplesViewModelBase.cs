using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QSF.ViewModels
{
    public abstract class ExamplesViewModelBase : PageViewModelBase
    {
        private bool isSideDrawerOpen;
        private ICommand navigateToThemesCommand;
        private ICommand navigateToDocumentationCommand;
        private ICommand navigateToCodeCommand;
        private bool canChangeTheme;

        internal sealed override Task InitializeAsync(object parameter)
        {
            this.AppBarLeftButtonCommand = new Command(async () => await this.NavigateBack());
            this.AppBarMiddleButtonCommand = new Command(async () => await this.NavigateToInfo());
            this.AppBarRightButtonCommand = new Command(this.ToggleIsSideDrawerOpen);

            this.NavigateToThemesCommand = new Command(async () => await this.NavigateToThemes());
            this.NavigateToDocumentationCommand = new Command(async () => await this.NavigateToDocumentation());
            this.NavigateToCodeCommand = new Command(async () => await this.NavigateToCode());

            return this.InitializeAsyncOverride(parameter);
        }

        public bool IsSideDrawerOpen
        {
            get
            {
                return this.isSideDrawerOpen;
            }
            set
            {
                if (this.isSideDrawerOpen != value)
                {
                    this.isSideDrawerOpen = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand NavigateToThemesCommand
        {
            get
            {
                return this.navigateToThemesCommand;
            }
            private set
            {
                if (this.navigateToThemesCommand != value)
                {
                    this.navigateToThemesCommand = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand NavigateToDocumentationCommand
        {
            get
            {
                return this.navigateToDocumentationCommand;
            }
            private set
            {
                if (this.navigateToDocumentationCommand != value)
                {
                    this.navigateToDocumentationCommand = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand NavigateToCodeCommand
        {
            get
            {
                return this.navigateToCodeCommand;
            }
            private set
            {
                if (this.navigateToCodeCommand != value)
                {
                    this.navigateToCodeCommand = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public virtual bool HasCode
        {
            get
            {
                return true;
            }
        }

        public virtual bool HasDocumentation
        {
            get
            {
                return false;
            }
        }

        public bool CanChangeTheme
        {
            get
            {
                return this.canChangeTheme;
            }
            protected set
            {
                if (this.canChangeTheme != value)
                {
                    this.canChangeTheme = value;
                    this.OnPropertyChanged();
                }
            }
        }

        protected virtual Task InitializeAsyncOverride(object parameter)
        {
            return Task.FromResult(false);
        }

        protected virtual Task NavigateToDocumentationOverride()
        {
            return Task.FromResult(false);
        }

        protected abstract Task NavigateToInfoOverride();

        protected abstract Task NavigateToCodeOverride();

        private Task NavigateToDocumentation()
        {
            this.IsSideDrawerOpen = false;
            return this.NavigateToDocumentationOverride();
        }

        private Task NavigateToInfo()
        {
            this.IsSideDrawerOpen = false;
            return this.NavigateToInfoOverride();
        }

        private Task NavigateToCode()
        {
            this.IsSideDrawerOpen = false;
            return this.NavigateToCodeOverride();
        }

        private void ToggleIsSideDrawerOpen(object obj)
        {
            this.IsSideDrawerOpen = !this.IsSideDrawerOpen;
        }

        private Task NavigateToThemes()
        {
            this.IsSideDrawerOpen = false;
            return this.NavigationService.NavigateToAsync<ThemesViewModel>();
        }
    }
}
