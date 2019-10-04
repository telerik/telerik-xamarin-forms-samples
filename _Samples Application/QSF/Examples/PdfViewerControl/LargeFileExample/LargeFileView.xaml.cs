using QSF.Services;
using QSF.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Fixed.Model.Actions;
using Telerik.XamarinForms.PdfViewer.Annotations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.PdfViewerControl.LargeFileExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LargeFileView : ContentView
    {
        public LargeFileView()
        {
            InitializeComponent();

            Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
            {
                Assembly assembly = typeof(LargeFileView).Assembly;
                string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("pdfviewer-large-file.pdf"));
                Stream stream = assembly.GetManifestResourceStream(fileName);
                return stream;
            });

            this.pdfViewer.Source = streamFunc;
        }

        private void PdfViewer_LinkAnnotationTapped(object sender, LinkAnnotationTappedEventArgs e)
        {
            UriAction uriAction = e.LinkAnnotation.Action as UriAction;
            if (uriAction == null)
            {
                return;
            }

            string uriString = uriAction.Uri.ToString();
            string qsfMarker = @"qsf:///";
            if (!uriString.StartsWith(qsfMarker))
            {
                return;
            }

            e.Handled = true;
            string link = uriString.Substring(qsfMarker.Length);
            string[] split = link.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length == 0)
            {
                return;
            }

            string controlName = split[0];
            INavigationService navigationService = DependencyService.Get<INavigationService>();

            if (split.Length > 1)
            {
                string exampleName = split[1];
                navigationService?.NavigateToExampleAsync(new ExampleInfo(controlName, exampleName));
            }
            else
            {
                navigationService?.NavigateToAsync<ControlExamplesViewModel>(controlName);
            }
        }
    }
}