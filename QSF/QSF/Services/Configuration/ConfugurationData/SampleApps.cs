using System.Collections.Generic;

namespace QSF.Services.Configuration
{
    public class SampleApps
    {
        public string SampleAppsPageTitle { get; set; }

        public string NavigateToSampleAppsText { get; set; }

        public string DescriptionHeader { get; set; }

        public List<SampleApp> Apps { get; set; }
    }
}
