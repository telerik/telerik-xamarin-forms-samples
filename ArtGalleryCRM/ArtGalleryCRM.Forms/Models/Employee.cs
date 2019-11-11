using System;
using Telerik.XamarinForms.Common.DataAnnotations;

namespace ArtGalleryCRM.Forms.Models
{
    public class Employee : BaseDataObject
    {
        private string _name;
        private string _photoUri = "profile_photo.png";
        private string _officeLocation;
        private DateTime _hireDate = DateTime.Today;
        private double _salary;
        private int _vacationBalance;
        private int _vacationUsed;
        private string _notes;
        
        [DisplayOptions(Header = "Name", PlaceholderText = "employee name", Group = "Personal", Position = 0)]
        [NonEmptyValidator("Please enter employee name")]
        public string Name
        {
            get => this._name;
            set => SetProperty(ref this._name, value);
        }
        
        [Ignore]
        public string PhotoUri
        {
            get => this._photoUri;
            set => SetProperty(ref this._photoUri, value);
        }

        [DisplayOptions(Header = "Office Location", PlaceholderText = "employee home office", Group = "Personal", Position = 1)]
        [NonEmptyValidator("Enter Office Location")]
        public string OfficeLocation
        {
            get => this._officeLocation;
            set => SetProperty(ref this._officeLocation, value);
        }

        [DisplayOptions(Header = "Hire Date", Group = "Personal", Position = 2)]
        [NonEmptyValidator("Enter Hire Date")]
        public DateTime HireDate
        {
            get => this._hireDate;
            set => SetProperty(ref this._hireDate, value);
        }

        [DisplayOptions(Header = "Salary", Group = "Personal", Position = 3)]
        [NumericalRangeValidator(0, 10000000)]
        public double Salary
        {
            get => this._salary;
            set => SetProperty(ref this._salary, value);
        }

        [DisplayOptions(Header = "Vacation Total", Group = "PTO", Position = 0)]
        [NumericalRangeValidator(0, 360)]
        public int VacationBalance
        {
            get => this._vacationBalance;
            set => SetProperty(ref this._vacationBalance, value);
        }

        [DisplayOptions(Header = "Vacation Used", Group = "PTO", Position = 1)]
        [NumericalRangeValidator(0, 360)]
        public int VacationUsed
        {
            get => this._vacationUsed;
            set => SetProperty(ref this._vacationUsed, value);
        }

        [DisplayOptions(Header = "Notes", PlaceholderText = "employee notes", Group = "Notes", Position = 0)]
        public string Notes
        {
            get => this._notes;
            set => SetProperty(ref this._notes, value);
        }
    }
}