using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.PdfViewerControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : ContentView
    {
        public FirstLookView()
        {
            InitializeComponent();

            Assembly assembly = typeof(FirstLookView).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("pdfviewer-firstlook.pdf"));
            this.pdfViewer.Source = assembly.GetManifestResourceStream(fileName);
        }
    }
}