using QSF.Services.XmlDocumentReader;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QSF.Examples.PopupControl.ContextMenuExample
{
    public static class ExampleDataProvider
    {
        private static IXmlDocumentReader docReader;

        static ExampleDataProvider()
        {
            docReader = DependencyService.Get<IXmlDocumentReader>();
        }

        async public static Task<List<FileViewModel>> GetRootItems(string filePath)
        {
            var root = await docReader.GetRootChildAsync(filePath);
            return CreateViewModels(root);
        }

        public static List<FileViewModel> GetChildren(string filePath, string parentName, string nodePath)
        {
            string path;
            if (nodePath == @"\")
            {
                path = nodePath + parentName;
            }
            else
            {
                path = nodePath + @"\" + parentName;
            }

            var folder = docReader.SelectSingleNode(path);
            return CreateViewModels(folder);
        }

        private static List<FileViewModel> CreateViewModels(IXmlDocumentElement folder)
        {
            var result = new List<FileViewModel>();
            var childElements = docReader.GetChildNodes(folder);
            foreach (IXmlDocumentElement element in childElements)
            {
                if (element.ElementType.Equals("Folder"))
                {
                    result.Add(new FolderViewModel() { Name = element.NameAttribute, Path = folder.CurrentPath, HasUnrealizedChildren = element.HasChildNodes });
                }
                else
                {
                    result.Add(new FileViewModel() { Name = element.NameAttribute, Icon = GetFileIcon(element.CurrentPath) });
                }
            }
            return result;
        }

        private static string GetFileIcon(string currentPath)
        {
            string[] path = currentPath.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            string currentChar = "";
            foreach (string item in path)
            {
                if (item.Equals("Graphics"))
                {
                    currentChar = (string)Application.Current.Resources["GraphicIcon"];
                }
                else if (item.Equals("Fonts"))
                {
                    currentChar = (string)Application.Current.Resources["FontIcon"];
                }
                else if (item.Equals("Photos"))
                {
                    currentChar = (string)Application.Current.Resources["PhotoIcon"];
                }
                else if (item.Equals("Books"))
                {
                    currentChar = (string)Application.Current.Resources["BookIcon"];
                }
                else if (item.Equals("Music"))
                {
                    currentChar = (string)Application.Current.Resources["PlayIcon"];
                }
                else if (item.Equals("Wireframes"))
                {
                    currentChar = (string)Application.Current.Resources["WireframeIcon"];
                }
                else if (item.Equals("Design"))
                {
                    currentChar = (string)Application.Current.Resources["DesignIcon"];
                }
                else if (item.Equals("Assets"))
                {
                    currentChar = (string)Application.Current.Resources["AssetIcon"];
                }
            }
            return currentChar;
        }
    }
}
