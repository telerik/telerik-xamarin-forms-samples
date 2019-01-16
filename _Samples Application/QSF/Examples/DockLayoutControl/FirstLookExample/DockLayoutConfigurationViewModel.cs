using QSF.ViewModels;
using System.Collections.Generic;

namespace QSF.Examples.DockLayoutControl.FirstLookExample
{
    public class DockLayoutConfigurationViewModel : ConfigurationViewModel
    {
        private string titlePanelDock = "Top";
        private string listPanelDock = "Left";
        private string navigationPanelDock = "Bottom";
        private bool isTitlePanelVisible = true;
        private bool isListPanelVisible = true;
        private bool isNavigationPanelVisible = true;
        private bool isContentPanelVisible = true;

        public string TitlePanelDock
        {
            get
            {
                return this.titlePanelDock;
            }
            set
            {
                if (this.titlePanelDock != value)
                {
                    this.titlePanelDock = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string ListPanelDock
        {
            get
            {
                return this.listPanelDock;
            }
            set
            {
                if (this.listPanelDock != value)
                {
                    this.listPanelDock = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string NavigationPanelDock
        {
            get
            {
                return this.navigationPanelDock;
            }
            set
            {
                if (this.navigationPanelDock != value)
                {
                    this.navigationPanelDock = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsTitlePanelVisible
        {
            get
            {
                return this.isTitlePanelVisible;
            }
            set
            {
                if (this.isTitlePanelVisible != value)
                {
                    this.isTitlePanelVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsListPanelVisible
        {
            get
            {
                return this.isListPanelVisible;
            }
            set
            {
                if (this.isListPanelVisible != value)
                {
                    this.isListPanelVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsNavigationPanelVisible
        {
            get
            {
                return this.isNavigationPanelVisible;
            }
            set
            {
                if (this.isNavigationPanelVisible != value)
                {
                    this.isNavigationPanelVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsContentPanelVisible
        {
            get
            {
                return this.isContentPanelVisible;
            }
            set
            {
                if (this.isContentPanelVisible != value)
                {
                    this.isContentPanelVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public List<string> DockPickerOptions { get; set; } = new List<string> { "Top", "Bottom", "Left", "Right" };
    }
}
