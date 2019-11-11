using Xamarin.Forms;

namespace TodoApp.Pages
{
	public partial class TextInputPage : ContentPage
	{
		public TextInputPage()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.editor.Focus();
        }
    }
}