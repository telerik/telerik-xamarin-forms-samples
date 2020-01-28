using CommonHelpers.Common;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.Models
{
    public class WelcomeCard : BindableBase
    {
        private string _title;
        private string _subtitle;
        private bool _isFinalItem;
        private ImageSource _iconSource;
        
        public string Title
        {
            get => this._title;
            set => SetProperty(ref this._title, value);
        }

        public string Subtitle
        {
            get => this._subtitle;
            set => SetProperty(ref this._subtitle, value);
        }
        
        public ImageSource IconSource
        {
            get => this._iconSource;
            set => SetProperty(ref this._iconSource, value);
        }

        public bool IsFinalItem
        {
            get => this._isFinalItem;
            set => SetProperty(ref this._isFinalItem, value);
        }
    }
}