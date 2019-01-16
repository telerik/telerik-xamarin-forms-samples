using System;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.MonthViewExample
{
    public partial class MonthViewView : ContentView
    {
        public MonthViewView()
        {
            this.InitializeComponent();

            this.calendar.SetStyleForCell = this.SetStyleForCell;
        }

        private CalendarCellStyle SetStyleForCell(CalendarCell cell)
        {
            double dayNameFontSize = 0;
            double weekNumberFontSize = 0;
            double defaultFontSize = 0;
            Thickness defaultThickness = new Thickness(1);

            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    {
                        dayNameFontSize = 16;
                        weekNumberFontSize = 13;
                        defaultFontSize = 0;
                        defaultThickness = new Thickness(0);
                    }
                    break;
                case Device.Android:
                    {
                        dayNameFontSize = 16;
                        weekNumberFontSize = 24;
                        defaultFontSize = 16;
                        defaultThickness = new Thickness(1);
                    }
                    break;
                case Device.iOS:
                    {
                        dayNameFontSize = 10;
                        weekNumberFontSize = 13;
                        defaultFontSize = 15;
                        defaultThickness = new Thickness(0, 0, 0, 1);
                    }
                    break;
            }

            if (cell.Type == CalendarCellType.DayName)
            {
                return new CalendarCellStyle
                {
                    BackgroundColor = Color.FromHex("EEEEEE"),
                    BorderColor = Color.Transparent,
                    FontSize = dayNameFontSize,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromHex("999999")
                };
            }

            if (cell.Type == CalendarCellType.WeekNumber)
            {
                return new CalendarCellStyle
                {
                    BackgroundColor = Color.FromHex("E5E5E5"),
                    BorderColor = Color.Transparent,
                    FontSize = weekNumberFontSize,
                    TextColor = Color.FromHex("A9A9A9")
                };
            }

            var defaultStyle = new CalendarCellStyle
            {
                BackgroundColor = Color.FromHex("EEEEEE"),
                BorderColor = Color.FromHex("CCCCCC"),
                BorderThickness = defaultThickness,
                FontSize = defaultFontSize,

                TextColor = Color.FromHex("333333")
            };

            var dayCell = cell as CalendarDayCell;

            if (dayCell != null)
            {
                if (dayCell.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    defaultStyle.TextColor = Color.FromHex("D42A28");
                }

                if (dayCell.IsFromCurrentMonth)
                {
                    if (dayCell.IsToday)
                    {
                        defaultStyle.FontAttributes = FontAttributes.Bold;
                    }
                }
                else
                {
                    if (dayCell.IsToday)
                    {
                        defaultStyle.TextColor = Color.FromRgb(115, 174, 239);
                    }
                    else
                    {
                        defaultStyle.TextColor = Color.FromHex("999999");
                        defaultStyle.BackgroundColor = Color.FromHex("E5E5E5");
                    }
                }

                if (dayCell.IsSelected)
                {
                    defaultStyle.TextColor = Color.White;
                }

                return defaultStyle;
            }

            return null; // default style
        }
    }
}