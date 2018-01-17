namespace QSF.ViewModels
{
    public class ControlDescriptionSearchResultViewModel : ControlNameSearchResultViewModel
    {
        public ControlDescriptionSearchResultViewModel(SearchResultType resultType, string controlName, string description, HighlightedTextInfo highlightedTextInfo)
            : base(resultType, controlName, highlightedTextInfo)
        {
            this.Description = description;
        }


        public string Description { get; }
    }
}
