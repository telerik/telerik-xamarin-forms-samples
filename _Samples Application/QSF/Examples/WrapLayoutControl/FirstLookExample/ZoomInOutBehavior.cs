using System.Collections.Generic;
using Telerik.XamarinForms.Common;
using Xamarin.Forms;

namespace QSF.Examples.WrapLayoutControl.FirstLookExample
{
    public class ZoomInOutBehavior : Behavior<RadWrapLayout>
    {
        RadWrapLayout owner;
        PinchGestureRecognizer pinchRecognizer;
        double currentScale, prevouseScale, totalScale;
        List<double> sizes;

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
            this.sizes = new List<double> { 75, 94, 125 };
            this.owner = (RadWrapLayout)bindable;
            this.pinchRecognizer = new PinchGestureRecognizer();
            this.pinchRecognizer.PinchUpdated += PinchRecognizer_PinchUpdated;
            this.owner.GestureRecognizers.Add(this.pinchRecognizer);
        }

        private void PinchRecognizer_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Completed)
            {
                this.currentScale = this.prevouseScale = this.totalScale = 0;
                return;
            }

            if (e.Status == GestureStatus.Started)
            {
                this.prevouseScale = e.Scale;
                return;
            }

            if (e.Status == GestureStatus.Running)
            {
                this.currentScale = e.Scale;
            }

            var deltaScale = this.prevouseScale - this.currentScale;
            bool zoomOut = deltaScale > 0;
            this.totalScale += System.Math.Abs(deltaScale);
            int currentSizeIndex = this.sizes.IndexOf(owner.ItemHeight);

            if (this.totalScale < 0.4)
            {
                return;
            }
            this.totalScale = 0;

            if (zoomOut)
            {
                // zoom out
                currentSizeIndex--;
                if (currentSizeIndex >= 0)
                {
                    double newSize = this.sizes[currentSizeIndex];
                    this.owner.ItemHeight = this.owner.ItemWidth = newSize;
                }
            }
            else
            {
                // zoom in
                currentSizeIndex++;
                if (currentSizeIndex <= this.sizes.Count - 1)
                {
                    double newSize = this.sizes[currentSizeIndex];
                    this.owner.ItemHeight = this.owner.ItemWidth = newSize;
                }
            }
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            this.pinchRecognizer.PinchUpdated -= PinchRecognizer_PinchUpdated;
            this.pinchRecognizer = null;
            this.owner = null;
            base.OnDetachingFrom(bindable);
        }
    }
}