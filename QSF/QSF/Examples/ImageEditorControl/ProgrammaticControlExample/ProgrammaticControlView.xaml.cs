using Telerik.XamarinForms.ImageEditor;
using Telerik.XamarinForms.ImageEditor.Commands;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.ImageEditorControl.ProgrammaticControlExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgrammaticControlView : ContentView
    {
        bool brightnessTimerStarted = false;
        bool hueTimerStarted = false;

        double newBrightness;
        double newHue;
        public ProgrammaticControlView()
        {
            InitializeComponent();
        }

        private void HueValueChanged(object sender, ValueChangedEventArgs e)
        {
            this.newHue = e.NewValue;
            if (!this.hueTimerStarted)
            {
                this.hueTimerStarted = true;
                Device.StartTimer(System.TimeSpan.FromMilliseconds(400), this.UpdateHue);
            }
        }
        
        private void BrightnessValueChanged(object sender, ValueChangedEventArgs e)
        {
            this.newBrightness = e.NewValue;
            if (!this.brightnessTimerStarted)
            {
                this.brightnessTimerStarted = true;
                Device.StartTimer(System.TimeSpan.FromMilliseconds(400), this.UpdateBrightness);
            }
        }

        private bool UpdateHue()
        {
            var hue = new HueCommandContext()
            {
                Hue = this.newHue
            };

            if (this.imageEditor.HueCommand.CanExecute(hue))
            {
                this.imageEditor.HueCommand.Execute(hue);
            }
            this.hueTimerStarted = false;
            return false;
        }

        private bool UpdateBrightness()
        {
            var brightness = new BrightnessCommandContext()
            {
                Brightness = this.newBrightness
            };
            if (this.imageEditor.BrightnessCommand.CanExecute(brightness))
            {
                this.imageEditor.BrightnessCommand.Execute(brightness);
            }
            this.brightnessTimerStarted = false;
            return false;
        }
    }
}