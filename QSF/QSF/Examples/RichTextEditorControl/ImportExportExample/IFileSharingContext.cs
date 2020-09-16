namespace QSF.Examples.RichTextEditorControl.ImportExportExample
{
    public interface IFileSharingContext
    {
        string HtmlText { get; set; }
        string FilePath { get; set; }
    }
}
