using System;

namespace ErpApp
{
    public static class Constants
    {
        // TODO: Replace with your Azure Mobile App endpoint
        public static string ApplicationURL = "";
        // TODO: Provide image URL for the avatar of the customer
        public static readonly Uri EmptyCustomerImage = new Uri("ENTER_YOUR_CUSTOMER_AVATAR_URL");
        // TODO: Provide image URL for the avatar of the product
        public static readonly Uri EmptyProductImage = new Uri("ENTER_YOUR_PRODUCT_AVATAR_URL");
        // TODO: Provide image URL for the avatar of the vendor
        public static readonly Uri EmptyVendorImage = new Uri("ENTER_YOUR_VENDOR_AVATAR_URL");
    }
}