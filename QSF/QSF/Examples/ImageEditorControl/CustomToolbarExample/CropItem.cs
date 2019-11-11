using QSF.ViewModels;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.ImageEditor;

namespace QSF.Examples.ImageEditorControl.CustomToolbarExample
{
    public class CropItem : BindableBase
    {
        private string text;
        private string icon;
        private RadGeometry geometry;
        private AspectRatio aspectRatio;
        private bool isSelected;

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                if (this.text != value)
                {
                    this.text = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Icon
        {
            get
            {
                return this.icon;
            }
            set
            {
                if (this.icon != value)
                {
                    this.icon = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public RadGeometry Geometry
        {
            get
            {
                return this.geometry;
            }
            set
            {
                if (this.geometry != value)
                {
                    this.geometry = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public AspectRatio AspectRatio
        {
            get
            {
                return this.aspectRatio;
            }
            set
            {
                if (this.aspectRatio != value)
                {
                    this.aspectRatio = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
