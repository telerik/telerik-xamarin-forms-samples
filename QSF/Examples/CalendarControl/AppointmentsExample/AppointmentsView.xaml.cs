using System;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.AppointmentsExample
{
    public partial class AppointmentsView : ContentView
    {
        public AppointmentsView()
        {
            this.InitializeComponent();

            this.calendar.SetStyleForCell = this.SetStyleForCell;
        }

        private CalendarCellStyle SetStyleForCell(CalendarCell cell)
        {
            if (cell.Type == CalendarCellType.DayName)
            {
                return new CalendarCellStyle
                {
                    BackgroundColor = Color.FromHex("EEEEEE"),
                    BorderColor = Color.Transparent,
                    FontSize = Device.OnPlatform<double>(10, 16, 16),
                    FontWeight = Telerik.XamarinForms.Common.FontWeight.Bold,
                    ForegroundColor = Color.FromHex("999999")
                };
            }

            if (cell.Type == CalendarCellType.WeekNumber)
            {
                return new CalendarCellStyle
                {
                    BackgroundColor = Color.FromHex("E5E5E5"),
                    BorderColor = Color.Transparent,
                    FontSize = Device.OnPlatform<double>(13, 24, 13),
                    ForegroundColor = Color.FromHex("A9A9A9")
                };
            }

            var defaultStyle = new CalendarCellStyle
            {
                BackgroundColor = Color.FromHex("EEEEEE"),
                BorderColor = Color.FromHex("CCCCCC"),
                BorderThickness = Device.OnPlatform<Thickness>(new Thickness(0, 0, 0, 1), 1, 0),
                FontSize = Device.OnPlatform<double>(15, 16, 0),

                ForegroundColor = Color.FromHex("333333")
            };

            var dayCell = cell as CalendarDayCell;

            if (dayCell != null)
            {
                if (dayCell.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    defaultStyle.ForegroundColor = Color.FromHex("D42A28");
                }

                if (dayCell.IsFromCurrentMonth)
                {
                    if (dayCell.IsToday)
                    {
                        defaultStyle.FontWeight = Telerik.XamarinForms.Common.FontWeight.Bold;
                    }
                }
                else
                {
                    if (dayCell.IsToday)
                    {
                        defaultStyle.ForegroundColor = Color.FromRgb(115, 174, 239);
                    }
                    else
                    {
                        defaultStyle.ForegroundColor = Color.FromHex("999999");
                        defaultStyle.BackgroundColor = Color.FromHex("E5E5E5");
                    }
                }

                if (dayCell.IsSelected)
                {
                    defaultStyle.ForegroundColor = Color.White;
                }

                return defaultStyle;
            }

            return null; // default style
        }
    }
}