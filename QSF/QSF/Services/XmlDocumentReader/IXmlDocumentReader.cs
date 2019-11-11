using System.Collections.Generic;
using System.Threading.Tasks;

namespace QSF.Services.XmlDocumentReader
{
    public interface IXmlDocumentReader
    {
        Task<IXmlDocumentElement> GetRootChildAsync(string filePath);
        IXmlDocumentElement SelectSingleNode(string nodePathToReturn);
        IList<IXmlDocumentElement> GetChildNodes(IXmlDocumentElement parent);
    }
}