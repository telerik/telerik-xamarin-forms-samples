using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace ErpApp.Models
{
    public class Customer : IEntity, INotifyPropertyChanged
    {
        private string _name, _email, _phone, _channel;
        private double _discount;

        public static string[] AvailableCommunicationChannels = new string[] { "email", "phone" };
        public static readonly string PhoneRegexPattern = @"^\+?[-\s\./0-9]*[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";
        public static readonly string EmailRegexPattern = @"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "customerNumber")]
        [Telerik.XamarinForms.Common.DataAnnotations.Ignore]
        public string CustomerNumber { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value)
                    return;

                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get => _email;
            set
            {
                if (_email == value)
                    return;

                _email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
            }
        }

        [JsonProperty(PropertyName = "phone")]
        public string Phone
        {
            get => _phone;
            set
            {
                if (_phone == value)
                    return;

                _phone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Phone)));
            }
        }

        [JsonProperty(PropertyName = "image")]
        public Uri ImageURL { get; set; }

        [JsonProperty(PropertyName = "defaultDiscount")]
        public double DefaultDiscount
        {
            get => _discount;
            set
            {
                if (_discount == value)
                    return;

                _discount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DefaultDiscount)));
            }
        }

        [JsonProperty(PropertyName = "customerSatisfaction")]
        public double CustomerSatisfaction { get; set; }

        [JsonProperty(PropertyName = "preferredCommunicationChannel")]
        public string PreferredCommunicationChannel
        {
            get => _channel;
            set
            {
                if (_channel == value)
                    return;

                _channel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PreferredCommunicationChannel)));
            }
        }

        [Version]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "addresses")]
        public ICollection<CustomerAddress> Addresses { get; set; }

        [JsonProperty(PropertyName = "orders")]
        public virtual ICollection<Order> Orders { get; set; }

        public string Number => CustomerNumber;
        public string Description => Name;

        public event PropertyChangedEventHandler PropertyChanged;

        public Customer Copy()
        {
            Customer newCustomer = new Customer()
            {
                Id = this.Id,
                CustomerNumber = this.CustomerNumber,
                Name = this.Name,
                Email = this.Email,
                Phone = this.Phone,
                ImageURL = this.ImageURL,
                DefaultDiscount = this.DefaultDiscount,
                CustomerSatisfaction = this.CustomerSatisfaction,
                PreferredCommunicationChannel = this.PreferredCommunicationChannel,
                Addresses = this?.Addresses?.Select(c => c.Copy())?.ToList()
            };
            return newCustomer;
        }

        public bool Validate(out IList<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrEmpty(this.Name))
                errors.Add("Customer name cannot be empty");
            if (string.IsNullOrEmpty(this.Phone))
                errors.Add("Phone cannot be empty");
            if (!string.IsNullOrEmpty(this.Phone) && !System.Text.RegularExpressions.Regex.IsMatch(this.Phone, PhoneRegexPattern))
                errors.Add("Phone is invalid");
            if (string.IsNullOrEmpty(this.Email))
                errors.Add("Email cannot be empty");
            if (!string.IsNullOrEmpty(this.Email) && !System.Text.RegularExpressions.Regex.IsMatch(this.Email, EmailRegexPattern))
                errors.Add("Email is invalid");

            return errors.Count == 0;
        }

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;
            return customer != null &&
                   _name == customer._name &&
                   _email == customer._email &&
                   _phone == customer._phone &&
                   Id == customer.Id;
        }

        public override int GetHashCode()
        {
            var hashCode = -2004006483;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_phone);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            return hashCode;
        }

        public static bool operator ==(Customer customer1, Customer customer2)
        {
            if (System.Object.ReferenceEquals(customer1, customer2))
                return true;

            if (((object)customer1 == null) || ((object)customer2 == null))
                return false;

            return customer1.Equals(customer2);
        }

        public static bool operator !=(Customer customer1, Customer customer2)
        {
            return !(customer1 == customer2);
        }
    }
}
