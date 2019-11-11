namespace QSF.Examples.SegmentedControl.FirstLookExample
{
    public class MenuItem
    {
        public string Category { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }

        public MenuItem(string category, string name, double price)
        {
            this.Category = category;
            this.Name = name;
            this.Price = price;
        }
    }
}
