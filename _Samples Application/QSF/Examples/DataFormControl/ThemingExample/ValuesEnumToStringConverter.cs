using System;
using Telerik.XamarinForms.Common.DataAnnotations;

namespace QSF.Examples.DataFormControl.ThemingExample
{
    public class ValuesEnumToStringConverter : IPropertyConverter
    {
        public object Convert(object value)
        {
            switch ((EnumValue)value)
            {
                case EnumValue.Value1:
                    return "Value 1";

                case EnumValue.Value2:
                    return "Value 2";

                case EnumValue.Value3:
                    return "Value 3";

                default:
                    return "";
            }
        }

        public object ConvertBack(object value)
        {
            switch ((string)value)
            {
                case "Value 1":
                    return EnumValue.Value1;

                case "Value 2":
                    return EnumValue.Value2;

                case "Value 3":
                    return EnumValue.Value3;

                default:
                    return EnumValue.Value1;
            }
        }

        public Type TargetType
        {
            get { return typeof(string); }
        }
    }
}
