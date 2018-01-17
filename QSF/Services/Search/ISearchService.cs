using System.Collections.Generic;

namespace QSF.Services
{
    public interface ISearchService
    {
        IEnumerable<SearchResult> Search(string text);
    }
}
