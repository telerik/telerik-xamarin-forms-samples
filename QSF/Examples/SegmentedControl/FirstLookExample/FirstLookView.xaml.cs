using Xamarin.Forms;

namespace QSF.Examples.SegmentedControl.FirstLookExample
{
    public partial class FirstLookView : ContentView
    {
        public FirstLookView()
        {
            this.InitializeComponent();
            this.segmented.SetSegmentEnabled(2, false);
        }
    }
}