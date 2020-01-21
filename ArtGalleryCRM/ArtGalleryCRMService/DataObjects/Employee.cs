using Microsoft.Azure.Mobile.Server;
using System;

namespace ArtGalleryCRMService.DataObjects
{
    public class Employee : EntityData
    {
        public string Name { get; set; }

        public string PhotoUri { get; set; }

        public string OfficeLocation { get; set; }

        public DateTime HireDate { get; set; }

        public double Salary { get; set; }

        public int VacationBalance { get; set; }

        public int VacationUsed { get; set; }

        public string Notes { get; set; }
    }
}