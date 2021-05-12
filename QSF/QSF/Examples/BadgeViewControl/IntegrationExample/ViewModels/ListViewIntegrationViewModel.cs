using System;
using System.Collections.ObjectModel;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;

namespace QSF.Examples.BadgeViewControl.IntegrationExample
{
    public class ListViewIntegrationViewModel
    {
        private int unreadUserMessages = 1;

        public ListViewIntegrationViewModel()
        {
            this.Users = new ObservableCollection<User>();
            this.Users.Add(new User() { Name = "Abbie Hunter", LastMessageReceived = "Thanks", ImageSourcePath = "DataGrid_SalesPerson_1.png", ActivityStatus = BadgeType.DoNotDisturb, LastMessageReceivedDate = "Yesterday" });
            this.Users.Add(new User() { Name = "Archie Wilson", LastMessageReceived = "See you tomorrow", ImageSourcePath = "DataGrid_SalesPerson_2.png", ActivityStatus = BadgeType.Available, UnreadMessagesText = "1", LastMessageReceivedDate = "10:20 AM" });
            this.Users.Add(new User() { Name = "Blake Richardson", LastMessageReceived = "File uploaded", ImageSourcePath = "DataGrid_SalesPerson_3.png", ActivityStatus = BadgeType.Away, LastMessageReceivedDate = "09:42 AM" });
            this.Users.Add(new User() { Name = "Bree Conner", LastMessageReceived = "ok", ImageSourcePath = "DataGrid_SalesPerson_4.png", ActivityStatus = BadgeType.OutOfOffice, LastMessageReceivedDate = "Yesterday" });
            this.Users.Add(new User() { Name = "Cody Fleming", LastMessageReceived = "Please see issue #228", ImageSourcePath = "DataGrid_SalesPerson_5.png", ActivityStatus = BadgeType.Available, UnreadMessagesText = "22", LastMessageReceivedDate = "2:30 PM" });
            this.Users.Add(new User() { Name = "Dallas Ruiz", LastMessageReceived = "Thanks", ImageSourcePath = "DataGrid_SalesPerson_6.png", ActivityStatus = BadgeType.Add, LastMessageReceivedDate = "Wednesday" });
            this.Users.Add(new User() { Name = "Lola Hall", LastMessageReceived = "Need help with a ticket", ImageSourcePath = "DataGrid_SalesPerson_7.png", ActivityStatus = BadgeType.Remove, LastMessageReceivedDate = "09:36 AM" });
            this.Users.Add(new User() { Name = "Madeleine Haynes", LastMessageReceived = "What do you think about the new des...", ImageSourcePath = "DataGrid_SalesPerson_8.png", ActivityStatus = BadgeType.Rejected, LastMessageReceivedDate = "10:39 AM" });
            this.Users.Add(new User() { Name = "Poppy Mills", LastMessageReceived = "George will do it", ImageSourcePath = "Person_9.png", ActivityStatus = BadgeType.Offline, LastMessageReceivedDate = "Saturday" });

            if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
            {
                Device.StartTimer(new TimeSpan(0, 0, 2), () =>
                {
                    this.unreadUserMessages++;
                    this.Users[1].UnreadMessagesText = unreadUserMessages.ToString();
                    return true;
                });
            }
        }

        public ObservableCollection<User> Users { get; set; }
    }
}
