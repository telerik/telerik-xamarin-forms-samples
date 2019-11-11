using System;
using System.Diagnostics;
using Telerik.XamarinForms.Input;

namespace ArtGalleryCRM.Forms.Views
{
    public partial class ShippingPage
    {
        public ShippingPage()
        {
            this.InitializeComponent();
        }
        
        private async void RadCalendar_OnNativeControlLoaded(object sender, EventArgs e)
        {
            try
            {
                this.ViewModel.IsBusy = true;
                this.ViewModel.IsBusyMessage = "loading appointments...";

                this.radCalendar.TrySetViewMode(CalendarViewMode.MultiDay);
                
                await this.ViewModel.LoadShippingDataAsync();
                
                this.radCalendar.DisplayDate = this.ViewModel.CalendarDisplayDate;
                this.radCalendar.SelectedDate = this.ViewModel.CalendarDisplayDate;

                this.ViewModel.IsBusyMessage = "";
                this.ViewModel.IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"RadCalendar_OnNativeControlLoaded Exception: {ex}");
            }
        }
    }
}