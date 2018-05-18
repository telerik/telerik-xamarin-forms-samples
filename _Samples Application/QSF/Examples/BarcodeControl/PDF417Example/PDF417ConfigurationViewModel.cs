using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.XamarinForms.Barcode;

namespace QSF.Examples.BarcodeControl.PDF417Example
{
    public class PDF417ConfigurationViewModel : ConfigurationViewModel
    {
        private string[] countrySource;
        private string selectedCountry;
        private string city;
        private DateTime selectedDate;
        private string fullName;
        private string address;
        private string phoneNumber;
        private string orderNumber;

        public PDF417ConfigurationViewModel()
        {
            this.CountrySource = new string[] { "Afghanistan", "Albania", "Algeria", "American Samoa", "Andorra", "Angola", "Anguilla", "Antarctica", "Antigua & Barbuda", "Antilles, Netherlands", "Arabia, Saudi", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas, The", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Bouvet Island", "Brazil", "British Indian Ocean T.", "British Virgin Islands", "Brunei Darussalam", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Canada", "Cape Verde", "Caribbean, the", "Cayman Islands", "Central African Republic", "Chad", "Chile", "China", "Christmas Island", "Cocos (Keeling) Islands", "Colombia", "Comoros", "Congo", "Congo, Dem. Rep. of the", "Cook Islands", "Costa Rica", "Cote D'Ivoire", "Croatia", "Cuba", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "East Timor (Timor-Leste)", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Europe", "Falkland Islands (Malvinas)", "Faroe Islands", "Fiji", "Finland", "France", "French Guiana", "French Polynesia", "French Southern Terr.", "Gabon", "Gambia, the", "Georgia", "Germany", "Ghana", "Gibraltar", "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", "Guatemala", "Guernsey and Alderney", "Guiana, French", "Guinea", "Guinea-Bissau", "Guinea, Equatorial", "Guyana", "Haiti", "Heard & McDonald Is.(AU)", "Holy See (Vatican)", "Honduras", "Hong Kong, (China)", "Hungary", "Iceland", "India", "Indonesia", "Iran, Islamic Republic of", "Iraq", "Ireland", "Israel", "Italy", "Ivory Coast (Cote d'Ivoire)", "Jamaica", "Japan", "Jersey", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Korea Dem. People's Rep.", "Korea, (South) Republic of", "Kosovo", "Kuwait", "Kyrgyzstan", "Lao People's Democ. Rep.", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libyan Arab Jamahiriya", "Liechtenstein", "Lithuania", "Luxembourg", "Macao, (China)", "Macedonia, TFYR", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Man, Isle of", "Marshall Islands", "Martinique (FR)", "Mauritania", "Mauritius", "Mayotte (FR)", "Mexico", "Micronesia, Fed. States of", "Moldova, Republic of", "Monaco", "Mongolia", "Montenegro", "Montserrat", "Morocco", "Mozambique", "Myanmar (ex-Burma)", "Namibia", "Nauru", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "Niue", "Norfolk Island", "Northern Mariana Islands", "Norway", "Oman", "Pakistan", "Palau", "Palestinian Territory", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Pitcairn Island", "Poland", "Portugal", "Puerto Rico", "Qatar", "Reunion (FR)", "Romania", "Russia", "Rwanda", "Sahara, Western", "Saint Barthelemy (FR)", "Saint Helena (UK)", "Saint Kitts and Nevis", "Saint Lucia", "Saint Martin (FR)", "S Pierre & Miquelon(FR)", "S Vincent & Grenadines", "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South America", "S.George & S.Sandwich", "South Sudan", "Spain", "Sri Lanka (ex-Ceilan)", "Sudan", "Suriname", "Svalbard & Jan Mayen Is.", "Swaziland", "Sweden", "Switzerland", "Syrian Arab Republic", "Taiwan", "Tajikistan", "Tanzania, United Rep. of", "Thailand", "Timor-Leste (East Timor)", "Togo", "Tokelau", "Tonga", "Trinidad & Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks and Caicos Is.", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States", "US Minor Outlying Isl.", "Uruguay", "Uzbekistan", "Vanuatuv", "Venezuela", "Viet Nam", "Virgin Islands, British", "Virgin Islands, U.S.", "Wallis and Futuna", "Western Sahara", "Yemen", "Zambia", "Zimbabwe" };
            this.OrderNumber = "7374369";
            this.SelectedCountry = "Bulgaria";
            this.City = "Sofia";
            this.Address = "bul. Alexander Malinov 33";
            this.SelectedDate = new DateTime(2020, 5, 15);
            this.FullName = "Brian Anderson";
            this.PhoneNumber = "+359 883 426 531";
        }

        public string[] CountrySource
        {
            get
            {
                return this.countrySource;
            }
            private set
            {
                if (this.countrySource != value)
                {
                    this.countrySource = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string SelectedCountry
        {
            get
            {
                return this.selectedCountry;
            }
            set
            {
                if (this.selectedCountry != value)
                {
                    this.selectedCountry = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string City
        {
            get
            {
                return this.city;
            }
            set
            {
                if (this.city != value)
                {
                    this.city = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                if (this.address != value)
                {
                    this.address = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public DateTime SelectedDate
        {
            get
            {
                return this.selectedDate;
            }
            set
            {
                if (this.selectedDate != value)
                {
                    this.selectedDate = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string FullName
        {
            get
            {
                return this.fullName;
            }
            set
            {
                if (this.fullName != value)
                {
                    this.fullName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string PhoneNumber
        {
            get
            {
                return this.phoneNumber;
            }
            set
            {
                if (this.PhoneNumber != value)
                {
                    this.phoneNumber = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string OrderNumber
        {
            get
            {
                return this.orderNumber;
            }
            set
            {
                if (this.orderNumber != value)
                {
                    this.orderNumber = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
