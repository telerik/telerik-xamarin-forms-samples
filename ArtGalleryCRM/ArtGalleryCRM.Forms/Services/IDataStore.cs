using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtGalleryCRM.Forms.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);

        Task<bool> UpdateItemAsync(T item);

        Task<bool> DeleteItemAsync(T item);

        Task<T> GetItemAsync(string id);

        Task<string> GetIdAsync(string name);

        Task<IReadOnlyList<T>> GetItemsAsync();

        Task ShowErrorMessageAsync(string errorMessage = null);
    }
}