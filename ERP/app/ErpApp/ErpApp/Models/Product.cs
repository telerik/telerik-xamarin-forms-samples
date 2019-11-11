using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Telerik.XamarinForms.Common;

namespace ErpApp.Models
{
    public class Product : IEntity, INotifyPropertyChanged
    {
        private double _weight, _price, _stockLevel;
        private int _index;
        private string _location, _name;
        private Xamarin.Forms.Color _color;
        private RadSolidColorBrush _brush;

        public static string[] AvailableLocations = new string[] { "Tool Crib", "Warehouse", "External" };

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "productNumber")]
        public string ProductNumber { get; set; }

        [JsonProperty(PropertyName = "createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "dateAdded")]
        public DateTime DateAdded { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        [JsonProperty(PropertyName = "weight")]
        public double Weight { get => _weight; set => SetProperty(ref _weight, value); }

        [JsonProperty(PropertyName = "price")]
        public double Price { get => _price; set => SetProperty(ref _price, value); }

        [JsonProperty(PropertyName = "location")]
        public string Location { get => _location; set => SetProperty(ref _location, value); }

        [JsonProperty(PropertyName = "stockLevel")]
        public double StockLevel { get => _stockLevel; set => SetProperty(ref _stockLevel, value); }

        [JsonProperty(PropertyName = "image")]
        public Uri ImageURL { get; set; }

        [JsonProperty(PropertyName = "color")]
        public int ColorInt { get; set; }

        public Xamarin.Forms.Color Color { get => _color; private set => SetProperty(ref _color, value); }

        public RadSolidColorBrush Brush { get => _brush; private set => SetProperty(ref _brush, value); }

        public string Number => ProductNumber;
        public string Description => Name;

        [Version]
        public string Version { get; set; }

        public int Index { get => _index; set => SetProperty(ref _index, value); }

        public event PropertyChangedEventHandler PropertyChanged;

        public override bool Equals(object obj)
        {
            var product = obj as Product;
            return product != null &&
                   Id == product.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<string>.Default.GetHashCode(Id);
        }

        public static bool operator ==(Product product1, Product product2)
        {
            return EqualityComparer<Product>.Default.Equals(product1, product2);
        }

        public static bool operator !=(Product product1, Product product2)
        {
            return !(product1 == product2);
        }

        public void Prepare()
        {
            Color = Serialization.Utils.ToColor(ColorInt);
            Brush = new RadSolidColorBrush(Color);
        }

        public void ChangeBrush(RadBrush brush)
        {
            if (brush is RadSolidColorBrush solidColorBrush)
            {
                ColorInt = Serialization.Utils.ToColor(solidColorBrush.Color).ToArgb();
                Color = solidColorBrush.Color;
                Brush = solidColorBrush;
            }
        }

        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (object.Equals(storage, value))
                return false;

            storage = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        public Product Copy()
        {
            Product newProduct = new Product()
            {
                Id = this.Id,
                ProductNumber = this.ProductNumber,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt,
                DateAdded = this.DateAdded,
                Name = this.Name,
                Weight = this.Weight,
                Price = this.Price,
                Location = this.Location,
                StockLevel = this.StockLevel,
                ImageURL = this.ImageURL,
                Color = this.Color,
                Brush = this.Brush,
                ColorInt = this.ColorInt,
                Version = this.Version,
                Index = this.Index
            };
            return newProduct;
        }

        public bool Validate(out IList<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrEmpty(this.Name))
                errors.Add("Product name cannot be empty");
            if (string.IsNullOrEmpty(this.Location))
                errors.Add("Location not specified");
            if (this.Weight <= 0)
                errors.Add("Weight not specified");
            if (this.Price <= 0)
                errors.Add("Price not specified");
            if (this.StockLevel <= 0)
                errors.Add("StockLevel not specified");

            return errors.Count == 0;
        }
    }
}
