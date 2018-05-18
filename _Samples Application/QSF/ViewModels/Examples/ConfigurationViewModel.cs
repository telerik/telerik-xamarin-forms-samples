using QSF.Services;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QSF.ViewModels
{
    public abstract class ConfigurationViewModel : PageViewModelBase
    {
        private Dictionary<string, object> propertiesStore;

        public ConfigurationViewModel()
        {
            this.AppBarLeftButtonCommand = new Command(async () => await this.ApplyChanges());
            this.AppBarRightButtonCommand = new Command(async () => await this.DiscardChanges());

            this.propertiesStore = new Dictionary<string, object>();

            this.Title = "Configuration";
        }

        protected virtual void ApplyChangesOverride()
        {
        }

        protected virtual void DiscardChangesOverride()
        {
            this.RestoreInitialValues();
        }

        internal override void OnAppearing()
        {
            this.PreserveInitialValues();
            base.OnAppearing();
        }

        private void PreserveInitialValues()
        {
            this.propertiesStore.Clear();

            var typeInfo = this.GetType().GetTypeInfo();
            var properties = typeInfo.DeclaredProperties;

            foreach (var property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    var propertyName = property.Name;
                    var propertyValue = property.GetValue(this);

                    this.propertiesStore.Add(propertyName, propertyValue);
                }
            }
        }

        protected void RestoreInitialValues()
        {
            var typeInfo = this.GetType().GetTypeInfo();

            foreach (var pair in this.propertiesStore)
            {
                var propertyName = pair.Key;
                var propertyValue = pair.Value;

                var property = typeInfo.GetDeclaredProperty(pair.Key);
                property.SetValue(this, propertyValue);
            }
        }

        private Task ApplyChanges()
        {
            this.ApplyChangesOverride();
            return this.GoBack();
        }

        private Task DiscardChanges()
        {
            this.DiscardChangesOverride();
            return this.GoBack();
        }

        private Task GoBack()
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();

            return navigationService.NavigateBackAsync();
        }
    }
}
