using System;
using System.IO;
using ArtGalleryCRM.Forms.Models;
using Telerik.XamarinForms.ImageEditor;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.Views
{
    public partial class ImageEditorPage : BasePage
    {
        private readonly BaseDataObject _item;

        public ImageEditorPage()
        {
            InitializeComponent();
        }

        public ImageEditorPage(BaseDataObject item)
        {
            InitializeComponent();
            _item = item;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_item is Employee employee)
            {
                imageEditor.Source = employee.PhotoUri.StartsWith("http") 
                    ? ImageSource.FromUri(new Uri(employee.PhotoUri)) 
                    : ImageSource.FromFile(employee.PhotoUri);
            }

            if (_item is Product product)
            {
                imageEditor.Source = product.PhotoUri.StartsWith("http")
                    ? ImageSource.FromUri(new Uri(product.PhotoUri))
                    : ImageSource.FromFile(product.PhotoUri);
            }
        }

        private async void SaveToolbarItem_OnTapped(object sender, EventArgs e)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Success!",
                "You have successfully edited the image. However, since this is a readonly demo, the changes will not be persisted.",
                "OK");

            // READONLY DEMO - This demo is readonly, we're not saving the changes.
            // As an example, here are two possible ways you can save the changes.
            //using (var memStream = new MemoryStream())
            //{
            //    // Step 1. Save the image to the stream using Png format, with 100% quality.
            //    await imageEditor.SaveAsync(memStream, ImageFormat.Png, 1);

            //    // Step 2. Rewind the stream
            //    memStream.Position = 0;

            //    // Step 3. Upload/save image to final data location.

            //    // Example 1 - If you're using an Azure blob storage, you upload the file:
            //    // https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet#upload-blobs-to-a-container
            //    //BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            //    //BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
            //    //BlobClient blobClient = containerClient.GetBlobClient(fileName);
            //    //await blobClient.UploadAsync(memStream);

            //    // Example 2 - Saving byte[] to local database
            //    // https://stackoverflow.com/a/41337488
            //    //MyItem.ImageBytes = memStream.ToArray();
            //    //MyItem.SaveAsync();
            //}

            await App.RootPage.Detail.Navigation.PopModalAsync(true);
        }
    }
}