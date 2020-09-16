
using Telerik.XamarinForms.Common;
using Xamarin.Forms;

namespace QSF.Examples.CalendarControl.SchedulingUICustomizationExample
{
    public class OfficeRoom
    {
        public OfficeRoom(int roomNumber, Color color)
        {
            this.Number = roomNumber;
            this.Color = color;
        }

        public int Number { get; }
        public Color Color { get; }

        public string FullName
        {
            get
            {
                return $"Bedford - {this.Number}";
            }
        }
    }
}
