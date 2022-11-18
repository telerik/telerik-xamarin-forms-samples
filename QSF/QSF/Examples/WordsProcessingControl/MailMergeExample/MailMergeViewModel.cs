using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using QSF.Services;
using QSF.ViewModels;
using Telerik.Documents.Common.Model;
using Telerik.Documents.Core.Fonts;
using Telerik.Documents.Media;
using Telerik.Windows.Documents.Common.FormatProviders;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.FormatProviders.Html;
using Telerik.Windows.Documents.Flow.FormatProviders.Rtf;
using Telerik.Windows.Documents.Flow.FormatProviders.Txt;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.Model.Collections;
using Telerik.Windows.Documents.Flow.Model.Editing;
using Telerik.Windows.Documents.Flow.Model.Fields;
using Telerik.Windows.Documents.Flow.Model.Styles;
using Telerik.Windows.Documents.Media;
using Telerik.Windows.Documents.Primitives;
using Xamarin.Forms;
using DPSize = Telerik.Documents.Primitives.Size;
using DPStyle = Telerik.Windows.Documents.Flow.Model.Styles.Style;
using Model = Telerik.Windows.Documents.Model;

namespace QSF.Examples.WordsProcessingControl.MailMergeExample
{
    public class MailMergeViewModel : ExampleViewModel
    {
        private string selectedExportFormat;
        private readonly IFileViewerService fileViewerService;
        private ICommand mailMergeCommand = null;

        public string SelectedExportFormat
        {
            get
            {
                return this.selectedExportFormat;
            }
            set
            {
                if (!object.Equals(this.selectedExportFormat, value))
                {
                    this.selectedExportFormat = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public IEnumerable<string> ExportFormats { get; } = new string[] { "Docx", "Rtf", "Html", "Txt" };

        public ICommand MailMergeCommand
        {
            get
            {
                return this.mailMergeCommand;
            }
            set
            {
                if (this.mailMergeCommand != value)
                {
                    this.mailMergeCommand = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public MailMergeViewModel()
        {
            this.MailMergeCommand = new Command(async () => await this.MailMerge());
            this.fileViewerService = Xamarin.Forms.DependencyService.Get<IFileViewerService>();
            this.SelectedExportFormat = this.ExportFormats.ElementAt(0);
        }

        private static RadFlowDocument GenerateTemplate()
        {
            RadFlowDocument document = new RadFlowDocument();
            RadFlowDocumentEditor editor = new RadFlowDocumentEditor(document);

            Section section = document.Sections.AddSection();
            UpdatePageLayout(section);

            UpdateNormalStyle(document);

            editor.InsertText("Hello ");
            InsertMergeField(editor, "ClientName");
            editor.InsertText(",");

            Paragraph paragraph = editor.InsertParagraph();
            paragraph.Properties.SpacingBefore.LocalValue = Unit.PointToDip(18);
            editor.InsertText("We are happy to inform that your order ");
            InsertMergeField(editor, "OrderID", @"\b #");
            editor.InsertText(" has been sent for delivery. You can find information for your purchase below:");

            const int rowsCount = 3;
            const int columnsCount = 4;
            Table table = editor.InsertTable(rowsCount, columnsCount);
            TableCellCollection cells = table.Rows[0].Cells;
            AddTextToHeaderRowCell(cells[0], "Product Name");
            AddTextToHeaderRowCell(cells[1], "Price Per Unit");
            AddTextToHeaderRowCell(cells[2], "Quantity");
            AddTextToHeaderRowCell(cells[3], "Final Price");

            cells = table.Rows[1].Cells;

            string[] mergeFieldsValues = new string[] { "GroupStart:Products", "ProductName" };
            AddMergeFieldsToTableCell(cells[0], Alignment.Left, mergeFieldsValues);

            mergeFieldsValues = new string[] { "PricePerUnit" };
            string[] switches = new string[] { @"\b $" };
            AddMergeFieldsToTableCell(cells[1], Alignment.Right, mergeFieldsValues, switches);

            mergeFieldsValues = new string[] { "Quantity" };
            AddMergeFieldsToTableCell(cells[2], Alignment.Center, mergeFieldsValues);

            mergeFieldsValues = new string[] { "FinalPrice", "GroupEnd:Products" };
            switches = new string[] { @"\b $", "" };
            AddMergeFieldsToTableCell(cells[3], Alignment.Right, mergeFieldsValues, switches);

            cells = table.Rows[2].Cells;
            TableCell cell = cells[0];
            cell.ColumnSpan = 3;
            cell.Row.Cells.RemoveRange(2, 2);

            AddTextToTableCell(cell, "Total Price:", Alignment.Right);

            mergeFieldsValues = new string[] { "TotalPrice" };
            switches = new string[] { @"\b $" };
            AddMergeFieldsToTableCell(cells[1], Alignment.Right, mergeFieldsValues, switches);
            paragraph = cells[1].Blocks.OfType<Paragraph>().First();
            ApplyFontWeightToFieldResultOnly(paragraph, FontWeights.Bold);

            UpdateTableProperties(table);

            editor.MoveToTableEnd(table);
            paragraph = editor.InsertParagraph();
            paragraph.Properties.SpacingBefore.LocalValue = Unit.PointToDip(12);
            editor.InsertText("In case you need more information, or you find some discrepancies with your order, you can call us on:");
            editor.InsertBreak(BreakType.LineBreak);
            InsertMergeField(editor, "PhoneNumber");
            editor.InsertText(".");

            editor.InsertParagraph();
            editor.InsertText("We hope you will enjoy your meal.");

            paragraph = editor.InsertParagraph();
            paragraph.Properties.SpacingBefore.LocalValue = Unit.PointToDip(24);
            editor.InsertText("Regards,");

            editor.InsertParagraph();
            editor.InsertText("The staff of Pizzeria Random Name");

            return document;
        }

        private static void UpdateTableProperties(Table table)
        {
            RemoveSpacingAfterAndLineSpacingInsideTable(table);

            Border border = new Border(1.0, BorderStyle.Single, new ThemableColor(Colors.Black));
            table.Borders = new TableBorders(border);

            table.PreferredWidth = new TableWidthUnit(TableWidthUnitType.Percent, 100.0);

            TableCellCollection cells = table.Rows[0].Cells;
            cells[0].PreferredWidth = new TableWidthUnit(TableWidthUnitType.Percent, 54.0);
            cells[1].PreferredWidth = new TableWidthUnit(TableWidthUnitType.Percent, 16.0);
            cells[2].PreferredWidth = new TableWidthUnit(TableWidthUnitType.Percent, 15.0);
            cells[3].PreferredWidth = new TableWidthUnit(TableWidthUnitType.Percent, 15.0);
        }

        private static void UpdatePageLayout(Section section)
        {
            DPSize pageSize = Model.PaperTypeConverter.ToSize(Model.PaperTypes.Statement);
            section.PageSize = new DPSize(pageSize.Height, pageSize.Width);
            section.PageOrientation = Model.PageOrientation.Landscape;
            double margin = Unit.InchToDip(0.5);
            section.PageMargins = new Padding(margin);
        }

        private static void UpdateNormalStyle(RadFlowDocument document)
        {
            DPStyle style = document.StyleRepository.GetStyle("Normal");
            style.CharacterProperties.FontSize.LocalValue = Unit.PointToDip(11);
            style.CharacterProperties.FontFamily.LocalValue = new ThemableFontFamily("Segoe UI");
            style.ParagraphProperties.SpacingAfter.LocalValue = Unit.PointToDip(8);
            style.ParagraphProperties.LineSpacing.LocalValue = 1.08;
        }

        private static void AddTextToHeaderRowCell(TableCell cell, string text)
        {
            Paragraph paragraph = cell.Blocks.AddParagraph();
            paragraph.TextAlignment = Alignment.Center;
            paragraph.Properties.ParagraphMarkerProperties.ForegroundColor.LocalValue = new ThemableColor(Colors.White);
            paragraph.Properties.ParagraphMarkerProperties.FontWeight.LocalValue = FontWeights.Bold;

            Run run = paragraph.Inlines.AddRun(text);
            run.Properties.FontWeight.LocalValue = FontWeights.Bold;
            run.Properties.ForegroundColor.LocalValue = new ThemableColor(Colors.White);

            cell.Shading.BackgroundColor = new ThemableColor(new Telerik.Documents.Media.Color() { A = 255, R = 112, G = 173, B = 71 });
        }

        private static void RemoveSpacingAfterAndLineSpacingInsideTable(Table table)
        {
            foreach (Paragraph paragraph in table.EnumerateChildrenOfType<Paragraph>())
            {
                paragraph.Properties.SpacingAfter.LocalValue = 0;
                paragraph.Properties.LineSpacing.LocalValue = 1.0;
            }
        }

        private static void ApplyFontWeightToFieldResultOnly(Paragraph paragraph, FontWeight fontWeight)
        {
            bool fieldSeparatorFound = false;
            bool fieldEndFound = false;

            InlineCollection inlines = paragraph.Inlines;
            for (int i = 0; i < inlines.Count; i++)
            {
                InlineBase inline = inlines[i];
                if (fieldSeparatorFound && !fieldEndFound && inline is Run)
                {
                    ((Run)inline).Properties.FontWeight.LocalValue = fontWeight;
                }

                fieldEndFound = false;

                if (inline is FieldCharacter)
                {
                    FieldCharacter fieldCharacter = (FieldCharacter)inline;
                    fieldSeparatorFound = fieldCharacter.FieldCharacterType == FieldCharacterType.Separator;
                    fieldEndFound = fieldCharacter.FieldCharacterType == FieldCharacterType.End;
                }
            }
        }

        private static FieldInfo InsertMergeField(RadFlowDocumentEditor editor, string fieldName, string switches = null)
        {
            string fieldArgument = fieldName;
            string prefix = string.Empty;
            string suffix = string.Empty;
            if (!string.IsNullOrEmpty(switches))
            {
                fieldArgument = string.Format("{0} {1}", fieldArgument, switches);

                ExtractPrefixAndSuffixFromSwitches(switches, out prefix, out suffix);
            }

            return editor.InsertField(string.Format("MERGEFIELD {0}", fieldArgument), string.Format("{0}«{1}»{2}", prefix, fieldName, suffix));
        }

        private static void ExtractPrefixAndSuffixFromSwitches(string switches, out string prefix, out string suffix)
        {
            prefix = string.Empty;
            suffix = string.Empty;

            bool isPrefix = false;
            bool isSuffix = false;

            string[] splitSwitches = switches.Split();
            for (int i = 0; i < splitSwitches.Length; i++)
            {
                string current = splitSwitches[i];

                if (isPrefix)
                {
                    prefix = current;
                    isPrefix = false;
                }
                else if (isSuffix)
                {
                    suffix = current;
                    isSuffix = false;
                }
                else
                {
                    isPrefix = current == @"\b";
                    isSuffix = current == @"\f";
                }
            }
        }

        private static Run AddTextToTableCell(TableCell cell, string text, Alignment alignment)
        {
            Paragraph paragraph = cell.Blocks.AddParagraph();
            paragraph.TextAlignment = alignment;
            paragraph.Properties.ParagraphMarkerProperties.FontWeight.LocalValue = FontWeights.Bold;

            Run run = paragraph.Inlines.AddRun(text);
            run.Properties.FontWeight.LocalValue = FontWeights.Bold;

            return run;
        }

        private static void AddMergeFieldsToTableCell(TableCell cell, Alignment alignment, string[] mergeFieldsValues, string[] switches = null)
        {
            Paragraph paragraph = cell.Blocks.AddParagraph();
            paragraph.TextAlignment = alignment;

            RadFlowDocumentEditor editor = new RadFlowDocumentEditor(cell.Document);
            editor.MoveToParagraphEnd(paragraph);

            for (int i = 0; i < mergeFieldsValues.Length; i++)
            {
                string switchString = switches == null ? null : switches[i];
                InsertMergeField(editor, mergeFieldsValues[i], switchString);
            }
        }

        private async Task MailMerge()
        {
            RadFlowDocument document = GenerateTemplate();
            RadFlowDocument mailMergeDocument = document.MailMerge(this.GenerateMergeSource());

            IFormatProvider<RadFlowDocument> formatProvider = null;
            string exampleName = null;

            switch (SelectedExportFormat)
            {
                case "Docx":
                    formatProvider = new DocxFormatProvider();
                    exampleName = "example.docx";
                    break;
                case "Rtf":
                    formatProvider = new RtfFormatProvider();
                    exampleName = "example.rtf";
                    break;
                case "Html":
                    formatProvider = new HtmlFormatProvider();
                    exampleName = "example.html";
                    break;
                case "Txt":
                    formatProvider = new TxtFormatProvider();
                    exampleName = "example.txt";
                    break;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                formatProvider.Export(mailMergeDocument, stream);

                stream.Seek(0, SeekOrigin.Begin);

                await this.fileViewerService.View(stream, exampleName);
            }          
        }

        private IEnumerable<Order> GenerateMergeSource()
        {
            Product[] products1 = new Product[]
            {
                new Product("Calzone", 12.99, 2),
                new Product("Margarita", 8.99, 1),
                new Product("Greek Salad", 9.49, 1),
                new Product("Beer", 5.49, 1)
            };

            Order order1 = new Order(100278, "John Smith", products1, "555-666-666");

            Product[] products2 = new Product[]
            {
                new Product("Pollo", 13.99, 2),
                new Product("Soda", 2.99, 2),
                new Product("Creme Brulee", 6.99, 2)
            };

            Order order2 = new Order(100279, "Mary Johnson", products2, "555-777-777");

            return new Order[] { order1, order2 };
        }
    }
}
