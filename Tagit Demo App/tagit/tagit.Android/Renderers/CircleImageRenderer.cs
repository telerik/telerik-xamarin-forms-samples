using System;
using Android.Graphics;
using Android.OS;
using Android.Views;
using tagit.Controls;
using tagit.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;
using View = Android.Views.View;
using FFImageLoading.Forms.Droid;
using FFImageLoading.Forms;

[assembly: ExportRenderer(typeof(CircleImage), typeof(CircleImageRenderer))]

namespace tagit.Droid.Renderers
{   
    public class CircleImageRenderer : CachedImageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CachedImage> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
                if ((int)Build.VERSION.SdkInt < 18)
                    SetLayerType(LayerType.Software, null);
        }
 
        protected override bool DrawChild(Canvas canvas, View child, long drawingTime)
        {
            try
            {
                var radius = Math.Min(Width, Height) / 2;
                var strokeWidth = 10;
                radius -= strokeWidth / 2;

                var path = new Path();
                path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);
                canvas.Save();

                canvas.ClipPath(path);

                var result = base.DrawChild(canvas, child, drawingTime);

                canvas.Restore();

                path = new Path();
                path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);

                var paint = new Paint();
                paint.AntiAlias = true;
                paint.StrokeWidth = 3;
                paint.SetStyle(Paint.Style.Stroke);
                paint.Color = Color.White;

                canvas.DrawPath(path, paint);


                paint.Dispose();
                path.Dispose();
                return result;
            }
            catch
            {
            }

            return base.DrawChild(canvas, child, drawingTime);
        }
    }
}