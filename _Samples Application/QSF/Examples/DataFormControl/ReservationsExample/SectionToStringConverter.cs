using Telerik.XamarinForms.Common.DataAnnotations;

namespace QSF.Examples.DataFormControl.ReservationsExample
{
    public class SectionToStringConverter : IPropertyConverter
    {
        public object Convert(object value)
        {
            switch ((Section)value)
            {
                case Section.Patio:
                    return "Patio";
                case Section.FirstFloor:
                    return "FirstFloor";
                case Section.SecondFloor:
                    return "SecondFloor";
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value)
        {
            switch ((string)value)
            {
                case "Patio":
                    return Section.Patio;

                case "FirstFloor":
                    return Section.FirstFloor;

                case "SecondFloor":
                    return Section.SecondFloor;

                default:
                    return Section.Patio;
            }
        }
    }
}
