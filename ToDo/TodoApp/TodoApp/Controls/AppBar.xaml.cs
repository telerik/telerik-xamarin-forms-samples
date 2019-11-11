using System.Linq;
using System.Windows.Input;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace TodoApp.Controls
{
    public partial class AppBar : TemplatedView
    {
        private static readonly BindablePropertyKey IsExpandedPropertyKey = BindableProperty.CreateReadOnly(nameof(IsExpanded),
            typeof(bool), typeof(AppBar), false);

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title),
            typeof(string), typeof(AppBar), string.Empty);

        public static readonly BindableProperty SearchPlaceholderProperty = BindableProperty.Create(nameof(SearchPlaceholder),
            typeof(string), typeof(AppBar), string.Empty);

        public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create(nameof(SearchCommand),
            typeof(ICommand), typeof(AppBar), null);

        public static readonly BindableProperty CancelSearchCommandProperty = BindableProperty.Create(nameof(CancelSearchCommand),
            typeof(ICommand), typeof(AppBar), null);

        public static readonly BindableProperty BurgerCommandProperty = BindableProperty.Create(nameof(BurgerCommand),
            typeof(ICommand), typeof(AppBar), null);

        public static readonly BindableProperty IsExpandedProperty = IsExpandedPropertyKey.BindableProperty;

        public AppBar()
        {
            this.InitializeComponent();

            var grid = this.Children.First();
            this.searchBar = grid.FindByName<SearchBar>("searchBar");
        }

        private SearchBar searchBar;

        public string Title
        {
            get => (string)this.GetValue(TitleProperty);
            set => this.SetValue(TitleProperty, value);
        }

        public string SearchPlaceholder
        {
            get => (string)this.GetValue(SearchPlaceholderProperty);
            set => this.SetValue(SearchPlaceholderProperty, value);
        }

        public ICommand SearchCommand
        {
            get => (ICommand)this.GetValue(SearchCommandProperty);
            set => this.SetValue(SearchCommandProperty, value);
        }

        public ICommand CancelSearchCommand
        {
            get => (ICommand)this.GetValue(CancelSearchCommandProperty);
            set => this.SetValue(CancelSearchCommandProperty, value);
        }

        public ICommand BurgerCommand
        {
            get => (ICommand)this.GetValue(BurgerCommandProperty);
            set => this.SetValue(BurgerCommandProperty, value);
        }

        public bool IsExpanded => (bool)GetValue(IsExpandedProperty);

        private void OnSearchButtonClicked(object sender, System.EventArgs e)
        {
            SetValue(IsExpandedPropertyKey, true);
            searchBar?.Focus();
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue) || e.OldTextValue == null || e.OldTextValue.Length <= 1)
                return;

            SetValue(IsExpandedPropertyKey, false);
            this.CancelSearchCommand?.Execute(null);
        }
    }
}