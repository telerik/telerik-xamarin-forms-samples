using tagit.Common;
using tagit.Views;
using Xamarin.Forms;

namespace tagit
{
    public static class ViewExtensions
    {
        public static ContentView AsView(this PageType type)
        {
            ContentView view = null;

            switch (type)
            {
                case PageType.Analysis:
                    view = new AnalysisView();
                    break;
                case PageType.Favorites:
                    view = new FavoritesView();
                    break;
                case PageType.Gallery:
                    view = new GalleryView();
                    break;
                case PageType.Home:
                    view = new UploadView();
                    break;
                case PageType.Search:
                    view = new SearchResultsView();
                    break;
                case PageType.Timeline:
                    view = new TimelineView();
                    break;
            }

            return view;
        }
    }
}