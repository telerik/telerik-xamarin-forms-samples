namespace QSF.Services.XmlDocumentReader
{
    public class XmlDocumentElement : IXmlDocumentElement
    {
        public string NameAttribute
        {
            get;
            set;
        }

        public string ElementType
        {
            get;
            set;
        }

        public string CurrentPath
        {
            get;
            set;
        }

        public bool HasChildNodes
        {
            get;
            set;
        }
    }
}