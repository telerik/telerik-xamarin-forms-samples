using QSF.Services.Configuration;

namespace QSF.ViewModels
{
    public class ThemeViewModel : ViewModelBase
    {
        private bool isSelected;

        public ThemeViewModel(Theme theme)
        {
            this.Name = theme.Name;
            this.ResourceTypeName = theme.ResourceTypeName;
            this.Color1 = theme.Color1;
            this.Color2 = theme.Color2;
            this.Color3 = theme.Color3;
        }

        public string Name { get; private set; }

        public string ResourceTypeName { get; private set; }

        public string Color1 { get; private set; }

        public string Color2 { get; private set; }

        public string Color3 { get; private set; }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            internal set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
