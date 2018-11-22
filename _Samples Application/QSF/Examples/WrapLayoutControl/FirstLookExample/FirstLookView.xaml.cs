using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.WrapLayoutControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : ContentView
    {
        List<string> ImageNames { get;set; }

        public FirstLookView()
        {
            InitializeComponent();
            this.ImageNames = this.GetImages();
            this.LoadImages();
        }

        private void LoadImages()
        {
            if (Device.RuntimePlatform == "iOS" || Device.RuntimePlatform == "Android")
            {
                foreach (var item in this.ImageNames)
                {
                    this.wrp.Children.Add(new Image
                    {
                        Source = new FileImageSource
                        {
                            File = item
                        }
                    });
                }
            }
            else if (Device.RuntimePlatform == "UWP")
            {
                foreach (var item in this.ImageNames)
                {
                    this.wrp.Children.Add(new Image
                    {
                        Source = new FileImageSource
                        {
                            File = "Assets/" + item
                        }
                    });
                }
            }

        }

        private List<string> GetImages()
        {
            var result = new List<string>();
            result.Add("autocomplete_tokens_beach.jpg");
            result.Add("autocomplete_tokens_buenos_aires_hotel.jpg");
            result.Add("autocomplete_tokens_bulgaria_sunny_beach.jpg");
            result.Add("autocomplete_tokens_bulgaria_tevno_lake.jpg");
            result.Add("autocomplete_tokens_castel_sant_angelo.jpg");
            result.Add("autocomplete_tokens_church_st_george_attraction.jpg");
            result.Add("autocomplete_tokens_featherbed_nature_reserve_attraction.jpg");
            result.Add("autocomplete_tokens_forbidden_city_attraction.jpg");
            result.Add("autocomplete_tokens_forest_bulgaria.jpg");
            result.Add("autocomplete_tokens_georgetown_university.jpg");
            result.Add("autocomplete_tokens_key_spices_indian_cuisine.jpg");
            result.Add("autocomplete_tokens_kustendil_bulgaria.jpg");
            result.Add("autocomplete_tokens_leopard_samburu_reserve.jpg");
            result.Add("autocomplete_tokens_lioness_nakuru_national_park_kenya.jpg");
            return result;
        }
    }
}