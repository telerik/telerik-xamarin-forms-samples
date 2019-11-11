using QSF.Services.Configuration;

namespace QSF.ViewModels
{
    public class ExampleInfoViewModel : ViewModelBase
    {
        public ExampleInfoViewModel(Example example)
        {
            this.GroupName = example.IsScenario ? "Scenarios" : "Features";
            this.Image = example.Image;
            this.Name = example.DisplayName;
            this.Description = example.Description;
            this.Example = example;
            this.IsNew = example.IsNew;
            this.IsUpdated = example.IsUpdated;
        }

        public string GroupName { get; }

        public string Image { get; }

        public string Name { get; }

        public string Description { get; }

        internal Example Example { get; }

        public bool IsNew { get; }

        public bool IsUpdated { get; }
    }
}
