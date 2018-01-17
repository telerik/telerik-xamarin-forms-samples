using QSF.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Telerik.XamarinForms.Common;
using Xamarin.Forms;

namespace QSF.Services
{
    public class ThemesService : IThemesService
    {
        private const string ThemesAssemblyName = "Telerik.XamarinForms.Common";
        private IEnumerable<Theme> themes;

        public ThemesService()
        {
            this.SetTheme(this.Themes.First());
        }

        private IEnumerable<Theme> Themes
        {
            get
            {
                if (this.themes == null)
                {
                    IConfigurationService configurationService = DependencyService.Get<IConfigurationService>();
                    this.themes = configurationService.GetThemes();
                }

                return this.themes;
            }
        }

        public Theme CurrentTheme { get; private set; }

        public IEnumerable<Theme> GetThemes()
        {
            return this.Themes;
        }

        public void SetTheme(Theme theme)
        {
            var resources = (RadResourceDictionary)Application.Current.Resources;
            var mergedDictionaries = resources.MergedDictionaries;

            if (this.CurrentTheme != null && !string.IsNullOrEmpty(this.CurrentTheme.ResourceTypeName))
            {
                var currentThemeType = GetThemeClassType(this.CurrentTheme.ResourceTypeName);
                var currentThemeResources = mergedDictionaries.Where(p => p.GetType() == currentThemeType).FirstOrDefault();
                mergedDictionaries.Remove(currentThemeResources);
            }

            if (!string.IsNullOrEmpty(theme.ResourceTypeName))
            {
                var type = GetThemeClassType(theme.ResourceTypeName);
                var instance = Activator.CreateInstance(type);
                mergedDictionaries.Add((ResourceDictionary)instance);
            }

            this.CurrentTheme = theme;
        }

        public Theme GetTheme(string name)
        {
            return this.themes.Single(p => p.Name == name);
        }

        private static Type GetThemeClassType(string themeResourceTypeName)
        {
            var assembly = Assembly.Load(new AssemblyName(ThemesAssemblyName));
            var type = assembly.ExportedTypes.Where(p => p.Name == themeResourceTypeName).FirstOrDefault();

            return type;
        }
    }
}
