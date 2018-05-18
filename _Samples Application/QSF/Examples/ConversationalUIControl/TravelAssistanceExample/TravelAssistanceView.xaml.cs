using QSF.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.ConversationalUIControl.TravelAssistanceExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TravelAssistanceView : ContentView
    {
        public TravelAssistanceView()
        {
            InitializeComponent();
        }

        private void OnChatPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ClassStyle")
            {
                var themesService = DependencyService.Get<IThemesService>();
                if (themesService.CurrentTheme.Name == "Blue")
                {
                    this.Resources["ChatChoiceItemsColor"] = Color.FromHex("#3148CA");
                }
                else
                {
                    this.Resources["ChatChoiceItemsColor"] = Color.Accent;
                }
            }
        }
    }
}