using System;
using System.Collections.Generic;
using QSF.ViewModels;

namespace QSF.Examples.DataGridControl.Common
{
    public class SalesPerson : ViewModelBase
    {
        private string firstName;
        private string lastName;
        private int sales;
        private string city;
        private string countryName;
        private string region;
        private List<string> regions;
        private List<string> images;
        private string image;

        private static Random random = new Random();

        public SalesPerson()
        {
            this.regions = new List<string>
            {
                "North",
                "South",
                "Central",
                "East",
                "West"
            };

            this.images = new List<string>
            {
                "DataGrid_SalesPerson_1.png",
                "DataGrid_SalesPerson_2.png",
                "DataGrid_SalesPerson_3.png",
                "DataGrid_SalesPerson_4.png",
                "DataGrid_SalesPerson_5.png",
                "DataGrid_SalesPerson_6.png",
                "DataGrid_SalesPerson_7.png",
                "DataGrid_SalesPerson_8.png",
            };

            this.region = this.regions[random.Next(0, this.regions.Count)];
            this.sales = random.Next(2, 100) * random.Next(100, 999);
            this.image = this.images[random.Next(0, this.images.Count)];
        }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                if (this.firstName != value)
                {
                    this.firstName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                if (this.lastName != value)
                {
                    this.lastName = value;
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

        public string CountryName
        {
            get
            {
                return this.countryName;
            }

            set
            {
                if (this.countryName != value)
                {
                    this.countryName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Image
        {
            get
            {
                return this.image;
            }

            set
            {
                if (this.image != value)
                {
                    this.image = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        public int Sales
        {
            get
            {
                return this.sales;
            }
        }

        public string Region
        {
            get
            {
                return this.region;
            }
        }
    }
}

