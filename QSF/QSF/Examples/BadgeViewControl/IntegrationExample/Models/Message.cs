using Telerik.XamarinForms.Common;
using Xamarin.Forms;

namespace QSF.Examples.BadgeViewControl.IntegrationExample
{
    public class Message
    {
        public string Sender { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string TimeSended { get; set; }

        public bool IsUnread { get; set; } = false;

        public FontAttributes HighlightedText
        {
            get
            {
                return this.IsUnread ? FontAttributes.Bold : FontAttributes.None;
            }
        }

        public Visibility BadgeVisibility
        {
            get
            {
                return this.IsUnread ? Visibility.Visible : Visibility.Hidden;

            }
        }
    }
}
