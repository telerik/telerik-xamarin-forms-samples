using System;
using Xamarin.Forms;

using AndroidSpecific = Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace QSF.Examples.RichTextEditorControl.ImportExportExample
{
    public partial class ImportExportView : ContentView
    {
        private AndroidSpecific.WindowSoftInputModeAdjust lastInputMode = AndroidSpecific.WindowSoftInputModeAdjust.Unspecified;

        public ImportExportView()
        {
            this.InitializeComponent();
        }

        private void OnOpenItemTapped(object sender, EventArgs eventArgs)
        {
            this.openToolbarItem.IsSelected = false;
        }

        private void OnSaveItemTapped(object sender, EventArgs eventArgs)
        {
            this.saveToolbarItem.IsSelected = false;
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
