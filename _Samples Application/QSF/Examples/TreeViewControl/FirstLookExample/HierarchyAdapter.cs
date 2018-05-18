using System.Collections.Generic;
using Telerik.XamarinForms.Common;

namespace QSF.Examples.TreeViewControl.FirstLookExample
{
    public class HierarchyAdapter : IHierarchyAdapter
    {
        public IEnumerable<object> GetItems(object item)
        {
            var folder = (FolderViewModel)item;

            return folder.Folders;
        }

        public object GetItemAt(object item, int index)
        {
            var folder = (FolderViewModel)item;

            return folder.Folders[index];
        }
    }
}
