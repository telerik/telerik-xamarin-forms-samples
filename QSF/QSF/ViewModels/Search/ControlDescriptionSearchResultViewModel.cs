namespace QSF.ViewModels
{
    public class ControlDescriptionSearchResultViewModel : ControlNameSearchResultViewModel
    {
        public ControlDescriptionSearchResultViewModel(SearchResultType resultType, string controlName, string controlDisplayName, string description, HighlightedTextInfo highlightedTextInfo)
            : base(resultType, controlName, controlDisplayName, highlightedTextInfo)
        {
            this.Description = description;
        }


        public string Description { get; }
    }
}
