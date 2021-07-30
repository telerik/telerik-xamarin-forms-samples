using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;

namespace QSF.Examples.BadgeViewControl.IntegrationExample
{
    public class User : NotifyPropertyChangedBase
    {
        private string unreadMessagesText;
        private Color highLightedTextColor = Application.Current.RequestedTheme == OSAppTheme.Light ? Color.FromHex("#0E88F2") : Color.FromHex("#42A5F5");
        private Color defaultTextColor = Application.Current.RequestedTheme == OSAppTheme.Light ? Color.FromHex("#99000000") : Color.FromHex("#99FFFFFF");

        public string Name { get; set; }

        public string LastMessageReceived { get; set; }

        public string ImageSourcePath { get; set; }

        public BadgeType ActivityStatus { get; set; }

        public string UnreadMessagesText
        {
            get
            {
                return this.unreadMessagesText;
            }
            set
            {
                if (this.unreadMessagesText != value)
                {
                    this.unreadMessagesText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string LastMessageReceivedDate { get; set; }

        public Color LastMessageReceivedDateColor
        {
            get
            {
                return this.UnreadMessagesText != null ? this.highLightedTextColor : this.defaultTextColor;
            }
        }

        public FontAttributes MessageFontAttributes
        {
            get
            {
                return this.UnreadMessagesText != null ? FontAttributes.Bold : FontAttributes.None;
            }
        }

        public bool IsVisibleUnreadMessages
        {
            get
            {
                return this.UnreadMessagesText != null;
            }
        }
    }
}
