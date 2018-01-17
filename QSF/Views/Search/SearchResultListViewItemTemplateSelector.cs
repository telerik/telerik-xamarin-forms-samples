using QSF.ViewModels;
using System;
using Xamarin.Forms;

namespace QSF.Views
{
    public class SearchResultListViewItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ExampleDescriptionDataTemplate { get; set; }

        public DataTemplate ExampleNameDataTemplate { get; set; }

        public DataTemplate ControlDescriptionDataTemplate { get; set; }

        public DataTemplate ControlNameDataTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var exampleDescription = item as ExampleDescriptionSearchResultViewModel;
            if (exampleDescription != null)
            {
                return this.ExampleDescriptionDataTemplate;
            }

            var exampleName = item as ExampleNameSearchResultViewModel;
            if (exampleName != null)
            {
                return this.ExampleNameDataTemplate;
            }

            var controlDescription = item as ControlDescriptionSearchResultViewModel;
            if (controlDescription != null)
            {
                return this.ControlDescriptionDataTemplate;
            }

            var controlName = item as ControlNameSearchResultViewModel;
            if (controlName != null)
            {
                return this.ControlNameDataTemplate;
            }

            throw new ArgumentException("The view model should deliver from ControlNameSearchResultViewModel");
        }
    }
}
