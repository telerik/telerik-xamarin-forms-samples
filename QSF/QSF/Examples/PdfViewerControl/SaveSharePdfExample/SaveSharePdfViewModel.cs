using QSF.Services;
using QSF.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QSF.Examples.PdfViewerControl.SaveSharePdfExample
{
    public class SaveSharePdfViewModel : ExampleViewModel
    {
        public SaveSharePdfViewModel()
        {
            this.ShareDocumentCommand = new Command(this.ShareDocument);
            this.SaveDocumentCommand = new Command(this.SaveDocument);
        }
        public RadFixedDocument Document { get; set; }

        public ICommand SaveDocumentCommand { get; set; }

        public ICommand ShareDocumentCommand { get; set; }

        private async void ShareDocument(object obj)
        {
            await ShareAsync();
        }

        private async void SaveDocument(object obj)
        {
            await SaveAsync();
        }

        private async Task SaveAsync()
        {
            var fileName = "pdf-telerik.pdf";
            var localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var filePath = Path.Combine(localFolder, fileName);
            
            if (this.Document == null)
            {
                return;
            }
            
            using (Stream output = File.OpenWrite(filePath))
            {
                var provider = new PdfFormatProvider();
                provider.Export(this.Document, output);
            }
            await Application.Current.MainPage.DisplayAlert("Saved on this device as pdf-telerik.pdf.", "Location: " + filePath, "OK");
        }

        private async Task ShareAsync()
        {
            Assembly assembly = typeof(SaveSharePdfView).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("pdfviewer-firstlook.pdf"));
            Stream stream = assembly.GetManifestResourceStream(fileName);

            var cacheFile = Path.Combine(FileSystem.CacheDirectory, "pdfviewer-firstlook.pdf");
            using (var file = new FileStream(cacheFile, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(file);
            }

            var fileShareService = DependencyService.Get<IFileShareService>();
            await fileShareService.ShareFileAsync(cacheFile);
        }
    }
}
