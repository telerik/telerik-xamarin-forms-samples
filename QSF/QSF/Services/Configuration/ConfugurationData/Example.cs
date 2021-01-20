namespace QSF.Services.Configuration
{
    public class Example
    {
        public Example()
        {
            this.IsThemable = true;
        }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public Icon Icon { get; set; }

        public string DescriptionHeader { get; set; }

        public string Description { get; set; }

        public bool IsScenario { get; set; }

        public bool IsThemable { get; set; }

        public string ExcludeFrom { get; set; }

        public string CodeURL { get; set; }

        public bool IsNew { get; set; }

        public bool IsUpdated { get; set; }
    }
}
