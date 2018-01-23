using Telerik.XamarinForms.DataControls.ListView;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.AutoCompleteControl.TokensExample
{
    public partial class TokensView : ContentView
    {
        public TokensView()
        {
            this.InitializeComponent();

            BindableProperty property = null;
            StringArrayToStringConverter converter = null;

            switch(Device.RuntimePlatform)
            {
                case Device.iOS:
                case Device.Android:
                    property = RadAutoComplete.TokensProperty;
                    break;

                default:
                    property = RadAutoComplete.TextProperty;
                    converter = new StringArrayToStringConverter();
                    break;
            }

            Binding binding = new Binding(nameof(TokensViewModel.Tokens), BindingMode.OneWayToSource, converter);
            this.autoComplete.SetBinding(property, binding);
        }

        private void ListViewImages_SizeChanged(object sender, System.EventArgs e)
        {
            ListViewGridLayout listViewLayout = (ListViewGridLayout)this.listViewImages.LayoutDefinition;
            int desiredColumnsCount = listViewLayout.SpanCount;
            double spacing = (desiredColumnsCount - 1) * listViewLayout.HorizontalItemSpacing;
            double availableWidth = this.listViewImages.Width - spacing;
            double itemWidth = availableWidth / desiredColumnsCount;
            listViewLayout.ItemLength = itemWidth;
        }
    }
}