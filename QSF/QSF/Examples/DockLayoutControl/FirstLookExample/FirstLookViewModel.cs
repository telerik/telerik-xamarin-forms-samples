using QSF.ViewModels;
using System.Threading.Tasks;

namespace QSF.Examples.DockLayoutControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private DockLayoutConfigurationViewModel configurationViewModel = new DockLayoutConfigurationViewModel();

        public string TitlePanelDock
        {
            get
            {
                return this.configurationViewModel.TitlePanelDock;
            }
        }

        public string ListPanelDock
        {
            get
            {
                return this.configurationViewModel.ListPanelDock;
            }
        }

        public string NavigationPanelDock
        {
            get
            {
                return this.configurationViewModel.NavigationPanelDock;
            }
        }

        public bool IsTitlePanelVisible
        {
            get
            {
                return this.configurationViewModel.IsTitlePanelVisible;
            }
        }

        public bool IsListPanelVisible
        {
            get
            {
                return this.configurationViewModel.IsListPanelVisible;
            }
        }

        public bool IsNavigationPanelVisible
        {
            get
            {
                return this.configurationViewModel.IsNavigationPanelVisible;
            }
        }

        public bool IsContentPanelVisible
        {
            get
            {
                return this.configurationViewModel.IsContentPanelVisible;
            }
        }

        public override bool HasConfiguration => true;

        public override bool IsPopupHintOpen => true;

        protected override Task NavigateToConfigurationOverride()
        {
            return this.NavigationService.NavigateToConfigurationAsync(this.configurationViewModel);
        }

        internal override void OnAppearing()
        {
            base.OnAppearing();
            this.OnPropertyChanged(nameof(this.IsNavigationPanelVisible));
            this.OnPropertyChanged(nameof(this.IsListPanelVisible));
            this.OnPropertyChanged(nameof(this.IsTitlePanelVisible));
            this.OnPropertyChanged(nameof(this.IsContentPanelVisible));
            this.OnPropertyChanged(nameof(this.NavigationPanelDock));
            this.OnPropertyChanged(nameof(this.ListPanelDock));
            this.OnPropertyChanged(nameof(this.TitlePanelDock));
        }
    }
}