using Telerik.XamarinForms.Input;

namespace QSF.Examples.CalendarControl.SpecialSlotsExample
{
    public class HolidaySlot : SpecialSlot
    {
        private bool isRegional;
        private string name;

        public bool IsRegional
        {
            get
            {
                return this.isRegional;
            }
            set
            {
                if (this.isRegional != value)
                {
                    this.isRegional = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
