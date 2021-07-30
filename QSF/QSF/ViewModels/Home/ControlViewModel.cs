using QSF.Services.Configuration;

namespace QSF.ViewModels
{
    public class ControlViewModel : ViewModelBase
    {
        public ControlViewModel(Control control)
        {
            this.Name = control.Name;
            this.DisplayName = control.DisplayName;
            this.Icon = control.Icon;
            this.ShortDescription = control.ShortDescription;
            this.FullDescription = control.FullDescription;
            this.DocumentationURL = control.DocumentationURL;
            this.IsFeatured = control.Featured > 0;
            this.IsNew = control.IsNew;
            this.IsCTP = control.IsCTP;
            this.IsBeta = control.IsBeta;
            this.IsUpdated = control.IsUpdated;
        }

        public string Name { get; }

        public Icon Icon { get; }

        public string DisplayName { get; }

        public string ShortDescription { get; }

        public string FullDescription { get; }

        public string DocumentationURL { get; }

        public bool IsFeatured { get; }

        public bool IsNew { get; }

        public bool IsCTP { get; }

        public bool IsBeta { get; }

        public bool IsUpdated { get; }
    }
}
