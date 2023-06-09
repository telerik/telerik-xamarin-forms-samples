using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.PdfViewerControl.SaveSharePdfExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SaveSharePdfView : ContentView
    {
        public SaveSharePdfView()
        {
            InitializeComponent();
            Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
            {
                Assembly assembly = typeof(SaveSharePdfView).Assembly;
                string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("pdfviewer-firstlook.pdf"));
                Stream stream = assembly.GetManifestResourceStream(fileName);
                return stream;
            });
            this.pdfViewer.Source = streamFunc;
        }
    }
}