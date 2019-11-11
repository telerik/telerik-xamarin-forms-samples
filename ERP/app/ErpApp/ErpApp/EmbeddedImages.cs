using System;
using System.Reflection;
using Xamarin.Forms;

namespace ErpApp
{
    public static class EmbeddedImages
    {
        public static ImageSource LoginBackgroundImage =>
            ImageSource.FromResource("ErpApp.Assets.bg_login.png", typeof(EmbeddedImages).GetTypeInfo().Assembly);
        public static ImageSource MailImage =>
            ImageSource.FromResource("ErpApp.Assets.mail.png", typeof(EmbeddedImages).GetTypeInfo().Assembly);
        public static ImageSource PhoneImage =>
            ImageSource.FromResource("ErpApp.Assets.phone.png", typeof(EmbeddedImages).GetTypeInfo().Assembly);
        public static ImageSource NoItemsImage =>
            ImageSource.FromResource("ErpApp.Assets.no_items.png", typeof(EmbeddedImages).GetTypeInfo().Assembly);
    }
}
