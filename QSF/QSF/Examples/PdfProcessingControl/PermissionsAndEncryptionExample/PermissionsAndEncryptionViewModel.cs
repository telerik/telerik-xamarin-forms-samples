using QSF.Services;
using QSF.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Model.Encryption;
using Telerik.Windows.Documents.Fixed.Model;
using Xamarin.Forms;

namespace QSF.Examples.PdfProcessingControl.PermissionsAndEncryptionExample
{
    public class PermissionsAndEncryptionViewModel : ExampleViewModel
    {
        private RadFixedDocument document;
        private PdfFormatProvider provider = new PdfFormatProvider();
        private readonly IFileViewerService fileViewerService;
        private string documentName = "PdfViewer.pdf";
        private string password = string.Empty;
        private bool isEditingRestricted;
        private bool isCopyingRestricted;
        private bool isAccessibilityCopyingRestricted = true;
        private PrintingPermissionType printingPermissionTypeSelection = PrintingPermissionType.None;
        private ChangingPermissionType changingPermissionTypeSelection = ChangingPermissionType.None;
        private EncryptionType encryptionTypeSelection = EncryptionType.RC4;

        public PermissionsAndEncryptionViewModel()
        {
            this.ExportCommand = new Command(this.ExportDocument, this.CanExport);

            this.fileViewerService = DependencyService.Get<IFileViewerService>();
            this.ImportDocument();
        }

        public string Password
        {
            get => this.password;
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    this.OnPropertyChanged();
                    this.ExportCommand?.ChangeCanExecute();
                }
            }
        }

        public bool IsEditingRestricted
        {
            get => this.isEditingRestricted;
            set
            {
                if (this.isEditingRestricted != value)
                {
                    this.isEditingRestricted = value;
                    this.OnPropertyChanged();
                    if (!this.isEditingRestricted)
                    {
                        this.PrintingPermissionTypeSelection = PrintingPermissionType.None;
                        this.ChangingPermissionTypeSelection = ChangingPermissionType.None;
                        this.IsAccessibilityCopyingRestricted = true;
                        this.IsCopyingRestricted = false;
                        this.Password = string.Empty;
                    }
                }
            }
        }

        public bool IsCopyingRestricted
        {
            get => this.isCopyingRestricted;
            set
            {
                if (this.isCopyingRestricted != value)
                {
                    this.isCopyingRestricted = value;
                    this.OnPropertyChanged();
                    if (this.isCopyingRestricted)
                    {
                        this.IsAccessibilityCopyingRestricted = true;
                    }
                }
            }
        }

        public bool IsAccessibilityCopyingRestricted
        {
            get => this.isAccessibilityCopyingRestricted;
            set
            {
                if (this.isAccessibilityCopyingRestricted != value)
                {
                    this.isAccessibilityCopyingRestricted = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Array PrintingPermissionTypes { get; set; } = Enum.GetValues(typeof(PrintingPermissionType));

        public Array ChangingPermissionTypes { get; set; } = Enum.GetValues(typeof(ChangingPermissionType));

        public Array EncryptionTypes { get; set; } = Enum.GetValues(typeof(EncryptionType));

        public PrintingPermissionType PrintingPermissionTypeSelection
        {
            get => this.printingPermissionTypeSelection;
            set
            {
                if (this.printingPermissionTypeSelection != value)
                {
                    this.printingPermissionTypeSelection = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ChangingPermissionType ChangingPermissionTypeSelection
        {
            get => this.changingPermissionTypeSelection;
            set
            {
                if (this.changingPermissionTypeSelection != value)
                {
                    this.changingPermissionTypeSelection = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public EncryptionType EncryptionTypeSelection
        {
            get => this.encryptionTypeSelection;
            set
            {
                if (this.encryptionTypeSelection != value)
                {
                    this.encryptionTypeSelection = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Command ExportCommand { get; set; }

        private void ImportDocument()
        {
            Assembly assembly = typeof(PermissionsAndEncryptionView).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains(this.documentName));

            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            {
                this.document = this.provider.Import(stream);
            }
        }

        private async void ExportDocument(object obj)
        {
            await this.ExportAsync();
        }

        private async Task ExportAsync()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                PdfFormatProvider pdfFormatProvider = new PdfFormatProvider();
                PdfExportSettings settings = new PdfExportSettings
                {
                    OwnerPassword = this.Password,
                    IsEncrypted = true,
                    UserAccessPermissions = new UserAccessPermissions()
                    {
                        Printing = this.PrintingPermissionTypeSelection,
                        Changing = this.ChangingPermissionTypeSelection,
                        Copying = this.GetCopyingPermissionType(),
                    },
                    EncryptionType = this.EncryptionTypeSelection,
                };

                pdfFormatProvider.ExportSettings = settings;
                pdfFormatProvider.Export(this.document, stream);

                stream.Seek(0, SeekOrigin.Begin);

                await this.fileViewerService.View(stream, "example.pdf");
            }
        }

        private bool CanExport(object sender)
        {
            return this.password.Length > 0;
        }

        private CopyingPermissionType GetCopyingPermissionType()
        {
            if (this.IsCopyingRestricted)
            {
                return CopyingPermissionType.Copying;
            }

            return this.IsAccessibilityCopyingRestricted ? CopyingPermissionType.TextAccess : CopyingPermissionType.None;
        }
    }
}
