using System;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.Models
{
    public class ShippingAppointment : IAppointment
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
        public Color Color { get; set; }
        public bool IsAllDay { get; set; }
        public string Detail { get; set; }
        public string OrderId { get; set; }
    }
}