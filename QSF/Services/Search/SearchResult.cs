namespace QSF.Services
{
    public class SearchResult
    {
        public SearchResult(SearchResultType resultType, string controlName, string exampleName, int firstCharIndex, int lastCharIndex)
        {
            this.ResultType = resultType;
            this.ControlName = controlName;
            this.ExampleName = exampleName;
            this.FirstCharIndex = firstCharIndex;
            this.LastCharIndex = lastCharIndex;
        }

        public SearchResultType ResultType { get; }

        public string ControlName { get; }

        public string ExampleName { get; }

        public int FirstCharIndex { get; }

        public int LastCharIndex { get; }
    }
}
