using QSF.Converters;

namespace QSF.ViewModels
{
    public class ControlNameSearchResultViewModel : ViewModelBase
    {
        public ControlNameSearchResultViewModel(SearchResultType resultType, string controlName, HighlightedTextInfo highlightedTextInfo)
        {
            this.ResultType = resultType;
            this.ResultTypeText = EnumToStringConverter.Convert(this.ResultType);
            this.ControlName = controlName;
            this.HighlightedTextInfo = highlightedTextInfo;
        }

        public SearchResultType ResultType { get; }

        public string ResultTypeText { get; }

        public string ControlName { get; }

        public HighlightedTextInfo HighlightedTextInfo { get; }
    }
}
