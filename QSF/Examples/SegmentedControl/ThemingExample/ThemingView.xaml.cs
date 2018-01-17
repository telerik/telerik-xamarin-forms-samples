using Xamarin.Forms;

namespace QSF.Examples.SegmentedControl.ThemingExample
{
    public partial class ThemingView : ContentView
    {
        public ThemingView()
        {
            this.InitializeComponent();
            this.segmented.SetSegmentEnabled(2, false);
        }
    }
}