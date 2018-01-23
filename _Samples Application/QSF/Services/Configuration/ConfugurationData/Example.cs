namespace QSF.Services.Configuration
{
    public class Example
    {
        public Example()
        {
            this.IsThemable = true;
        }

        public string Name { get; set; }

        public string Image { get; set; }

        public string DescriptionHeader { get; set; }

        public string Description { get; set; }

        public bool IsScenario { get; set; }

        public bool IsThemable { get; set; }

        public string ExcludeFrom { get; set; }

        public string CodeURL { get; set; }
    }
}
