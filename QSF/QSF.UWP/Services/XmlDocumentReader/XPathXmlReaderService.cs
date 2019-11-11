using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Xamarin.Forms;
using QSF.Services;
using QSF.Services.XmlDocumentReader;
using QSF.UWP.Services.XmlDocumentReader;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(XPathXmlReaderService))]
namespace QSF.UWP.Services.XmlDocumentReader
{
    public class XPathXmlReaderService : IXmlDocumentReader
    {
        readonly static string NODE_NAME_ATTRIBUTE = "Name";
        XmlDocument doc = null;

        public IList<IXmlDocumentElement> GetChildNodes(IXmlDocumentElement parent)
        {
            List<IXmlDocumentElement> result = new List<IXmlDocumentElement>();
            XmlNode folder = this.NavigateToPath(parent.CurrentPath);

            foreach (XmlNode child in folder.ChildNodes)
            {
                result.Add(new XmlDocumentElement()
                {
                    ElementType = child.Name,
                    CurrentPath = parent.CurrentPath + @"\" + parent.NameAttribute,
                    HasChildNodes = child.HasChildNodes,
                    NameAttribute = ((XmlElement)child).GetAttribute(NODE_NAME_ATTRIBUTE)
                });
            }
            return result;
        }

        async public Task<IXmlDocumentElement> GetRootChildAsync(string filePath)
        {
            await this.EnsureDocumentIsReadyAsync(filePath);
            XmlNode root = this.doc.FirstChild;
            var element = new XmlDocumentElement()
            {
                ElementType = root.Name,
                NameAttribute = ((XmlElement)root).GetAttribute(NODE_NAME_ATTRIBUTE),
                CurrentPath = @"\",
                HasChildNodes = root.HasChildNodes
            };
            return element;
        }

        public IXmlDocumentElement SelectSingleNode(string nodePathToReturn)
        {
            XmlElement folder = this.NavigateToPath(nodePathToReturn);
            var node = new XmlDocumentElement();
            node.HasChildNodes = folder.HasChildNodes;
            node.NameAttribute = folder.GetAttribute(NODE_NAME_ATTRIBUTE);
            node.CurrentPath = nodePathToReturn;
            node.ElementType = folder.Name;
            return node;
        }

        async private Task EnsureDocumentIsReadyAsync(string filePath)
        {
            if (doc == null)
            {
                string xml = await ReadFileAsync(filePath);
                doc = new XmlDocument();
                doc.LoadXml(xml);
            }
        }

        private XmlElement NavigateToPath(string nodePathToReturn)
        {
            string[] splitPath = nodePathToReturn.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            XmlNode folder = doc.FirstChild;
            for (int i = 0; i < splitPath.Length; i++)
            {
                var xpath = "Folder[@Name = \"" + splitPath[i] + "\"]";
                folder = folder.SelectSingleNode(xpath);
            }
            return (XmlElement)folder;
        }

        async private Task<string> ReadFileAsync(string filePath)
        {
            var resourceService = DependencyService.Get<IResourceService>();
            string xml = string.Empty;
            Stream fileStream = resourceService.GetResourceStream(filePath);
            var reader = new StreamReader(fileStream);
            xml = await reader.ReadToEndAsync();
            return xml;
        }
    }
}