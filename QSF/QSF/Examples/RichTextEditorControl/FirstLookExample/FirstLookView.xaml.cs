using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AndroidSpecific = Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace QSF.Examples.RichTextEditorControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : ContentView
    {
        private AndroidSpecific.WindowSoftInputModeAdjust lastInputMode = AndroidSpecific.WindowSoftInputModeAdjust.Unspecified;

        public FirstLookView()
        {
            InitializeComponent();
            Device.BeginInvokeOnMainThread(() => this.recipientsAutoComplete.Tokens.Add(((FirstLookViewModel)this.BindingContext).RecipientsItemSource[0]));
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (Device.RuntimePlatform == Device.Android)
            {
                if (this.Parent != null)
                {
                    if (this.lastInputMode == AndroidSpecific.WindowSoftInputModeAdjust.Unspecified)
                    {
                        this.lastInputMode = GetSoftInputMode();
                    }

                    SetSoftInputMode(AndroidSpecific.WindowSoftInputModeAdjust.Resize);
                }
                else
                {
                    SetSoftInputMode(this.lastInputMode);
                }
            }
        }

        private static AndroidSpecific.WindowSoftInputModeAdjust GetSoftInputMode()
        {
            return AndroidSpecific.Application.GetWindowSoftInputModeAdjust(Application.Current);
        }

        private static void SetSoftInputMode(AndroidSpecific.WindowSoftInputModeAdjust inputMode)
        {
            AndroidSpecific.Application.SetWindowSoftInputModeAdjust(Application.Current, inputMode);
        }
    }
}