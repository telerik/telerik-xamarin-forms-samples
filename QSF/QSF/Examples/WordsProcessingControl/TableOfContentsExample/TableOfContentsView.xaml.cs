using QSF.Examples.WordsProcessingControl.Converters;
using QSF.Examples.WordsProcessingControl.NumberingFieldsExample;
using Telerik.Windows.Documents.Extensibility;
using Telerik.Windows.Documents.Flow.Extensibility;
using Telerik.Windows.Documents.Flow.FormatProviders.Pdf;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.WordsProcessingControl.TableOfContentsExample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TableOfContentsView : ContentView
	{
		public TableOfContentsView ()
		{
            FlowExtensibilityManager.NumberingFieldsProvider = new NumberingFieldsProvider();
            FixedExtensibilityManager.FontsProvider = new FontsProvider();
            FixedExtensibilityManager.JpegImageConverter = new SkiaImageConverter();

            InitializeComponent ();
		}
	}
}