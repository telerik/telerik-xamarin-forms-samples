using System;
using CommonHelpers.Common;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.Models
{
    public class NavigationMenuItem : BindableBase
    {
        private int _id;
        private string _title;
        private Type _targetType;
        private string _iconPath;
        private Color _rowBackgroundColor;

        public int Id
        {
            get => this._id;
            set => SetProperty(ref this._id, value);
        }

        public string Title
        {
            get => this._title;
            set => SetProperty(ref this._title, value);
        }

        public Type TargetType
        {
            get => this._targetType;
            set => SetProperty(ref this._targetType, value);
        }

        public string IconPath
        {
            get => this._iconPath;
            set => SetProperty(ref this._iconPath, value);
        }

        public Color RowBackgroundColor
        {
            get => this._rowBackgroundColor;
            set => SetProperty(ref this._rowBackgroundColor, value);
        }
    }
}