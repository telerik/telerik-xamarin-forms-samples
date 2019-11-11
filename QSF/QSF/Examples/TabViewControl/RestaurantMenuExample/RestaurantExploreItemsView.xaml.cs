using System;
using Telerik.XamarinForms.DataControls;
using Telerik.XamarinForms.DataControls.ListView;
using Xamarin.Forms;

namespace QSF.Examples.TabViewControl.RestaurantMenuExample
{
    public partial class RestaurantExploreItemsView : ContentView
    {
        public static readonly BindableProperty IsSelectableProperty =
            BindableProperty.Create("IsSelectable", typeof(bool),
                typeof(RestaurantExploreItemsView), false);

        public bool IsSelectable
        {
            get
            {
                return (bool)this.GetValue(IsSelectableProperty);
            }
            set
            {
                this.SetValue(IsSelectableProperty, value);
            }
        }

        public RestaurantExploreItemsView()
        {
            this.InitializeComponent();
        }

        private void OnItemTapped(object sender, ItemTapEventArgs eventArgs)
        {
            if (this.IsSelectable)
            {
                var item = (RestaurantMenuItem)eventArgs.Item;

                item.IsSaved = !item.IsSaved;
            }
        }

        private void OnSizeChanged(object sender, EventArgs eventArgs)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                return;
            }

            var listView = (RadListView)sender;
            var listViewLayout = (ListViewGridLayout)listView.LayoutDefinition;
            var desiredColumnsCount = listViewLayout.SpanCount;
            var spacing = (desiredColumnsCount - 1) * listViewLayout.HorizontalItemSpacing;
            var availableWidth = listView.Width - spacing;
            var itemWidth = availableWidth / desiredColumnsCount;

            listViewLayout.ItemLength = 1.5 * itemWidth;
        }
    }
}
