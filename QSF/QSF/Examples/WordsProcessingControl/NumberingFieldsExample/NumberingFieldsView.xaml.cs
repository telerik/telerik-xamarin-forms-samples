using QSF.Examples.WordsProcessingControl.Converters;
using Telerik.Windows.Documents.Extensibility;
using Telerik.Windows.Documents.Flow.Extensibility;
using Telerik.Windows.Documents.Flow.FormatProviders.Pdf;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.WordsProcessingControl.NumberingFieldsExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NumberingFieldsView : ContentView
    {
        public NumberingFieldsView()
        {
            FlowExtensibilityManager.NumberingFieldsProvider = new NumberingFieldsProvider();
            FixedExtensibilityManager.FontsProvider = new FontsProvider();
            FixedExtensibilityManager.JpegImageConverter = new SkiaImageConverter();

            InitializeComponent();
        }
    }
}