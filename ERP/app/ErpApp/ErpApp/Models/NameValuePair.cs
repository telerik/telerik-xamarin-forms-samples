namespace ErpApp.Models
{
    public class NameValuePair
    {
        public NameValuePair(string name, double value)
        {
            Name = name;
            Value = value;
        }

        public double Value { get; set; }
        public string Name { get; set; }
    }
}
