﻿using System.IO;
using System.Threading.Tasks;
using Telerik.XamarinForms.ImageEditor;
using Xamarin.Forms;

namespace QSF.Examples.ImageEditorControl
{
    public class ImageContext : BindableObject, IImageContext
    {
        public static readonly BindableProperty ImageEditorProperty =
            BindableProperty.Create(nameof(ImageEditor),
                typeof(RadImageEditor), typeof(ImageContext));

        public RadImageEditor ImageEditor
        {
            get
            {
                return (RadImageEditor)this.GetValue(ImageEditorProperty);
            }
            set
            {
                this.SetValue(ImageEditorProperty, value);
            }
        }

        public async Task SaveAsync(Stream outputStream, ImageFormat imageFormat, double imageQuality)
        {
            if (this.ImageEditor != null)
            {
                await this.ImageEditor.SaveAsync(outputStream, imageFormat, imageQuality);
            }
        }

        public async Task SaveAsync(Stream outputStream, ImageFormat imageFormat, double imageQuality, Size maximumSize)
        {
            if (this.ImageEditor != null)
            {
                await this.ImageEditor.SaveAsync(outputStream, imageFormat, imageQuality, maximumSize);
            }
        }

        public async Task SaveAsync(Stream outputStream, ImageFormat imageFormat, double imageQuality, double scaleFactor)
        {
            if (this.ImageEditor != null)
            {
                await this.ImageEditor.SaveAsync(outputStream, imageFormat, imageQuality, scaleFactor);
            }
        }
    }
}
