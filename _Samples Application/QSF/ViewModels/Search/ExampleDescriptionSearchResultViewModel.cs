namespace QSF.ViewModels
{
    public class ExampleDescriptionSearchResultViewModel : ExampleNameSearchResultViewModel
    {
        public ExampleDescriptionSearchResultViewModel(SearchResultType resultType, string controlName, string exampleName, string exampleDisplayName, string description, HighlightedTextInfo highlightedTextInfo)
            : base(resultType, controlName, exampleName, exampleDisplayName, highlightedTextInfo)
        {
            this.Description = description;
        }

        public string Description { get; }
    }
}
