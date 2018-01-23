using QSF.ViewModels;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QSF.Services
{
    public class NavigationService : INavigationService
    {
        public Task InitializeAsync()
        {
            return NavigateToAsync<HomeViewModel>();
        }

        public bool CanNavigateBack
        {
            get
            {
                var navigationPage = Application.Current.MainPage as NavigationPage;
                if (navigationPage == null)
                {
                    return false;
                }

                return navigationPage.Navigation.NavigationStack.Count > 0;
            }
        }

        public Task NavigateToAsync<TViewModel>()
            where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter)
            where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToExampleAsync(ExampleInfo exampleInfo)
        {
            var viewModelType = typeof(ExampleViewModel);

            return InternalNavigateToAsync(viewModelType, exampleInfo);
        }

        public Type GetExampleViewType(ExampleInfo exampleInfo)
        {
            var type = this.GetType();
            var assemblyName = type.GetTypeInfo().Assembly.GetName();
            var viewTypeName = string.Format("{0}.Examples.{1}Control.{2}Example.{3}View", assemblyName.Name, exampleInfo.ControlName, exampleInfo.ExampleName, exampleInfo.ExampleName);
            var fullViewTypeName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewTypeName, assemblyName.FullName);
            var viewType = Type.GetType(fullViewTypeName);

            if (viewType == null)
            {
                throw new ArgumentException(string.Format("Missing view {0}", viewTypeName));
            }

            return viewType;
        }

        public async Task NavigateBackAsync()
        {
            var navigationPage = Application.Current.MainPage as NavigationPage;
            if (navigationPage != null)
            {
                await navigationPage.Navigation.PopAsync();
            }
        }

        public Task RemovePreviousFromBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as NavigationPage;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as NavigationPage;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        private Type GetViewTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);

            return viewType;
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType);

            if (page.BindingContext == null)
            {
                var viewModel = (ViewModelBase)Activator.CreateInstance(viewModelType);
                await viewModel.InitializeAsync(parameter);

                page.BindingContext = viewModel;
            }

            var navigationPage = Application.Current.MainPage as NavigationPage;
            if (navigationPage != null)
            {
                await navigationPage.PushAsync(page);
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(page);
            }
        }

        private Page CreatePage(Type viewModelType)
        {
            Type pageType = GetViewTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            return Activator.CreateInstance(pageType) as Page;
        }
    }
}
