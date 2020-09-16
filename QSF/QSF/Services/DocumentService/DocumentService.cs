using System;
using System.IO;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Common.FormatProviders;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.FormatProviders.Html;
using Telerik.Windows.Documents.Flow.FormatProviders.Rtf;
using Telerik.Windows.Documents.Flow.FormatProviders.Txt;
using Telerik.Windows.Documents.Flow.Model;

namespace QSF.Services
{
    public class DocumentService : IDocumentService
    {
        public DocumentType GetDocumentType(string filePath)
        {
            return ParseDocumentType(filePath);
        }

        public Task<string> OpenDocumentAsync(string filePath)
        {
            return Task.Run(() => OpenDocumentCore(filePath));
        }

        public Task<string> OpenDocumentAsync(string filePath, DocumentType documentType)
        {
            return Task.Run(() => OpenDocumentCore(filePath, documentType));
        }

        public Task<string> OpenDocumentAsync(Stream inputStream, DocumentType documentType)
        {
            return Task.Run(() => OpenDocumentCore(inputStream, documentType));
        }

        public Task SaveDocumentAsync(string htmlText, string filePath)
        {
            return Task.Run(() => SaveDocumentCore(htmlText, filePath));
        }

        public Task SaveDocumentAsync(string htmlText, string filePath, DocumentType documentType)
        {
            return Task.Run(() => SaveDocumentCore(htmlText, filePath, documentType));
        }

        public Task SaveDocumentAsync(string htmlText, Stream outputStream, DocumentType documentType)
        {
            return Task.Run(() => SaveDocumentCore(htmlText, outputStream, documentType));
        }

        private static string OpenDocumentCore(string filePath)
        {
            var documentType = ParseDocumentType(filePath);

            return OpenDocumentCore(filePath, documentType);
        }

        private static string OpenDocumentCore(string filePath, DocumentType documentType)
        {
            using (var inputStream = File.OpenRead(filePath))
            {
                return OpenDocumentCore(inputStream, documentType);
            }
        }

        private static string OpenDocumentCore(Stream inputStream, DocumentType documentType)
        {
            var importProvider = CreateFormatProvider(documentType);
            var exportProvider = new HtmlFormatProvider();
            var flowDocument = importProvider.Import(inputStream);

            //// Remove large left and hanging indentations of paragraphs in lists when importing from RTF
            foreach (Paragraph paragraph in flowDocument.EnumerateChildrenOfType<Paragraph>())
            {
                if (paragraph.ListId > -1)
                {
                    Telerik.Windows.Documents.Flow.Model.Lists.List list = flowDocument.Lists.GetList(paragraph.ListId);
                    if (paragraph.Indentation.HangingIndent == list.Levels[0].ParagraphProperties.HangingIndent.LocalValue)
                    {
                        paragraph.Indentation.HangingIndent = Paragraph.HangingIndentPropertyDefinition.DefaultValue.Value;
                        paragraph.Indentation.LeftIndent = Paragraph.LeftIndentPropertyDefinition.DefaultValue.Value;
                    }

                    if (paragraph.Indentation.FirstLineIndent == list.Levels[0].ParagraphProperties.FirstLineIndent.LocalValue)
                    {
                        paragraph.Indentation.FirstLineIndent = Paragraph.FirstLineIndentPropertyDefinition.DefaultValue.Value;
                    }
                }
            }

            return exportProvider.Export(flowDocument);
        }

        private static void SaveDocumentCore(string htmlText, string filePath)
        {
            var documentType = ParseDocumentType(filePath);

            SaveDocumentCore(htmlText, filePath, documentType);
        }

        private static void SaveDocumentCore(string htmlText, string filePath, DocumentType documentType)
        {
            var folderPath = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (var outputStream = File.Create(filePath))
            {
                SaveDocumentCore(htmlText, outputStream, documentType);
            }
        }

        private static void SaveDocumentCore(string htmlText, Stream outputStream, DocumentType documentType)
        {
            var importProvider = new HtmlFormatProvider();
            var exportProvider = CreateFormatProvider(documentType);
            var flowDocument = importProvider.Import(htmlText);

            exportProvider.Export(flowDocument, outputStream);
        }

        private static DocumentType ParseDocumentType(string filePath)
        {
            var fileExtension = Path.GetExtension(filePath);
            var fileType = fileExtension.TrimStart('.');

            DocumentType documentType;

            if (Enum.TryParse(fileType, true, out documentType))
            {
                return documentType;
            }

            var errorMessage = $"The file type \"{fileType}\" is not supported.";

            throw new NotSupportedException(errorMessage);
        }

        private static FormatProviderBase<RadFlowDocument> CreateFormatProvider(DocumentType documentType)
        {
            switch (documentType)
            {
                case DocumentType.Html:
                    return new HtmlFormatProvider();
                case DocumentType.Docx:
                    return new DocxFormatProvider();
                case DocumentType.Rtf:
                    return new RtfFormatProvider();
                case DocumentType.Txt:
                    return new TxtFormatProvider();
            }

            var errorMessage = $"The format \"{documentType}\" is not supported.";

            throw new NotSupportedException(errorMessage);
        }
    }
}
