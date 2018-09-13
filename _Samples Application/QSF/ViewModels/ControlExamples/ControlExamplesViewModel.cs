using QSF.Services;
using QSF.Services.Configuration;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Telerik.XamarinForms.DataControls.ListView;
using Xamarin.Forms;

namespace QSF.ViewModels
{
    public class ControlExamplesViewModel : ExamplesViewModelBase
    {
        private Control control;
        private ObservableCollection<ExampleInfoViewModel> examples;
        private ExampleInfoViewModel selectedExample;
        private bool canCollapseGroups;

        public ObservableCollection<ExampleInfoViewModel> Examples
        {
            get
            {
                return this.examples;
            }
            private set
            {
                if (this.examples != value)
                {
                    this.examples = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ExampleInfoViewModel SelectedExample
        {
            get
            {
                return this.selectedExample;
            }
            set
            {
                if (this.selectedExample != value)
                {
                    this.selectedExample = value;
                    this.OnSelectedExampleChanged();
                    this.OnPropertyChanged();
                }
            }
        }

        public bool CanCollapseGroups
        {
            get
            {
                return this.canCollapseGroups;
            }
            private set
            {
                if (this.canCollapseGroups != value)
                {
                    this.canCollapseGroups = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public override bool HasCode
        {
            get
            {
                return false;
            }
        }

        public override bool HasDocumentation
        {
            get
            {
                return true;
            }
        }

        public Command<GroupHeaderContext> GroupHeaderTapCommand { get; private set; }

        public ControlExamplesViewModel()
        {
            this.GroupHeaderTapCommand = new Command<GroupHeaderContext>(this.OnGroupHeaderTap);
        }

        private void OnGroupHeaderTap(GroupHeaderContext context)
        {
            if (this.CanCollapseGroups)
            {
                context.IsExpanded = !context.IsExpanded;
            }
        }

        protected override Task InitializeAsyncOverride(object parameter)
        {
            string controlName = (string)parameter;

            var controlsService = DependencyService.Get<IControlsService>();
            this.control = controlsService.GetControlByName(controlName);

            this.Title = this.control.DisplayName;
            this.CanChangeTheme = this.control.IsThemable;

            this.Examples = new ObservableCollection<ExampleInfoViewModel>(this.control.Examples.Select(p => new ExampleInfoViewModel(p)));
            this.CanCollapseGroups = this.Examples.Select(example => example.GroupName).Distinct().Count() > 1;

            return Task.FromResult<bool>(false);
        }

        protected override Task NavigateToCodeOverride()
        {
            throw new InvalidOperationException("This view is not expected to have code to show.");
        }

        private void OnSelectedExampleChanged()
        {
            if (this.SelectedExample != null)
            {
                var task = this.NavigateToExample(this.SelectedExample);
                if (task.Exception != null)
                {
                    throw task.Exception;
                }

                this.SelectedExample = null;
            }
        }

        private Task NavigateToExample(ExampleInfoViewModel selectedExample)
        {
            return this.NavigationService.NavigateToExampleAsync(new ExampleInfo(this.control.Name, selectedExample.Example.Name));
        }

        protected override Task NavigateToInfoOverride()
        {
            InfoViewSettings settings = new InfoViewSettings(InfoType.ControlInfo, this.control.DescriptionHeader, this.control.FullDescription);
            return this.NavigationService.NavigateToAsync<InfoViewModel>(settings);
        }

        protected override Task NavigateToDocumentationOverride()
        {
            AnalyticsHelper.TraceNavigateToDocumentation(this.control.Name);
            Device.OpenUri(new Uri(this.control.DocumentationURL));

            return Task.FromResult(false);
        }
    }
}
