namespace QSF.ViewModels
{
    public class ExampleNameSearchResultViewModel : ControlNameSearchResultViewModel
    {
        public ExampleNameSearchResultViewModel(SearchResultType resultType, string controlName, string exampleName, HighlightedTextInfo highlightedTextInfo)
            : base(resultType, controlName, highlightedTextInfo)
        {
            this.ExampleName = exampleName;
        }

        public string ExampleName { get; }
    }
}
