using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace ErpApp.Models
{
    public class Vendor : IEntity, INotifyPropertyChanged
    {
        private int _index;
        public static string[] AvailableOrderFrequencies = new string[] { "S", "M", "L" };
        public static int MaxRating = 5;

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "vendorNumber")]
        public string VendorNumber { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public byte Rating { get; set; }

        [JsonProperty(PropertyName = "annualRevenue")]
        public double AnnualRevenue { get; set; }

        [JsonProperty(PropertyName = "salesAmmount")]
        public double SalesAmount { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "orderFrequency")]
        public string OrderFrequency { get; set; }

        [JsonProperty(PropertyName = "image")]
        public Uri ImageURL { get; set; }

        [JsonProperty(PropertyName = "lastOrderDate")]
        public DateTime? LastOrderDate { get; set; }

        [Version]
        public string Version { get; set; }

        public int Index { get => _index; set => SetProperty(ref _index, value); }
        public string Description => Name;

        public string Number => VendorNumber;

        public event PropertyChangedEventHandler PropertyChanged;

        public override bool Equals(object obj)
        {
            var vendor = obj as Vendor;
            return vendor != null &&
                   Id == vendor.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<string>.Default.GetHashCode(Id);
        }

        public static bool operator ==(Vendor vendor1, Vendor vendor2)
        {
            return EqualityComparer<Vendor>.Default.Equals(vendor1, vendor2);
        }

        public static bool operator !=(Vendor vendor1, Vendor vendor2)
        {
            return !(vendor1 == vendor2);
        }

        public Vendor Copy()
        {
            Vendor newVendor = new Vendor()
            {
                Id = this.Id,
                VendorNumber = this.VendorNumber,
                Name = this.Name,
                Rating = this.Rating,
                AnnualRevenue = this.AnnualRevenue,
                SalesAmount = this.SalesAmount,
                Phone = this.Phone,
                OrderFrequency = this.OrderFrequency,
                ImageURL = this.ImageURL,
                LastOrderDate = this.LastOrderDate
            };
            return newVendor;
        }

        public bool Validate(out IList<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrEmpty(this.Name))
                errors.Add("Vendor name cannot be empty");
            if (string.IsNullOrEmpty(this.Phone))
                errors.Add("Phone cannot be empty");
            if (!string.IsNullOrEmpty(this.Phone) && !System.Text.RegularExpressions.Regex.IsMatch(this.Phone, Customer.PhoneRegexPattern))
                errors.Add("Phone is invalid");
            if (this.Rating <= 0)
                errors.Add("Rating not specified");
            if (string.IsNullOrEmpty(OrderFrequency))
                errors.Add("Order Frequency not specified");
            if (this.AnnualRevenue <= 0)
                errors.Add("Annual Revenue not specified");

            return errors.Count == 0;
        }

        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (object.Equals(storage, value))
                return false;

            storage = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }
    }
}
