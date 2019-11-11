using System.Collections;
using System.Linq;
using Telerik.XamarinForms.Input.DataForm;

namespace QSF.Examples.DataFormControl.ReservationsExample
{
    public class UserPropertyDataSourceProvider : PropertyDataSourceProvider
    {
        public override IList GetSourceForKey(object key)
        {
            if (key.ToString().Equals(nameof(Reservation.TableNumber)))
            {
                return Enumerable.Range(1, 10).ToList();
            }

            return base.GetSourceForKey(key);
        }
    }
}
