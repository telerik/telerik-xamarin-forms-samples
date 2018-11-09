using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tagit.ViewModels;
using Telerik.XamarinForms.Input;
using Telerik.XamarinForms.Input.AutoComplete;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tagit.Common;

namespace tagit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultsView : ContentView
    {
        public SearchResultsView()
        {
            InitializeComponent();

            App.ViewModel.SearchResults.Initialize();
            BindingContext = App.ViewModel.SearchResults;
        }
    }
}