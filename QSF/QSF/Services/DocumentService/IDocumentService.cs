using System.IO;
using System.Threading.Tasks;

namespace QSF.Services
{
    public interface IDocumentService
    {
        DocumentType GetDocumentType(string filePath);
        Task<string> OpenDocumentAsync(string filePath);
        Task<string> OpenDocumentAsync(string filePath, DocumentType documentType);
        Task<string> OpenDocumentAsync(Stream inputStream, DocumentType documentType);
        Task SaveDocumentAsync(string htmlText, string filePath);
        Task SaveDocumentAsync(string htmlText, string filePath, DocumentType documentType);
        Task SaveDocumentAsync(string htmlText, Stream outputStream, DocumentType documentType);
    }
}
