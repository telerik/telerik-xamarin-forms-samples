using System;
using System.Linq;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.AutoCompleteViewControl.RemoteSearchExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RemoteSearchView : ContentView
    {
        private RemoteSearchViewModel viewModel;

        public RemoteSearchView()
        {
            InitializeComponent();

            this.viewModel = new RemoteSearchViewModel();
            this.BindingContext = this.viewModel;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var autoCompleteView = (RadAutoCompleteView)sender;
            string searchText = e.NewTextValue ?? "";

            if (searchText.Length >= autoCompleteView.SearchThreshold && autoCompleteView.ShowSuggestionView)
            {
                if (string.IsNullOrEmpty(e.OldTextValue) && autoCompleteView.ItemsSource == null)
                {
                    Device.StartTimer(TimeSpan.FromMilliseconds(1500), () =>
                    {
                        autoCompleteView.ItemsSource = this.viewModel.MovieSource.Where(i => i.Title.ToLower().Contains(searchText.ToLower()));
                        return false;
                    });
                }
            }
            else
            {
                autoCompleteView.ItemsSource = null;
            }
        }

        private void OnSuggestionItemSelected(object sender, Telerik.XamarinForms.Input.AutoComplete.SuggestionItemSelectedEventArgs e)
        {
            this.viewModel.ShowDetails((Movie)e.DataItem);
        }
    }
}