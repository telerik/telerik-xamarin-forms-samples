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
        private bool hasNavigationInProgress;

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
            return this.NavigateToAsync<TViewModel>(null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter)
            where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToExampleAsync(ExampleInfo exampleInfo)
        {
            AnalyticsHelper.TraceNavigateToExample(exampleInfo);

            var viewModelType = typeof(ExampleViewModel);

            var page = this.CreatePage(viewModelType);

            var exampleViewModelType = this.GetExampleViewModelType(exampleInfo);
            if (exampleViewModelType != null)
            {
                viewModelType = exampleViewModelType;
            }

            return InternalNavigateToAsync(page, viewModelType, null, exampleInfo);
        }

        public Task NavigateToConfigurationAsync<T>(T configurationViewModel)
            where T : ConfigurationViewModel
        {
            var viewModelType = typeof(ConfigurationViewModel);

            var page = this.CreatePage(viewModelType);

            return InternalNavigateToAsync(page, viewModelType, configurationViewModel, null);
        }

        public Type GetExampleViewType(ExampleInfo exampleInfo)
        {
            AssemblyName assemblyName = this.GetAssemblyName();
            var typeName = string.Format("{0}.Examples.{1}Control.{2}Example.{3}View", assemblyName.Name, exampleInfo.ControlName, exampleInfo.ExampleName, exampleInfo.ExampleName);
            Type type = GetTypeFromTypeName(assemblyName, typeName);

            if (type == null)
            {
                throw new ArgumentException(string.Format("Missing view {0}", typeName));
            }

            return type;
        }

        public Type GetExampleViewModelType(ExampleInfo exampleInfo)
        {
            var assemblyName = this.GetAssemblyName();
            var typeName = string.Format("{0}.Examples.{1}Control.{2}Example.{3}ViewModel", assemblyName.Name, exampleInfo.ControlName, exampleInfo.ExampleName, exampleInfo.ExampleName);
            var type = GetTypeFromTypeName(assemblyName, typeName);

            return type;
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

        public Type GetViewTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);

            return viewType;
        }

        private Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            try
            {
                Page page = CreatePage(viewModelType);

                return this.InternalNavigateToAsync(page, viewModelType, null, parameter);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        private async Task InternalNavigateToAsync(Page page, Type viewModelType, ViewModelBase viewModel, object parameter)
        {
            if (this.hasNavigationInProgress)
            {
                return;
            }

            this.hasNavigationInProgress = true;

            try
            {
                if (page.BindingContext == null)
                {
                    if (viewModel == null)
                    {
                        viewModel = (ViewModelBase)Activator.CreateInstance(viewModelType);
                    }
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
            finally
            {
                this.hasNavigationInProgress = false;
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

        private static Type GetTypeFromTypeName(AssemblyName assemblyName, string typeName)
        {
            var fullTypeName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", typeName, assemblyName.FullName);
            var type = Type.GetType(fullTypeName);
            return type;
        }

        private AssemblyName GetAssemblyName()
        {
            var type = this.GetType();
            var assemblyName = type.GetTypeInfo().Assembly.GetName();
            return assemblyName;
        }
    }
}
