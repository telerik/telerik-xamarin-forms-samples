using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ArtGalleryCRM.Forms.Common;
using ArtGalleryCRM.Forms.Models;
using ArtGalleryCRM.Forms.Services;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels
{
    public class ShippingViewModel : PageViewModelBase
    {
        private DateTime _calendarDisplayDate = DateTime.Now;

        public ShippingViewModel()
        {
            this.Title = "Shipping Calendar";
        }
        
        public ObservableCollection<ShippingAppointment> ShippingAppointments { get; } = new ObservableCollection<ShippingAppointment>();
        
        public DateTime CalendarDisplayDate
        {
            get => this._calendarDisplayDate;
            set => SetProperty(ref this._calendarDisplayDate, value);
        }

        public async Task LoadShippingDataAsync()
        {
            try
            {
                this.IsBusy = true;

                this.IsBusyMessage = "loading orders...";

                var fetch = await DependencyService.Get<IDataStore<Order>>().GetItemsAsync();

                this.IsBusyMessage = "creating appointments...";

                var tempList = new List<ShippingAppointment>();

                for (int i = 0; i < fetch.Count - 1; i++)
                {
                    var order = fetch[i];

                    var start = order.OrderDate.Date.AddHours(i);
                    var end = start.AddHours(1);

                    tempList.Add(new ShippingAppointment
                    {
                        OrderId = order.Id,
                        StartDate = start,
                        EndDate = end,
                        Title = order.DeliveryService,
                        Detail = $"Order Total: {order.TotalPrice:C2}",
                        IsAllDay = false,
                        Color = (Color)Application.Current.Resources["AccentTertiaryColor"]
                    });
                }
                
                if (this.ShippingAppointments.Any())
                {
                    this.ShippingAppointments.Clear();
                }

                foreach (var shippingAppointment in tempList)
                {
                    this.ShippingAppointments.Add(shippingAppointment);
                }
                
                this.CalendarDisplayDate = this.ShippingAppointments.Min(appointment => appointment.StartDate);
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = $"There was a problem loading shipping data, check your network connection and try again. Details: \r\n\n{ex.Message}",
                    Cancel = "OK"
                }, "Alert");
            }
            finally
            {
                this.IsBusyMessage = "";
                this.IsBusy = false;
            }
        }
    }
}