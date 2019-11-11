using Telerik.XamarinForms.ShapefileReader;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.MapControl.HotelFloorPlanExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HotelFloorPlanView : ContentView
    {
        public HotelFloorPlanView()
        {
            InitializeComponent();

            this.BindingContext = new HotelFloorPlanViewModel();

            var rectDiff = Device.RuntimePlatform == Device.Android ? 0.01 : 0;
            var bestView = this.roomsLayer.GetBestView();
            var northWest = new Location(bestView.Northwest.Latitude + rectDiff, bestView.Northwest.Longitude + rectDiff);
            var southEast = new Location(bestView.Southeast.Latitude - rectDiff, bestView.Southeast.Longitude - rectDiff);
            var view = new LocationRect(northWest, southEast);
            this.map.SetView(view);
        }
    }
}