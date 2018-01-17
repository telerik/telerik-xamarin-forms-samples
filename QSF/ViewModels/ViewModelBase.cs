using QSF.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QSF.ViewModels
{
    public abstract class ViewModelBase : BindableBase
    {
        internal virtual Task InitializeAsync(object parameter)
        {
            return Task.FromResult(false);
        }

        protected INavigationService NavigationService
        {
            get
            {
                return DependencyService.Get<INavigationService>();
            }
        }

        protected async Task NavigateBack()
        {
            if (this.NavigationService.CanNavigateBack)
            {
                await this.NavigationService.NavigateBackAsync();
            }
        }
    }
}
