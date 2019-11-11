using Xamarin.Forms;

namespace ErpApp.Controls
{
    public partial class EmptySearchResultsView : TemplatedView
    {
        public EmptySearchResultsView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TermProperty = BindableProperty.Create(nameof(Term),
            typeof(string), typeof(EmptySearchResultsView), null, defaultBindingMode: BindingMode.OneWay);

        public string Term
        {
            get { return (string)GetValue(TermProperty); }
            set { SetValue(TermProperty, value); }
        }
    }
}
