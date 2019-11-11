using System.Collections.Generic;
using System.Threading.Tasks;
using tagit.Models;

namespace tagit.Services
{
    /// <summary>
    ///     Used to faciliate dependency injection for the
    ///     platform-specific SettingsStorageService
    /// </summary>
    public interface ISettingsStorageService
    {
        T Read<T>(string name, T defaultValue);
        void Write<T>(string name, T value);
        Task<List<ImageInformation>> ReadAsync(string name);
        Task WriteAsync(string name, List<ImageInformation> value);
    }
}