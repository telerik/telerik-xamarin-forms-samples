using System.Threading.Tasks;
using QSF.ViewModels;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.DataGrid;

namespace QSF.Examples.DataGridControl.EmptyTemplateExample
{
    public class EmptyTemplateViewModel : ExampleViewModel
    {
        private readonly EmptyTemplateConfigurationViewModel configurationViewModel;

        public EmptyTemplateViewModel()
        {
            this.configurationViewModel = new EmptyTemplateConfigurationViewModel();
        }

        public DataGridColumnCollection Columns
        {
            get => this.configurationViewModel.Columns;
            set => this.configurationViewModel.Columns = value;
        }

        public string EmptyContentImage => "nodata.png";

        public object ItemsSource => this.configurationViewModel.ItemsSource;

        public EmptyContentDisplayMode EmptyContentDisplayMode => this.configurationViewModel.EmptyContentDisplayMode;

        public override bool HasConfiguration => true;

        internal override void OnAppearing()
        {
            base.OnAppearing();
            this.OnPropertyChanged(nameof(this.ItemsSource));
            this.OnPropertyChanged(nameof(this.EmptyContentDisplayMode));
        }

        protected override Task NavigateToConfigurationOverride()
        {
            return this.NavigationService.NavigateToConfigurationAsync(this.configurationViewModel);
        }
    }
}
