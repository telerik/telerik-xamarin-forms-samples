using QSF.Services;
using QSF.Services.Configuration;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QSF.ViewModels
{
    public class ControlExamplesViewModel : ExamplesViewModelBase
    {
        private Control control;
        private ObservableCollection<ExampleInfoViewModel> examples;
        private ExampleInfoViewModel selectedExample;

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

        protected override Task InitializeAsyncOverride(object parameter)
        {
            string controlName = (string)parameter;

            var controlsService = DependencyService.Get<IControlsService>();
            this.control = controlsService.GetControlByName(controlName);

            this.Title = controlName;
            this.CanChangeTheme = this.control.IsThemable;

            this.Examples = new ObservableCollection<ExampleInfoViewModel>(this.control.Examples.Select(p => new ExampleInfoViewModel(p)));

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
            Device.OpenUri(new Uri(this.control.DocumentationURL));

            return Task.FromResult(false);
        }
    }
}
