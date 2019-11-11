using tagit.Common;

namespace tagit.Models
{
    /// <summary>
    ///     Used to display SideDrawer menu items
    /// </summary>
    public class NavigationItem : ObservableBase
    {
        private string _icon;
        private string _label;

        private PageType _pageType;

        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public PageType PageType
        {
            get => _pageType;
            set => SetProperty(ref _pageType, value);
        }
    }
}