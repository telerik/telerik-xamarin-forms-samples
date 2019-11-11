using Foundation;
using QSF.iOS.Services.FileViewer;
using QSF.Services;
using QuickLook;
using System;
using System.IO;
using System.Threading.Tasks;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(FileViewerService))]
namespace QSF.iOS.Services.FileViewer
{
    public class FileViewerService : IFileViewerService
    {
        public Task<bool> View(Stream stream, string filename)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                string filePath = Path.Combine(path, filename);

                using (FileStream fileStream = File.Open(filePath, FileMode.Create))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }

                UIViewController currentController = UIApplication.SharedApplication.KeyWindow.RootViewController;

                while (currentController.PresentedViewController != null)
                {
                    currentController = currentController.PresentedViewController;
                }

                UIView currentView = currentController.View;

                QLPreviewController qlPreview = new QLPreviewController();

                QLPreviewItem item = new QLPreviewItemBundle(filename, filePath);

                qlPreview.DataSource = new PreviewControllerDS(item);

                currentController.PresentViewController(qlPreview, true, null);

                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
    }

    public class QLPreviewItemBundle : QLPreviewItem
    {
        private string fileName;
        private string filePath;

        public QLPreviewItemBundle(string fileName, string filePath)
        {
            this.fileName = fileName;
            this.filePath = filePath;
        }

        public override string ItemTitle
        {
            get
            {
                return this.fileName;
            }
        }

        public override NSUrl ItemUrl
        {
            get
            {
                var documents = NSBundle.MainBundle.BundlePath;
                var lib = Path.Combine(documents, this.filePath);
                var url = NSUrl.FromFilename(lib);
                return url;
            }
        }
    }

    public class PreviewControllerDS : QLPreviewControllerDataSource
    {
        private QLPreviewItem item;

        public PreviewControllerDS(QLPreviewItem item)
        {
            this.item = item;
        }

        public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
        {
            return this.item;
        }

        public override nint PreviewItemCount(QLPreviewController controller)
        {
            return 1;
        }
    }
}