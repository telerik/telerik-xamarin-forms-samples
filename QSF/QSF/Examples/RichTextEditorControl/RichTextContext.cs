using System.Threading.Tasks;
using Telerik.XamarinForms.RichTextEditor;
using Xamarin.Forms;

namespace QSF.Examples.RichTextEditorControl
{
    public class RichTextContext : BindableObject, IRichTextContext
    {
        public static BindableProperty RichTextEditorProperty =
            BindableProperty.Create(nameof(RichTextEditor), typeof(RadRichTextEditor), typeof(RichTextContext));

        public RadRichTextEditor RichTextEditor
        {
            get
            {
                return (RadRichTextEditor)this.GetValue(RichTextEditorProperty);
            }
            set
            {
                this.SetValue(RichTextEditorProperty, value);
            }
        }

        public Task<string> GetHtmlAsync()
        {
            return this.RichTextEditor.GetHtmlAsync();
        }
    }
}
