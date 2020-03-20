using QSF.Services.Configuration;
using QSF.Services.Serialization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QSF.Services
{
    public class XmlConfigurationService : IConfigurationService
    {
        private const string XmlPath = "config.xml";
        private readonly object lockObject = new object();
        private QSFConfiguration configuration;

        public Task InitializeAsync()
        {
            lock (this.lockObject)
            {
                var resourceService = DependencyService.Get<IResourceService>();
                var serializationService = DependencyService.Get<ISerializationService>();

                var stream = resourceService.GetResourceStream(XmlPath);
                this.configuration = serializationService.XmlDeserializeFromStream<QSFConfiguration>(stream);
            }

            this.FilterPlatformSpecificExamples();
            this.UpdateIsThemableInheritance();
            this.UpdateDisplayName();

            return Task.FromResult(false);
        }

        private void UpdateIsThemableInheritance()
        {
            foreach (var control in this.configuration.Controls)
            {
                if (control.IsThemable)
                {
                    continue;
                }

                foreach (var example in control.Examples)
                {
                    example.IsThemable = false;
                }
            }
        }

        private void FilterPlatformSpecificExamples()
        {
            foreach (var control in this.configuration.Controls)
            {
                List<Example> examplesToRemove = new List<Example>();
                foreach (var example in control.Examples)
                {
                    if (string.IsNullOrEmpty(example.ExcludeFrom))
                    {
                        continue;
                    }

                    var filter = example.ExcludeFrom.ToLowerInvariant();
                    var platform = Device.RuntimePlatform.ToLowerInvariant();
                    if (filter.Contains(platform))
                    {
                        examplesToRemove.Add(example);
                    }
                }

                foreach (var example in examplesToRemove)
                {
                    control.Examples.Remove(example);
                }
            }
        }

        private void UpdateDisplayName()
        {
            foreach (var control in this.configuration.Controls)
            {
                if (string.IsNullOrEmpty(control.DisplayName))
                {
                    control.DisplayName = control.Name.InsertSpacesInPascalCase();
                }

                foreach (var example in control.Examples)
                {
                    if (string.IsNullOrEmpty(example.DisplayName))
                    {
                        example.DisplayName = example.Name.InsertSpacesInPascalCase();
                    }
                }
            }
        }

        public SplashScreen GetSplashScreenConfiguration()
        {
            return this.configuration.SplashScreen;
        }

        public string GetSideDrawerHeaderTitle()
        {
            return this.configuration.SideDrawerHeaderTitle;
        }

        public string GetSideDrawerSubHeaderTitle()
        {
            return this.configuration.SideDrawerSubHeaderTitle;
        }

        public string GetSideDrawerHeaderIcon()
        {
            return this.configuration.SideDrawerHeaderIcon;
        }

        public string GetHomePageTitle()
        {
            return this.configuration.HomePageTitle;
        }

        public string GetQSFAboutHeader()
        {
            return this.configuration.QSFAboutHeader;
        }

        public string GetQSFAboutLinkText()
        {
            return this.configuration.QSFAboutLinkText;
        }

        public string GetQSFAboutImageName()
        {
            return this.configuration.QSFAboutImageName;
        }

        public string GetQSFAboutContent()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                return this.configuration.QSFAboutContentiOS;
            }
            else
            {
                return this.configuration.QSFAboutContent;
            }
        }

        public string GetHomePageTitleIcon()
        {
            return this.configuration.HomePageTitleIcon;
        }

        public IEnumerable<Control> GetControlsConfiguration()
        {
            return this.configuration.Controls;
        }

        public string GetSourceURL()
        {
            return this.configuration.SourceURL;
        }

        public string GetDocumentationURL()
        {
            return this.configuration.DocumentationURL;
        }

        public string GetProductPageURL()
        {
            return this.configuration.ProductPageURL;
        }

        public string GetWhatsNewPageURL()
        {
            return this.configuration.WhatsNewPageURL;
        }

        public string GetPrivacyPolicyPageURL()
        {
            return this.configuration.PrivacyPolicyPageURL;
        }

        public IEnumerable<Theme> GetThemes()
        {
            return this.configuration.Themes;
        }
    }
}
