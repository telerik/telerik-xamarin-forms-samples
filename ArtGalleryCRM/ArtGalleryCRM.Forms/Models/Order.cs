using System;

namespace ArtGalleryCRM.Forms.Models
{
    public class Order : BaseDataObject
    {
        private string _customerId;
        private string _employeeId;
        private string _productId;
        private double _totalPrice;
        private int _quantity;
        private DateTime _orderDate = DateTime.Today;
        private string _deliveryService;
        private string _street;
        private string _city;
        private string _state;
        private string _country;
        private string _zipCode;
        private string _notes;
        
        public string CustomerId
        {
            get => this._customerId;
            set => SetProperty(ref this._customerId, value);
        }
        
        public string EmployeeId
        {
            get => this._employeeId;
            set => SetProperty(ref this._employeeId, value);
        }
        
        public string ProductId
        {
            get => this._productId;
            set => SetProperty(ref this._productId, value);
        }
        
        public double TotalPrice
        {
            get => this._totalPrice;
            set => SetProperty(ref this._totalPrice, value);
        }
        
        public int Quantity
        {
            get => this._quantity;
            set => SetProperty(ref this._quantity, value);
        }
        
        public DateTime OrderDate
        {
            get => this._orderDate;
            set => SetProperty(ref this._orderDate, value);
        }
        
        public string DeliveryService
        {
            get => this._deliveryService;
            set => SetProperty(ref this._deliveryService, value);
        }
        
        public string Street
        {
            get => this._street;
            set => SetProperty(ref this._street, value);
        }
        
        public string City
        {
            get => this._city;
            set => SetProperty(ref this._city, value);
        }
        
        public string State
        {
            get => this._state;
            set => SetProperty(ref this._state, value);
        }
        
        public string Country
        {
            get => this._country;
            set => SetProperty(ref this._country, value);
        }
        
        public string ZipCode
        {
            get => this._zipCode;
            set => SetProperty(ref this._zipCode, value);
        }
        
        public string Notes
        {
            get => this._notes;
            set => SetProperty(ref this._notes, value);
        }
    }
}