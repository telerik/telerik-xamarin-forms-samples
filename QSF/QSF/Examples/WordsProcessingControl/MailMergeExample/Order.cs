using System.Collections.Generic;

namespace QSF.Examples.WordsProcessingControl.MailMergeExample
{
    internal class Order
    {
        public Order(int orderID, string clientName, IEnumerable<Product> products, string phoneNumber)
        {
            this.OrderID = orderID;
            this.ClientName = clientName;
            this.Products = new List<Product>(products);
            this.PhoneNumber = phoneNumber;
        }

        public int OrderID { get; private set; }

        public string ClientName { get; private set; }

        public IEnumerable<Product> Products { get; private set; }

        public double TotalPrice
        {
            get
            {
                return this.CalculateTotalPrice();
            }
        }

        public string PhoneNumber { get; private set; }

        private double CalculateTotalPrice()
        {
            double totalPrice = 0;
            foreach (Product product in this.Products)
            {
                totalPrice += product.FinalPrice;
            }

            return totalPrice;
        }
    }
}
