using Telerik.XamarinForms.Map;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.MapControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : ContentView
    {
        public FirstLookView()
        {
            InitializeComponent();

            var souce = MapSource.FromResource("QSF.Examples.MapControl.FirstLookExample.usa.shp", typeof(FirstLookView));
            var dataSource = MapSource.FromResource("QSF.Examples.MapControl.FirstLookExample.usa.dbf", typeof(FirstLookView));
            this.shapeReader.Source = souce;
            this.shapeReader.DataSource = dataSource;

            this.BindingContext = new FirstLookViewModel();
        }

        private void OnReadCompleted(object sender, System.EventArgs e)
        {
            var bestView = this.shapeLayer.GetBestView();
            this.map.SetView(bestView);

            this.shapeReader.ReadCompleted -= this.OnReadCompleted;
        }

        private void OnSizeChanged(object sender, System.EventArgs e)
        {
            this.popup.HorizontalOffset = this.Width / 2 - this.popup.Content.WidthRequest / 2 - 24;
        }
    }
}