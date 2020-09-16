using System.Threading.Tasks;

namespace QSF.Services
{
    public interface IFileShareService
    {
        Task ShareFileAsync(string filePath);
    }
}