using QSF.Services.Configuration;
using System.Collections.Generic;

namespace QSF.Services
{
    public interface IControlsService
    {
        IEnumerable<Control> GetFeatured();

        IEnumerable<Control> GetAllControls();

        Control GetControlByName(string controlName);

        Example GetControlExample(string controlName, string exampleName);
    }
}
