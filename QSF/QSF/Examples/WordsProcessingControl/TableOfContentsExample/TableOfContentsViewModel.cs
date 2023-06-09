using QSF.Examples.WordsProcessingControl.FindAndReplaceExample;
using QSF.Services;
using QSF.ViewModels;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Common.FormatProviders;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.FormatProviders.Pdf;
using Telerik.Windows.Documents.Flow.FormatProviders.Rtf;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.Model.Editing;
using Telerik.Windows.Documents.Flow.Model.Fields;
using Telerik.Windows.Documents.Flow.Model.Styles;
using Xamarin.Forms;
using Style = Telerik.Windows.Documents.Flow.Model.Styles.Style;

namespace QSF.Examples.WordsProcessingControl.TableOfContentsExample
{
    public class TableOfContentsViewModel : ExampleViewModel
    {
        private const string documentName = "TableOfContents.docx";
        private const string docxFileTypeName = "DOCX files(*.docx)";
        private const string pdfFileTypeName = "PDF files(*.pdf)";
        private const string rtfFileTypeName = "RTF files(*.rtf)";
        private const string exportDocumentName = "example";

        private readonly IFileViewerService fileViewerService;

        private RadFlowDocument document;

        private bool isDocumentLoaded;

        private readonly Command loadSampleCommand;
        private readonly Command exportCommand;
        private readonly Command insertAndUpdateTocFieldsCommand;
        private readonly Command insertAndUpdateToaFieldsCommand;

        private string fileName;

        private readonly ObservableCollection<string> exportFormats = new ObservableCollection<string> { docxFileTypeName, pdfFileTypeName, rtfFileTypeName };

        private bool includeHeadings;
        private string headingLevel;
        private bool includeTcFields;
        private string tcLevel;
        private bool includeCaptions;
        private string caption;
        private int categoryLevel;
        private bool includeCategoryHeader;
        private string selectedExportFormat;

        public TableOfContentsViewModel()
        {
            this.fileViewerService = DependencyService.Get<IFileViewerService>();
            this.SampleDocumentFile = documentName;

            this.exportCommand = new Command(this.Export, this.CanExecuteDocumentCommand);
            this.loadSampleCommand = new Command(this.OpenSample);
            this.insertAndUpdateTocFieldsCommand = new Command(this.InsertAndUpdateTocField, this.CanExecuteDocumentCommand);
            this.insertAndUpdateToaFieldsCommand = new Command(this.InsertAndUpdateToaField, this.CanExecuteDocumentCommand);

            this.includeHeadings = true;
            this.includeCategoryHeader = true;
            this.headingLevel = "1-9";
            this.tcLevel = "1-9";
            this.caption = "Image";
            this.categoryLevel = 1;
        }

        public string SampleDocumentFile { get; private set; }

        public RadFlowDocument Document
        {
            get
            {
                return this.document;
            }
            set
            {
                if (this.document != value)
                {
                    this.document = value;
                    this.OnPropertyChanged();
                    this.IsDocumentLoaded = this.document != null;
                }
            }
        }

        public bool IncludeHeadings
        {
            get
            {
                return this.includeHeadings;
            }
            set
            {
                if (this.includeHeadings != value)
                {
                    this.includeHeadings = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string HeadingLevel
        {
            get
            {
                return this.headingLevel;
            }
            set
            {
                if (this.headingLevel != value)
                {
                    this.headingLevel = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IncludeTcFields
        {
            get
            {
                return this.includeTcFields;
            }
            set
            {
                if (this.includeTcFields != value)
                {
                    this.includeTcFields = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string TcLevel
        {
            get
            {
                return this.tcLevel;
            }
            set
            {
                if (this.tcLevel != value)
                {
                    this.tcLevel = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IncludeCaptions
        {
            get
            {
                return this.includeCaptions;
            }
            set
            {
                if (this.includeCaptions != value)
                {
                    this.includeCaptions = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Caption
        {
            get
            {
                return this.caption;
            }
            set
            {
                if (this.caption != value)
                {
                    this.caption = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsDocumentLoaded
        {
            get
            {
                return this.isDocumentLoaded;
            }
            set
            {
                if (this.isDocumentLoaded != value)
                {
                    this.isDocumentLoaded = value;
                    this.OnPropertyChanged();
                    this.ExportCommand.ChangeCanExecute();
                    this.InsertAndUpdateTocFieldsCommand.ChangeCanExecute();
                    this.InsertAndUpdateToaFieldsCommand.ChangeCanExecute();
                }
            }
        }

        public int CategoryLevel
        {
            get
            {
                return this.categoryLevel;
            }
            set
            {
                if (this.categoryLevel != value)
                {
                    this.categoryLevel = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IncludeCategoryHeader
        {
            get
            {
                return this.includeCategoryHeader;
            }
            set
            {
                if (this.includeCategoryHeader != value)
                {
                    this.includeCategoryHeader = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Command LoadSampleCommand
        {
            get
            {
                return this.loadSampleCommand;
            }
        }

        public Command ExportCommand
        {
            get
            {
                return this.exportCommand;
            }
        }

        public Command InsertAndUpdateTocFieldsCommand
        {
            get
            {
                return this.insertAndUpdateTocFieldsCommand;
            }
        }

        public Command InsertAndUpdateToaFieldsCommand
        {
            get
            {
                return this.insertAndUpdateToaFieldsCommand;
            }
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                if (this.fileName != value)
                {
                    this.fileName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> ExportFormats
        {
            get
            {
                return this.exportFormats;
            }
        }

        public string SelectedExportFormat
        {
            get
            {
                return this.selectedExportFormat;
            }
            set
            {
                if (this.selectedExportFormat != value)
                {
                    this.selectedExportFormat = value;

                    this.OnPropertyChanged();
                }
            }
        }

        private void OpenSample()
        {
            this.Document = FindAndReplaceViewModel.OpenSample(this.SampleDocumentFile);
            this.FileName = this.SampleDocumentFile;
        }

        private bool CanExecuteDocumentCommand()
        {
            return this.IsDocumentLoaded;
        }

        private void InsertAndUpdateTocField()
        {
            if (this.Document != null)
            {
                RadFlowDocumentEditor editor = new RadFlowDocumentEditor(this.Document);

                string code = "TOC";

                if (this.IncludeHeadings)
                {
                    code += string.Format(" \\o \"{0}\"", this.HeadingLevel);
                }

                if (this.IncludeTcFields)
                {
                    code += string.Format(" \\l {0}", this.TcLevel);
                }

                if (this.IncludeCaptions)
                {
                    code += string.Format(" \\c \"{0}\"", this.Caption);
                }

                Paragraph firstParagraph = this.document.EnumerateChildrenOfType<Paragraph>().FirstOrDefault();
                editor.MoveToParagraphStart(firstParagraph);
                Run title = editor.InsertText("Table of Contents:");
                Style style = this.Document.StyleRepository.AddBuiltInStyle(BuiltInStyleNames.TocHeadingStyleId);
                title.Paragraph.StyleId = style.Id;

                editor.InsertParagraph();
                FieldInfo fieldInfo = editor.InsertField(code);

                editor.InsertParagraph();
                editor.InsertBreak(BreakType.PageBreak);

                fieldInfo.UpdateField();
            }
        }

        private void InsertAndUpdateToaField()
        {
            RadFlowDocumentEditor editor = new RadFlowDocumentEditor(this.document);

            string code = string.Format("TOA \\c \"{0}\"", this.CategoryLevel);

            if (this.IncludeCategoryHeader)
            {
                code += string.Format(" \\h");
            }

            Paragraph firstParagraph = this.document.EnumerateChildrenOfType<Paragraph>().FirstOrDefault();
            editor.MoveToParagraphStart(firstParagraph);
            editor.InsertParagraph();
            FieldInfo fieldInfo = editor.InsertField(code);

            editor.InsertParagraph();
            editor.InsertBreak(BreakType.PageBreak);

            fieldInfo.UpdateField();
        }

        private async void Export()
        {
            string selectedFormat = this.SelectedExportFormat;
            await this.SaveDocument(this.Document, selectedFormat);
        }

        public async Task<bool> SaveDocument(RadFlowDocument document, string selectedFormat)
        {
            IFormatProvider<RadFlowDocument> formatProvider = null;
            string exampleName = null;
            switch (selectedFormat)
            {
                case pdfFileTypeName:
                    formatProvider = new PdfFormatProvider();
                    exampleName = exportDocumentName + ".pdf";
                    break;
                case rtfFileTypeName:
                    formatProvider = new RtfFormatProvider();
                    exampleName = exportDocumentName + ".rtf";
                    break;
                case docxFileTypeName:
                    formatProvider = new DocxFormatProvider();
                    exampleName = exportDocumentName + ".docx";
                    break;
            }

            if (formatProvider == null)
            {
                return false;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                formatProvider.Export(document, stream);
                stream.Seek(0, SeekOrigin.Begin);
                await this.fileViewerService.View(stream, exampleName);
                return true;
            }
        }
    }
}
