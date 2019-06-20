using QSF.Services.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QSF.Services
{
    public interface IConfigurationService
    {
        Task InitializeAsync();

        SplashScreen GetSplashScreenConfiguration();

        string GetSideDrawerHeaderTitle();

        string GetSideDrawerSubHeaderTitle();

        string GetSideDrawerHeaderIcon();

        string GetHomePageTitle();

        string GetQSFAboutHeader();

        string GetQSFAboutLinkText();

        string GetQSFAboutImageName();

        string GetQSFAboutContent();

        string GetHomePageTitleIcon();

        string GetSourceURL();

        string GetDocumentationURL();

        string GetProductPageURL();

        string GetWhatsNewPageURL();

        IEnumerable<Control> GetControlsConfiguration();

        IEnumerable<Theme> GetThemes();
    }
}
