using System;
using System.Diagnostics;
using ArtGalleryCRM.Forms.Common;
using ArtGalleryCRM.Forms.Models;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.Views
{
    public partial class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            this.InitializeComponent();
            this.MasterPage.ListView.ItemTapped += this.ListView_ItemTapped;

            // All MessageCenter alerts piped through this subscription
            MessagingCenter.Subscribe<MessagingCenterAlert> (this, "Alert", async (sender) =>
            {
                await DisplayAlert(sender.Title, sender.Message, sender.Cancel);

                sender.OnCompleted?.Invoke();
            });
        }

        private void ListView_ItemTapped(object sender, Telerik.XamarinForms.DataControls.ListView.ItemTapEventArgs e)
        {
            if (!(e.Item is NavigationMenuItem item))
            {
                return;
            }

            if (!(Activator.CreateInstance(item.TargetType) is Page page))
            {
                Debug.WriteLine($"ListView_ItemTapped - PageActivator failed : {item.TargetType} could not be created");
                return;
            }

            page.Title = item.Title;

            this.Detail = new NavigationPage(page);

            // Hide the pane on app launch if we're not running on desktop
            if (Device.RuntimePlatform != "UWP")
            {
                this.IsPresented = false;
            }
        }
    }
}