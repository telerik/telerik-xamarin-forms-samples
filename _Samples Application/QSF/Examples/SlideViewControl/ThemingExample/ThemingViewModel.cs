using System.Collections.ObjectModel;
using QSF.ViewModels;

namespace QSF.Examples.SlideViewControl.ThemingExample
{
    public class ThemingViewModel : ViewModelBase
    {
        public ObservableCollection<string> Items { get; private set; }

        public ThemingViewModel()
        {
            this.Items = new ObservableCollection<string>
            {
                "Theming support & color customization",
                "Scroll Directions - specifies possible direction for scrolling",
                "DataTemplates and DataTemplateSelectors"
            };
        }
    }
}
