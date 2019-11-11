using System;
using System.ComponentModel;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace ErpApp.Models
{
    public class OrderDetail : INotifyPropertyChanged
    {
        public static OrderDetail Create(Product product)
        {
            OrderDetail detail = new OrderDetail();
            detail.ModifiedDate = DateTime.Now;
            detail.ProductPrice = product.Price;
            detail.Count = 0;
            detail.Product = product;
            detail.ProductId = product.Id;
            return detail;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "orderID")]
        public string OrderId { get; set; }

        [JsonProperty(PropertyName = "modifiedDate")]
        public DateTime ModifiedDate { get; set; }

        [JsonProperty(PropertyName = "productPrice")]
        public double ProductPrice { get; set; }

        [JsonProperty(PropertyName = "discount")]
        public double Discount { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        public double Quantity
        {
            get => Count;
            set
            {
                int newCount = Convert.ToInt32(value);
                if (Count == newCount)
                    return;

                Count = newCount;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ammount)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Freight)));
            }
        }

        [JsonProperty(PropertyName = "productID")]
        public string ProductId { get; set; }

        [Version]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "product")]
        public Product Product { get; set; }

        public string ProductNumber => Product?.ProductNumber;
        public string ProductName => Product?.Name;
        public double Ammount => ProductPrice * Count;
        public double Freight => (Product?.Weight ?? 0) * Count;

        public OrderDetail Copy()
        {
            OrderDetail newOrderDetail = new OrderDetail()
            {
                Id = this.Id,
                OrderId = this.OrderId,
                ModifiedDate = this.ModifiedDate,
                ProductPrice = this.ProductPrice,
                Count = this.Count,
                ProductId = this.ProductId,
                Product = this.Product
            };
            return newOrderDetail;
        }
    }

    public class OrderDetailIDEqualityComparer : System.Collections.Generic.IEqualityComparer<OrderDetail>
    {
        public bool Equals(OrderDetail x, OrderDetail y)
        {
            return x?.ProductId == y?.ProductId;
        }

        public int GetHashCode(OrderDetail obj)
        {
            return obj.GetHashCode();
        }
    }
}
