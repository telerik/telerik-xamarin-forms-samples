using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.PdfViewerControl.CustomizationExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomizationView : ContentView
    {
        public CustomizationView()
        {
            InitializeComponent();

            Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
            {
                Assembly assembly = typeof(CustomizationView).Assembly;
                string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("pdfviewer-Customization.pdf"));
                Stream stream = assembly.GetManifestResourceStream(fileName);
                return stream;
            });
            this.pdfViewer.Source = streamFunc;
        }
    }
}