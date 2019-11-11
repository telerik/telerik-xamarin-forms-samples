using Xamarin.Forms;

namespace TodoApp.Controls
{
    public class BorderlessEntry : Xamarin.Forms.Entry
    {
        public BorderlessEntry()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                this.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            }
        }
    }
}
