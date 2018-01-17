using QSF.Services.Configuration;
using System.Collections.Generic;

namespace QSF.Services
{
    public interface IThemesService
    {
        Theme CurrentTheme { get; }

        IEnumerable<Theme> GetThemes();

        void SetTheme(Theme theme);

        Theme GetTheme(string name);
    }
}
