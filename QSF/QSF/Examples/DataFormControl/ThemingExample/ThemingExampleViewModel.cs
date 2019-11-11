using QSF.ViewModels;
using System;
using Telerik.XamarinForms.Common.DataAnnotations;

namespace QSF.Examples.DataFormControl.ThemingExample
{
    public class ThemingExampleViewModel : BindableBase
    {
        private EnumValue enumPick = EnumValue.Value3;
        private int listPick = 33;
        private string stringValue = "String";
        private EnumValue enumSegment = EnumValue.Value1;
        private int intSegment = 1;
        private double numberValue = 23;
        private DateTime dateValue = new DateTime(2017, 8, 29);
        private DateTime timeValue = new DateTime(2017, 8, 29);
        private bool checkValue1 = true;
        private bool checkValue2;
        private bool toggleValue;
        private bool toggleValue2 = true;
        private float baseValue = -7;

        [DisplayOptions(Group = "", Position = 0, ColumnSpan =2, Header = "EnumPick", PlaceholderText = "enum pick")]
        public EnumValue EnumPick
        {
            get
            {
                return this.enumPick;
            }
            set
            {
                if (this.enumPick != value)
                {
                    this.enumPick = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayOptions(Group = "", Position = 1, ColumnSpan = 2, Header = "ListPick", PlaceholderText = "list pick")]
        [DataSourceKey("ListPick")]
        public int ListPick
        {
            get
            {
                return this.listPick;
            }
            set
            {
                if (this.listPick != value)
                {
                    this.listPick = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayOptions(Group = "", Position = 2, ColumnSpan = 2, Header = "StringValue", PlaceholderText = "string value")]
        public string StringValue
        {
            get
            {
                return this.stringValue;
            }
            set
            {
                if (this.stringValue != value)
                {
                    this.stringValue = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayOptions(Group = "", Position = 3, ColumnSpan = 2, Header = "EnumSegment", PlaceholderText = "enum segment")]
        public EnumValue EnumSegment
        {
            get
            {
                return this.enumSegment;
            }
            set
            {
                if (this.enumSegment != value)
                {
                    this.enumSegment = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayOptions(Group = "", Position = 4, ColumnSpan = 2, Header = "IntSegment", PlaceholderText = "int sgment")]
        [DataSourceKey("IntSegment")]
        public int IntSegment
        {
            get
            {
                return this.intSegment;
            }
            set
            {
                if (this.intSegment != value)
                {
                    this.intSegment = value;
                    this.OnPropertyChanged();
                }
            }
        }


        [DisplayOptions(Group = "", Position = 5, ColumnSpan = 2, Header = "NumberValue", PlaceholderText = "number value")]
        public double NumberValue
        {
            get
            {
                return this.numberValue;
            }
            set
            {
                if (this.numberValue != value)
                {
                    this.numberValue = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayOptions(Group = "", Position = 6, ColumnSpan = 2, Header = "DateValue", PlaceholderText = "date value")]
        public DateTime DateValue
        {
            get
            {
                return this.dateValue;
            }
            set
            {
                if (this.dateValue != value)
                {
                    this.dateValue = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayOptions(Group = "", Position = 7, ColumnSpan = 2, Header = "TimeValue", PlaceholderText = "time value")]
        public DateTime TimeValue
        {
            get
            {
                return this.timeValue;
            }
            set
            {
                if (this.timeValue != value)
                {
                    this.timeValue = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayOptions(Group = "", ColumnSpan = 1, Position = 8, Header = "CheckValue1")]
        public bool CheckValue1
        {
            get
            {
                return this.checkValue1;
            }
            set
            {
                if (this.checkValue1 != value)
                {
                    this.checkValue1 = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayOptions(Group = "", ColumnSpan = 1, Position = 8, ColumnPosition = 1, Header = "CheckValue2")]
        public bool CheckValue2
        {
            get
            {
                return this.checkValue2;
            }
            set
            {
                if (this.checkValue2 != value)
                {
                    this.checkValue2 = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayOptions(Group = "", ColumnSpan = 1, Position = 9, Header = "ToggleValue", PlaceholderText = "toggle value")]
        public bool ToggleValue
        {
            get
            {
                return this.toggleValue;
            }
            set
            {
                if (this.toggleValue != value)
                {
                    this.toggleValue = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayOptions(Group = "", ColumnSpan = 1, Position = 9, ColumnPosition = 1, Header = "ToggleValue2")]
        public bool ToggleValue2
        {
            get
            {
                return this.toggleValue2;
            }
            set
            {
                if (this.toggleValue2 != value)
                {
                    this.toggleValue2 = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [DisplayOptions(Group = "", Position = 10, ColumnSpan = 2, Header = "BaseValue", PlaceholderText = "float value")]
        [NumericalRangeValidator(-10, -5, "between -10 and -5")]
        public float BaseValue
        {
            get
            {
                return this.baseValue;
            }
            set
            {
                if (this.baseValue != value)
                {
                    this.baseValue = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
