using QSF.Services;
using QSF.ViewModels;
using System.Windows.Input;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.RichTextEditor;
using Xamarin.Forms;

namespace QSF.Examples.RichTextEditorControl.CustomizationExample
{
    public class CustomizationViewModel : ExampleViewModel
    {
        private bool isReadonly = true;
        private readonly IResourceService resourceService;

        public CustomizationViewModel()
        {
            this.resourceService = DependencyService.Get<IResourceService>();

            this.Source = RichTextSource.FromStream(() => this.resourceService.GetResourceStream("RichTextEditorOverview.html"));

            this.MoreTextFormattingText = TelerikFont.IconFontFamily;
            this.MoreParagraphFormattingText = TelerikFont.IconAlignLeft;
            this.ToggleReadonlyCommand = new Command(ToggleReadonly);
        }

        public RichTextSource Source { get; set; }

        public string MoreTextFormattingText { get; set; }

        public string MoreParagraphFormattingText { get; set; }

        public ICommand ToggleReadonlyCommand { get; set; }

        public string ToggleReadonlyText
        {
            get
            {
                return this.IsReadonly ? char.ConvertFromUtf32(0xe818) : char.ConvertFromUtf32(0xe809);
            }
        }

        public bool IsReadonly
        {
            get 
            {
                return this.isReadonly;
            }
            set
            {
                if (this.isReadonly != value)
                {
                    this.isReadonly = value;
                    this.OnPropertyChanged(nameof(IsReadonly));
                    this.OnPropertyChanged(nameof(ToggleReadonlyText));
                }
            }
        }

        private void ToggleReadonly(object obj)
        {
            this.IsReadonly = !this.IsReadonly;
        }
    }
}
