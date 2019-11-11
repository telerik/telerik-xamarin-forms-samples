using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tagit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnalysisView : ContentView
    {
        public AnalysisView()
        {
            InitializeComponent();

            var viewModel = App.ViewModel.Analysis;
            viewModel.RefreshAnalysis();

            BindingContext = viewModel;
        }
    }
}