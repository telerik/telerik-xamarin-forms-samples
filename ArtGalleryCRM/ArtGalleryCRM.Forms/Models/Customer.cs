using Telerik.XamarinForms.Common.DataAnnotations;

namespace ArtGalleryCRM.Forms.Models
{
    public class Customer : BaseDataObject
    {
        private string _name;
        private string _street;
        private string _city;
        private string _state;
        private string _country;
        private string _zipCode;
        private string _notes;

        [DisplayOptions(Header = "Name", Group = "Name", Position = 0)]
        [NonEmptyValidator("Please fill the customer name")]
        public string Name
        {
            get => this._name;
            set => SetProperty(ref this._name, value);
        }
        
        [DisplayOptions(PlaceholderText = "street", Group = "Address", Position = 0)]
        public string Street
        {
            get => this._street;
            set => SetProperty(ref this._street, value);
        }

        [DisplayOptions(PlaceholderText = "city", Group = "Address", Position = 1)]
        public string City
        {
            get => this._city;
            set => SetProperty(ref this._city, value);
        }

        [DisplayOptions(PlaceholderText = "state", Group = "Address", Position = 2)]
        public string State
        {
            get => this._state;
            set => SetProperty(ref this._state, value);
        }

        [DisplayOptions(PlaceholderText = "country", Group = "Address", Position = 3)]
        public string Country
        {
            get => this._country;
            set => SetProperty(ref this._country, value);
        }

        [DisplayOptions(PlaceholderText = "zip code", Group = "Address", Position = 4)]
        public string ZipCode
        {
            get => this._zipCode;
            set => SetProperty(ref this._zipCode, value);
        }

        [DisplayOptions(Header = "Notes",Group = "Details", Position = 0)]
        public string Notes
        {
            get => this._notes;
            set => SetProperty(ref this._notes, value);
        }
    }
}