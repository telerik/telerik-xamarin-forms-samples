using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tagit.Common;

namespace tagit.Models
{
    public class ImageCreationInformation : ObservableBase
    {
        private string _detail;
        public string Detail
        {
            get { return this._detail; }
            set { this.SetProperty(ref this._detail, value); }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return this._startDate; }
            set { this.SetProperty(ref this._startDate, value); }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return this._endDate; }
            set { this.SetProperty(ref this._endDate, value); }
        }
    }
}
