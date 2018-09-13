using System.Collections.Generic;
using System.Reflection;
using Telerik.XamarinForms.Input.DataForm;

namespace QSF.Examples.DataGridControl.CRUDOperationsExample
{
    public class OrderMetadataProvider : PropertyMetadataProvider
    {
        private HashSet<string> registeredProperties;

        public OrderMetadataProvider(HashSet<string> registeredProperties)
        {
            this.registeredProperties = registeredProperties;
        }

        protected override IEnumerable<PropertyInfo> GetProperties(object source)
        {
            List<PropertyInfo> properties = new List<PropertyInfo>();

            foreach (PropertyInfo propertyInfo in base.GetProperties(source))
            {
                if (this.registeredProperties.Contains(propertyInfo.Name))
                {
                    properties.Add(propertyInfo);
                }
            }

            return properties;
        }
    }
}
