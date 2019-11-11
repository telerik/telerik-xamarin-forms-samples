using Telerik.XamarinForms.Common.DataAnnotations;

namespace QSF.Examples.DataFormControl.GlobalizationExample
{
    public class Person
    {
        [DisplayOptions(HeaderResourceKey = "Age",
                        PlaceholderTextResourceKey = "AgePlaceholder",
                        GroupResourceKey = "PrivateInfo")]
        public int? Age { get; set; }

        [DisplayOptions(HeaderResourceKey = "Weight",
                        PlaceholderTextResourceKey = "WeightPlaceholder",
                        GroupResourceKey = "PrivateInfo")]
        public int? Weight { get; set; }

        [DisplayOptions(HeaderResourceKey = "FirstName",
                        PlaceholderTextResourceKey = "FirstNamePlaceholder",
                        GroupResourceKey = "PublicInfo")]
        public string FirstName { get; set; }

        [DisplayOptions(HeaderResourceKey = "LastName",
                        PlaceholderTextResourceKey = "LastNamePlaceholder",
                        GroupResourceKey = "PublicInfo")]
        public string LastName { get; set; }

        [DisplayOptions(HeaderResourceKey = "Town",
                        PlaceholderTextResourceKey = "TownPlaceholder",
                        GroupResourceKey = "PublicInfo")]
        public string HomeTown { get; set; }

        [DisplayOptions(HeaderResourceKey = "Married",
                GroupResourceKey = "PrivateInfo")]
        public string Married { get; set; }
    }
}
