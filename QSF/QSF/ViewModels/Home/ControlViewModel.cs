using QSF.Services.Configuration;

namespace QSF.ViewModels
{
    public class ControlViewModel : ViewModelBase
    {
        public ControlViewModel(Control control)
        {
            this.Name = control.Name;
            this.DisplayName = control.DisplayName;
            this.Image = control.Image;
            this.ShortDescription = control.ShortDescription;
            this.FullDescription = control.FullDescription;
            this.DocumentationURL = control.DocumentationURL;
            this.IsLatest = control.Latest > 0;
            this.IsFeatured = control.Featured > 0;
            this.IsNew = control.IsNew;
            this.IsCTP = control.IsCTP;
            this.IsBeta = control.IsBeta;
            this.IsUpdated = control.IsUpdated;
        }

        public string Name { get; }

        public string DisplayName { get; }

        public string Image { get; }

        public string ShortDescription { get; }

        public string FullDescription { get; }

        public string DocumentationURL { get; }

        public bool IsLatest { get; }

        public bool IsFeatured { get; }

        public bool IsNew { get; }

        public bool IsCTP { get; }

        public bool IsBeta { get; }

        public bool IsUpdated { get; }
    }
}
