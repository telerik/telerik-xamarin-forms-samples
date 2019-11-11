using System.Collections.Generic;

namespace QSF.Services.Configuration
{
    public class QSFConfiguration
    {
        public string SideDrawerHeaderTitle { get; set; }

        public string SideDrawerSubHeaderTitle { get; set; }

        public string SideDrawerHeaderIcon { get; set; }

        public string HomePageTitle { get; set; }

        public string HomePageTitleIcon { get; set; }

        public string QSFAboutHeader { get; set; }

        public string QSFAboutImageName { get; set; }

        public string QSFAboutLinkText { get; set; }

        public string QSFAboutContent { get; set; }

        public SplashScreen SplashScreen { get; set; }

        public string SourceURL { get; set; }

        public string DocumentationURL { get; set; }

        public string ProductPageURL { get; set; }

        public string WhatsNewPageURL { get; set; }

        public List<Control> Controls { get; set; }

        public List<Theme> Themes { get; set; }
    }
}
