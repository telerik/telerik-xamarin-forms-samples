namespace QSF.Examples.WordsProcessingControl.MailMergeExample
{
    internal class Product
    {
        public Product(string productName, double pricePerUnit, int quantity)
        {
            this.ProductName = productName;
            this.PricePerUnit = pricePerUnit;
            this.Quantity = quantity;
        }
        public string ProductName { get; private set; }

        public double PricePerUnit { get; private set; }

        public int Quantity { get; private set; }

        public double FinalPrice
        {
            get
            {
                return this.CalculateFinalPrice();
            }
        }

        private double CalculateFinalPrice()
        {
            return this.PricePerUnit * this.Quantity;
        }
    }
}
