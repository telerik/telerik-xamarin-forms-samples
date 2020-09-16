using QSF.Services;
using QSF.ViewModels;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.RichTextEditor;
using Xamarin.Forms;

namespace QSF.Examples.RichTextEditorControl.CustomizationExample
{
    public class CustomizationViewModel: ExampleViewModel
    {
        private readonly IResourceService resourceService;

        public CustomizationViewModel()
        {
            this.resourceService = DependencyService.Get<IResourceService>();

            this.Source = RichTextSource.FromStream(() => this.resourceService.GetResourceStream("RichTextEditorOverview.html"));

            this.MoreTextFormattingText = TelerikFont.IconFontFamily;
            this.MoreParagraphFormattingText = TelerikFont.IconAlignLeft;
        }

        public RichTextSource Source { get; set; }

        public string MoreTextFormattingText { get; set; }

        public string MoreParagraphFormattingText { get; set; }
    }
}
