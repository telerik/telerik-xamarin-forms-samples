using QSF.Services.Configuration;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace QSF.Services
{
    public class ControlsService : IControlsService
    {
        public IEnumerable<Control> GetAllControls()
        {
            IConfigurationService configurationService = DependencyService.Get<IConfigurationService>();
            var controls = configurationService.GetControlsConfiguration().OrderBy(p => p.DisplayName);

            return controls;
        }

        public IEnumerable<Control> GetFeatured()
        {
            var controls = this.GetAllControls();

            return controls.Where(p => p.Featured > 0).OrderBy(p => p.Featured);
        }

        public Control GetControlByName(string controlName)
        {
            var controls = this.GetAllControls();

            return controls.Where(p => p.Name == controlName).FirstOrDefault();
        }

        public Example GetControlExample(string controlName, string exampleName)
        {
            var control = this.GetControlByName(controlName);
            return control.Examples.Where(p => p.Name == exampleName).FirstOrDefault();
        }
    }
}
