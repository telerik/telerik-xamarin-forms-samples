using System;
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
        private ICommand navigateToInfoCommand;

        internal sealed override Task InitializeAsync(object parameter)
        {
            var navigateToInfoCommand = new Command(async () => await this.NavigateToInfo());
            var navigateToConfigurationCommand = new Command(async () => await this.NavigateToConfiguration());

            this.AppBarLeftButtonCommand = new Command(async () => await this.NavigateBack());
            this.AppBarMiddleButtonCommand = this.HasConfiguration ? navigateToConfigurationCommand : navigateToInfoCommand;
            this.AppBarRightButtonCommand = new Command(this.ToggleIsSideDrawerOpen);

            this.NavigateToThemesCommand = new Command(async () => await this.NavigateToThemes());
            this.NavigateToDocumentationCommand = new Command(async () => await this.NavigateToDocumentation());
            this.NavigateToCodeCommand = new Command(async () => await this.NavigateToCode());
            this.NavigateToInfoCommand = navigateToInfoCommand;

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

        public ICommand NavigateToInfoCommand
        {
            get
            {
                return this.navigateToInfoCommand;
            }
            private set
            {
                if (this.navigateToInfoCommand != value)
                {
                    this.navigateToInfoCommand = value;
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

        public virtual bool HasConfiguration
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsPopupHintOpen
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

        protected virtual Task NavigateToConfigurationOverride()
        {
            return Task.FromResult(false);
        }

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

        private Task NavigateToConfiguration()
        {
            this.IsSideDrawerOpen = false;
            return this.NavigateToConfigurationOverride();
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
