using System;
using Xamarin.Forms;

namespace QSF.Views
{
    public partial class OnBoardingView : ContentView
    {
        private bool isAutoSliding;

        public event EventHandler Closing;

        public OnBoardingView()
        {
            this.InitializeComponent();
        }

        private bool AutoSlide()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (this.isAutoSliding)
                {
                    this.slideView.SelectedIndex++;
                }
            });

            return this.isAutoSliding;
        }

        public void StartAutoSliding()
        {
            this.isAutoSliding = true;

            var slideInterval = TimeSpan.FromSeconds(5);

            Device.StartTimer(slideInterval, this.AutoSlide);
        }

        public void StopAutoSliding()
        {
            this.isAutoSliding = false;
        }

        private void OnCloseClicked(object sender, EventArgs e)
        {
            this.Closing?.Invoke(this, EventArgs.Empty);
        }
    }
}