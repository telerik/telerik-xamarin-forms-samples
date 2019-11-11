namespace QSF.ViewModels
{
    public class ExampleNameSearchResultViewModel : ControlNameSearchResultViewModel
    {
        public ExampleNameSearchResultViewModel(SearchResultType resultType, string controlName, string controlDisplayName, string exampleName, string exampleDisplayName, HighlightedTextInfo highlightedTextInfo)
            : base(resultType, controlName, controlDisplayName, highlightedTextInfo)
        {
            this.ExampleName = exampleName;
            this.ExampleDisplayName = exampleDisplayName;
        }

        internal string ExampleName { get; }

        public string ExampleDisplayName { get; }
    }
}
