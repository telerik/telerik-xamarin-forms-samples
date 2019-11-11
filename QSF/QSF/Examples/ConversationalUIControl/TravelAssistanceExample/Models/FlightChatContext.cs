using System.Collections.Generic;
using Telerik.XamarinForms.Common;

namespace QSF.Examples.ConversationalUIControl.TravelAssistanceExample.Models
{
    public class FlightChatContext : NotifyPropertyChangedBase
    {
        private string passangerName;
        private string totalTicketPrice;

        public string PassangerName
        {
            get
            {
                return this.passangerName;
            }
            set
            {
                if (this.passangerName != value)
                {
                    this.passangerName = value;
                    this.OnPropertyChanged(nameof(this.PassangerName));
                }
            }
        }

        public string TotalTicketPrice
        {
            get
            {
                return this.totalTicketPrice;
            }
            set
            {
                if (this.totalTicketPrice != value)
                {
                    this.totalTicketPrice = value;
                    this.OnPropertyChanged(nameof(this.TotalTicketPrice));
                }
            }
        }

        public IList<FlightInfo> Flights { get; set; }
    }
}