using System.Collections.ObjectModel;
using Telerik.XamarinForms.Common;

namespace QSF.Examples.TemplatedPickerControl.Models
{
    public class Country : NotifyPropertyChangedBase
    {
        private string name;

        public Country()
        {
            this.Cities = new ObservableCollection<City>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value != this.name)
                {
                    this.UpdateValue(ref this.name, value);
                }
            }
        }

        public ObservableCollection<City> Cities { get; }
    }
}
