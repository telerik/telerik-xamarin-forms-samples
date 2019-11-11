using System.Collections.Generic;

namespace QSF.Services.Configuration
{
    public class SplashScreen
    {
        public string Title { get; set; }

        public string Icon { get; set; }

        public List<Slide> Slides { get; set; }
    }
}
