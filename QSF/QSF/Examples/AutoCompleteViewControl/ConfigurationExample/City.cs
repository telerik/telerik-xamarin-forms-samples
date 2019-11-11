namespace QSF.Examples.AutoCompleteViewControl.ConfigurationExample
{
    public class City
    {
        public City(string name, double population, string country)
        {
            this.Name = name;
            this.Population = population;
            this.Country = country;
        }

        public string Name { get; set; }
        public double Population { get; set; }
        public string Country { get; set; }
    }
}