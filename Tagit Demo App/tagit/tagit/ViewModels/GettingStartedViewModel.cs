using System.Collections.ObjectModel;
using tagit.Common;
using tagit.Helpers;
using tagit.Models;

namespace tagit.ViewModels
{
    /// <summary>
    /// View model to serve the Getting Started view
    /// </summary>
    public class GettingStartedViewModel : ObservableBase
    {
        public GettingStartedViewModel()
        {
            RefreshGettingStartedItems();
        }

        private ObservableCollection<GettingStartedInformation> _gettingStartedItems =
            new ObservableCollection<GettingStartedInformation>();
        
        public ObservableCollection<GettingStartedInformation> GettingStartedItems
        {
            get => _gettingStartedItems;
            set => SetProperty(ref _gettingStartedItems, value);
        }

        public void RefreshGettingStartedItems()
        {
            GettingStartedItems.Clear();

            foreach (var item in GettingStartedHelper.GettingStartedItems)
                GettingStartedItems.Add(item);
        }
    }
}