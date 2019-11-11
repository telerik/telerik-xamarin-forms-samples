using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tagit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Xml.Linq;

namespace tagit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();

           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (App.ViewModel == null)
            {
                App.ViewModel = new MainViewModel();
            }

            this.BindingContext = App.ViewModel;

            InitializeEvents();            
        }

        private void InitializeEvents()
        {
            uploadButton.Source = ImageSource.FromResource("tagit.Images.ic_upload.png");
        }
        
        private void OnUploadTapped(object sender, EventArgs e)
        {   
            Navigation.PopModalAsync(true);
            Navigation.PushModalAsync(new Views.PickerPage(), true);
        }
        
    }
}