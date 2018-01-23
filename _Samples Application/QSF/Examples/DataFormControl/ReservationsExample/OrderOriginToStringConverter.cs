using System;
using Telerik.XamarinForms.Common.DataAnnotations;

namespace QSF.Examples.DataFormControl.ReservationsExample
{
    public class OrderOriginToStringConverter : IPropertyConverter
    {
        public object Convert(object value)
        {
            switch ((OrderOrigin)value)
            {
                case OrderOrigin.Phone:
                    return "Phone";

                case OrderOrigin.Inperson:
                    return "In-person";

                case OrderOrigin.Online:
                    return "On-line";

                case OrderOrigin.Other:
                    return "Other";

                default:
                    return "";
            }
        }

        public object ConvertBack(object value)
        {
            switch ((string)value)
            {
                case "Phone":
                    return OrderOrigin.Phone;

                case "In-person":
                    return OrderOrigin.Inperson;

                case "On-line":
                    return OrderOrigin.Online;

                case "Other":
                    return OrderOrigin.Other;

                default:
                    return OrderOrigin.Phone;
            }
        }

        public Type TargetType
        {
            get { return typeof(string); }
        }
    }
}