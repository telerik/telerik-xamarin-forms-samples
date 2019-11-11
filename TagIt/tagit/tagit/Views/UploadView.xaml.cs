using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tagit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UploadView : ContentView
    {
        public UploadView()
        {
            InitializeComponent();

            this.BindingContext = App.ViewModel.Upload;
        }
    }
}