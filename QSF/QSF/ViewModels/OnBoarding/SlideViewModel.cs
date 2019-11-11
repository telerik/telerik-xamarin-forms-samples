using QSF.Services.Configuration;

namespace QSF.ViewModels
{
    public class SlideViewModel : ViewModelBase
    {
        public SlideViewModel(Slide slideConfig)
        {
            this.Title = slideConfig.Title;
            this.Content = slideConfig.Content;
            this.HasIcon = !string.IsNullOrEmpty(slideConfig.Icon);
            this.Icon = slideConfig.Icon;
        }

        public string Title { get; }

        public string Content { get; }

        public bool HasIcon { get; }

        public string Icon { get; }
    }
}
