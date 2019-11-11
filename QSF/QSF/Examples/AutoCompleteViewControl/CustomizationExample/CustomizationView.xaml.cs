using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.AutoCompleteViewControl.CustomizationExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomizationView : ContentView
    {
        public CustomizationView()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var closeLabel = sender as Label;
            if (closeLabel != null)
            {
                var item = closeLabel.BindingContext as JobCategory;
                if (item != null)
                {
                    this.autoCompleteViewCustomizationExample.Tokens.Remove(item);
                }
            }
        }
    }
}