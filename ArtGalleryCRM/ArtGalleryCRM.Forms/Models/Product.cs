using Telerik.XamarinForms.Common.DataAnnotations;

namespace ArtGalleryCRM.Forms.Models
{
    public class Product : BaseDataObject
    {
        private string _title;
        private double _price;
        private string _photoUri = "art_placeholder.png";
        private int _inventoryCount;
        private bool _isDiscontinued;
        private string _description;

        [DisplayOptions(Header = "Title", PlaceholderText = "enter art title or name", Position = 0)]
        public string Title
        {
            get => this._title;
            set => SetProperty(ref this._title, value);
        }

        [DisplayOptions(Header = "Price", PlaceholderText = "enter price", Position = 2)]
        [NumericalRangeValidator(0.00, 1000000000)]
        public double Price
        {
            get => this._price;
            set => SetProperty(ref this._price, value);
        }

        [Ignore]
        public string PhotoUri
        {
            get => this._photoUri;
            set => SetProperty(ref this._photoUri, value);
        }

        [DisplayOptions(Header = "Inventory Count", Position = 3)]
        [NumericalRangeValidator(0, 100000)]
        public int InventoryCount
        {
            get => this._inventoryCount;
            set => SetProperty(ref this._inventoryCount, value);
        }
        
        [Ignore]
        internal bool IsDiscontinued
        {
            get => this._isDiscontinued;
            set => SetProperty(ref this._isDiscontinued, value);
        }
        
        [DisplayOptions(Header = "Description", PlaceholderText = "enter description of artwork", Position = 1)]
        public string Description
        {
            get => this._description;
            set => SetProperty(ref this._description, value);
        }
    }
}