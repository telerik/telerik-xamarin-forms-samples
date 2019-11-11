using System;
using System.IO;
using ArtGalleryCRM.Forms.Models;
using Telerik.XamarinForms.ImageEditor;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.Views
{
    public partial class ImageEditorPage : BasePage
    {
        private readonly Employee _employee;
        private readonly Product _product;

        public ImageEditorPage()
        {
            InitializeComponent();
        }

        public ImageEditorPage(Employee employee)
        {
            InitializeComponent();
            _employee = employee;
        }

        public ImageEditorPage(Product product)
        {
            InitializeComponent();
            _product = product;
        }

        protected override void OnAppearing()
        {
            // If coming from ProductEditPage, this will be executed
            if (_product != null && !string.IsNullOrEmpty(_product.PhotoUri))
            {
                // Need to make sure if the photo is a URL or a File path before assigning the source
                imageEditor.Source = Uri.IsWellFormedUriString(_product.PhotoUri, UriKind.RelativeOrAbsolute) 
                    ? ImageSource.FromUri(new Uri(_product.PhotoUri)) 
                    : ImageSource.FromFile(_product.PhotoUri);
            }

            // If coming from EmployeeEditPage, this will be executed
            if (_employee != null && !string.IsNullOrEmpty(_employee.PhotoUri))
            {
                imageEditor.Source = Uri.IsWellFormedUriString(_employee.PhotoUri, UriKind.RelativeOrAbsolute)
                    ? ImageSource.FromUri(new Uri(_employee.PhotoUri))
                    : ImageSource.FromFile(_employee.PhotoUri);
            }

            base.OnAppearing();
        }

        private async void SaveToolbarItem_OnTapped(object sender, EventArgs e)
        {
            // READONLY NOTE
            // At this point, you would typically update the resource for this image (on disk or on the server).
            // Since this is readonly demo, we're only saving a local copy that will show the new image until the data is refreshed.

            string photoPath = string.Empty;

            var localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            if (_product != null && !string.IsNullOrEmpty(_product.PhotoUri))
            {
                photoPath = Path.Combine(localFolder, $"Product_{_product.Id}.png");
            }
            else if (_employee != null && !string.IsNullOrEmpty(_employee.PhotoUri))
            {
                photoPath = Path.Combine(localFolder, $"Product_{_employee.Id}.png");
            }

            if (File.Exists(photoPath))
            {
                File.Delete(photoPath);
            }

            using (var fileStream = File.Create(photoPath))
            {
                await imageEditor.SaveAsync(fileStream, ImageFormat.Png, 1);
            }

            if (_product != null)
            {
                _product.PhotoUri = photoPath;
            }
            else if (_employee != null)
            {
                _employee.PhotoUri = photoPath;
            }

            await App.RootPage.Detail.Navigation.PopModalAsync(true);
        }
    }
}