using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;

namespace QSF.Examples.BarcodeControl.QRCodeExample
{
    public partial class QRCodeConfigurationView : ContentView
    {
        public QRCodeConfigurationView()
        {
            InitializeComponent();

            this.tabView.Header.SetAppThemeColor(TabViewHeader.BackgroundColorProperty, (Color)App.Current.Resources["AccentColorLight"], (Color)App.Current.Resources["AccentColorDark"]);

            this.tabView.PropertyChanged += this.TabView_PropertyChanged;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (this.BindingContext != null)
            {
                var viewModel = (QRCodeConfigurationViewModel)this.BindingContext;
                this.tabView.SelectedItem = this.tabView.Items[viewModel.SelectedTabIndex];
            }
        }

        private void TabView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == RadTabView.SelectedItemProperty.PropertyName)
            {
                var selectedIndex = this.tabView.Items.IndexOf((TabViewItem)this.tabView.SelectedItem);

                var viewModel = (QRCodeConfigurationViewModel)this.BindingContext;

                viewModel.SelectedTabIndex = selectedIndex;
            }
        }
    }
}