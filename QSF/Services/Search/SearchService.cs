using System.Collections.Generic;
using Xamarin.Forms;

namespace QSF.Services
{
    public class SearchService : ISearchService
    {
        public IEnumerable<SearchResult> Search(string text)
        {
            var controlService = DependencyService.Get<IControlsService>();

            var allControls = controlService.GetAllControls();

            var lowerText = text.ToLowerInvariant();

            foreach (var control in allControls)
            {
                var nameIndex = control.Name.ToLowerInvariant().IndexOf(lowerText);
                if (nameIndex >= 0)
                {
                    yield return new SearchResult(SearchResultType.Control, control.Name, null, nameIndex, nameIndex + text.Length);
                }

                var descriptionIndex = control.FullDescription.ToLowerInvariant().IndexOf(lowerText);
                if (descriptionIndex >= 0)
                {
                    yield return new SearchResult(SearchResultType.ControlDescription, control.Name, null, descriptionIndex, descriptionIndex + text.Length);
                }

                foreach (var example in control.Examples)
                {
                    var exampleNameIndex = example.Name.ToLowerInvariant().IndexOf(lowerText);
                    if (exampleNameIndex >= 0)
                    {
                        yield return new SearchResult(SearchResultType.Example, control.Name, example.Name, exampleNameIndex, exampleNameIndex + text.Length);
                    }

                    var exampleDescrioptionIndex = example.Description.ToLowerInvariant().IndexOf(lowerText);
                    if (exampleDescrioptionIndex >= 0)
                    {
                        yield return new SearchResult(SearchResultType.ExampleDescription, control.Name, example.Name, exampleDescrioptionIndex, exampleDescrioptionIndex + text.Length);
                    }
                }
            }
        }
    }
}
