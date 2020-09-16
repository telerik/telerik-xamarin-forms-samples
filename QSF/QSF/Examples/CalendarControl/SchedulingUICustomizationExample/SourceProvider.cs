using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.SchedulingUICustomizationExample
{
    public class SourceProvider
    {
        private static List<OfficeRoom> DefaultLocations;
        private static List<Alert> DefaultAlerts;
        private static List<Priority> DefaultPriorities;

        static SourceProvider()
        {
            DefaultLocations = new List<OfficeRoom>()
            {
                new OfficeRoom(201, Color.FromHex("#0E88F2")),
                new OfficeRoom(301, Color.FromHex("#FF6F00")),
                new OfficeRoom(302, Color.FromHex("#FFAC3E"))
            };

            DefaultAlerts = Enum.GetValues(typeof(Alert)).Cast<Alert>().ToList();
            DefaultPriorities = Enum.GetValues(typeof(Priority)).Cast<Priority>().ToList();
        }

        public List<OfficeRoom> Locations 
        {
            get 
            {
                return DefaultLocations;
            }
        }

        public List<Alert> Alerts
        {
            get
            {
                return DefaultAlerts;
            }
        }

        public List<Priority> Priorities
        {
            get
            {
                return DefaultPriorities;
            }
        }

        public static List<OfficeRoom> GetLocations()
        {
            return DefaultLocations;
        }
    }
}
