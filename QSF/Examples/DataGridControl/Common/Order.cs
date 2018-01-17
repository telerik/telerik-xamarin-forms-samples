using System;
using System.Xml.Serialization;
using QSF.ViewModels;

namespace QSF.Examples.DataGridControl.Common
{
    public class Order : ViewModelBase
    {
        private double orderID;
        private string shipName;
        private string customerID;
        private double employeeID;
        private string employeeImage;
        private DateTime orderDate;
        private DateTime requiredDate;
        private DateTime shippedDate;
        private double shipVia;
        private double freight;
        private string shipAddress;
        private string shipCity;
        private string shipPostalCode;
        private string shipCountry;
        private string shipRegion;

        [XmlAttribute(AttributeName = "OrderID")]
        public double OrderID
        {
            get
            {
                return this.orderID;
            }

            set
            {
                if (this.orderID != value)
                {
                    this.orderID = value;
                    this.OnPropertyChanged("OrderID");
                }
            }
        }

        [XmlAttribute(AttributeName = "ShipName")]
        public string ShipName
        {
            get
            {
                return this.shipName;
            }

            set
            {
                if (this.shipName != value)
                {
                    this.shipName = value;
                    this.OnPropertyChanged("ShipName");
                }
            }
        }

        [XmlAttribute(AttributeName = "CustomerID")]
        public string CustomerID
        {
            get
            {
                return this.customerID;
            }

            set
            {
                if (this.customerID != value)
                {
                    this.customerID = value;
                    this.OnPropertyChanged("CustomerID");
                }
            }
        }

        [XmlAttribute(AttributeName = "EmployeeID")]
        public double EmployeeID
        {
            get
            {
                return this.employeeID;
            }

            set
            {
                if (this.employeeID != value)
                {
                    this.employeeID = value;
                    this.OnPropertyChanged("EmployeeID");
                }
            }
        }

        [XmlAttribute(AttributeName = "EmployeeImage")]
        public string EmployeeImage
        {
            get
            {
                return this.employeeImage;
            }

            set
            {
                if (this.employeeImage != value)
                {
                    this.employeeImage = value;
                    this.OnPropertyChanged("EmployeeImage");
                }
            }
        }

        [XmlAttribute(AttributeName = "OrderDate")]
        public DateTime OrderDate
        {
            get
            {
                return this.orderDate;
            }

            set
            {
                if (this.orderDate != value)
                {
                    this.orderDate = value;
                    this.OnPropertyChanged("OrderDate");
                }
            }
        }

        [XmlAttribute(AttributeName = "RequiredDate")]
        public DateTime RequiredDate
        {
            get
            {
                return this.requiredDate;
            }

            set
            {
                if (this.requiredDate != value)
                {
                    this.requiredDate = value;
                    this.OnPropertyChanged("RequiredDate");
                }
            }
        }

        [XmlAttribute(AttributeName = "ShippedDate")]
        public DateTime ShippedDate
        {
            get
            {
                return this.shippedDate;
            }

            set
            {
                if (this.shippedDate != value)
                {
                    this.shippedDate = value;
                    this.OnPropertyChanged("ShippedDate");
                }
            }
        }

        [XmlAttribute(AttributeName = "ShipVia")]
        public double ShipVia
        {
            get
            {
                return this.shipVia;
            }

            set
            {
                if (this.shipVia != value)
                {
                    this.shipVia = value;
                    this.OnPropertyChanged("ShipVia");
                }
            }
        }

        [XmlAttribute(AttributeName = "Freight")]
        public double Freight
        {
            get
            {
                return this.freight;
            }

            set
            {
                if (this.freight != value)
                {
                    this.freight = value;
                    this.OnPropertyChanged("Freight");
                }
            }
        }

        [XmlAttribute(AttributeName = "ShipAddress")]
        public string ShipAddress
        {
            get
            {
                return this.shipAddress;
            }

            set
            {
                if (this.shipAddress != value)
                {
                    this.shipAddress = value;
                    this.OnPropertyChanged("ShipAddress");
                }
            }
        }

        [XmlAttribute(AttributeName = "ShipCity")]
        public string ShipCity
        {
            get
            {
                return this.shipCity;
            }

            set
            {
                if (this.shipCity != value)
                {
                    this.shipCity = value;
                    this.OnPropertyChanged("ShipCity");
                }
            }
        }

        [XmlAttribute(AttributeName = "ShipPostalCode")]
        public string ShipPostalCode
        {
            get
            {
                return this.shipPostalCode;
            }

            set
            {
                if (this.shipPostalCode != value)
                {
                    this.shipPostalCode = value;
                    this.OnPropertyChanged("ShipPostalCode");
                }
            }
        }

        [XmlAttribute(AttributeName = "ShipCountry")]
        public string ShipCountry
        {
            get
            {
                return this.shipCountry;
            }

            set
            {
                if (this.shipCountry != value)
                {
                    this.shipCountry = value;
                    this.OnPropertyChanged("ShipCountry");
                }
            }
        }

        [XmlAttribute(AttributeName = "ShipRegion")]
        public string ShipRegion
        {
            get
            {
                return this.shipRegion;
            }

            set
            {
                if (this.shipRegion != value)
                {
                    this.shipRegion = value;
                    this.OnPropertyChanged("ShipRegion");
                }
            }
        }
    }
}
