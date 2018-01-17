using QSF.ViewModels;
using System;
using System.Threading.Tasks;

namespace QSF.Services
{
    public interface INavigationService
    {
        bool CanNavigateBack { get; }

        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task NavigateBackAsync();

        Task RemovePreviousFromBackStackAsync();

        Task RemoveBackStackAsync();

        Task NavigateToExampleAsync(ExampleInfo exampleInfo);

        Type GetExampleViewType(ExampleInfo exampleInfo);
    }
}
