using System;

namespace ErpApp
{
    public static class Constants
    {
        public static string ApplicationURL = @"https://telerikerpapp.azurewebsites.net/";
        public static readonly Uri EmptyCustomerImage = new Uri("https://telerikerpappstorage.blob.core.windows.net/erpapppics/Person_Avatar.png");
        public static readonly Uri EmptyProductImage = new Uri("https://telerikerpappstorage.blob.core.windows.net/erpapppics/Product_Avatar.png");
        public static readonly Uri EmptyVendorImage = new Uri("https://telerikerpappstorage.blob.core.windows.net/erpapppics/Vendor_Avatar.png");
    }
}