using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSF.Services.XmlDocumentReader
{
    public interface IXmlDocumentElement
    {
        string NameAttribute
        {
            get; set;
        }

        string ElementType
        {
            get; set;

        }

        string CurrentPath
        {
            get; set;
        }

        bool HasChildNodes
        {
            get; set;
        }
    }
}
