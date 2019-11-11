using TodoApp.Models;
using Xamarin.Forms;

namespace TodoApp.TemplateSelectors
{
    public class RadTreeViewItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CategoryTemplate { get; set; }
        public DataTemplate NewCategoryTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch (item)
            {
                case Category c when !c.IsNew:
                    return CategoryTemplate;
                case Category c when c.IsNew:
                    return NewCategoryTemplate;
            }
            return null;
        }
    }
}
