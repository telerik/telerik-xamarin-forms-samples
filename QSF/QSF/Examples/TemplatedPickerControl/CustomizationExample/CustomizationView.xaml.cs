using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.TemplatedPickerControl.CustomizationExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomizationView : ContentView
    {
        public CustomizationView()
        {
            this.InitializeComponent();
            this.BindingContext = new ViewModel();
        }
    }
}
