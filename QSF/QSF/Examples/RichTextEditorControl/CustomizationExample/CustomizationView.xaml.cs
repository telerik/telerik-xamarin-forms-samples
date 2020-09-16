using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AndroidSpecific = Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace QSF.Examples.RichTextEditorControl.CustomizationExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomizationView : ContentView
    {
        private AndroidSpecific.WindowSoftInputModeAdjust lastInputMode = AndroidSpecific.WindowSoftInputModeAdjust.Unspecified;

        public CustomizationView()
        {
            InitializeComponent();
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