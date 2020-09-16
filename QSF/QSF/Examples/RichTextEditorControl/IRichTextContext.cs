using System.Threading.Tasks;

namespace QSF.Examples.RichTextEditorControl
{
    public interface IRichTextContext
    {
        Task<string> GetHtmlAsync();
    }
}
