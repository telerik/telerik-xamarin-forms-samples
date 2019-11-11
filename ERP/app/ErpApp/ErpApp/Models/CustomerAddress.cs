using System.ComponentModel;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace ErpApp.Models
{
    public class CustomerAddress : INotifyPropertyChanged
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "customerID")]
        public string CustomerID { get; set; }

        public int Index { get; set; }

        [JsonProperty(PropertyName = "primary")]
        public bool IsPrimary { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "poCode")]
        public string POCode { get; set; }

        [Version]
        public string Version { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CustomerAddress Copy()
        {
            CustomerAddress newCustomerAddress = new CustomerAddress()
            {
                Id = this.Id,
                Index = this.Index,
                CustomerID = this.CustomerID,
                IsPrimary = this.IsPrimary,
                Address = this.Address,
                City = this.City,
                State = this.State,
                Country = this.Country,
                POCode = this.POCode
            };
            return newCustomerAddress;
        }

        public void InvalidateFullAddress() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FullAddress)));

        public string FullAddress => $"{this.Address}, {this.City}, {this.State} {this.POCode}, {this.Country}";
        public string CityAddress => $"{this.City}, {this.State} {this.POCode}, {this.Country}";
    }
}
