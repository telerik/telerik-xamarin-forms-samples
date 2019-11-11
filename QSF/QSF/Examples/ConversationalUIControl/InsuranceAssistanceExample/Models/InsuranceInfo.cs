using Telerik.XamarinForms.Common;

namespace QSF.Examples.ConversationalUIControl.InsuranceAssistanceExample.Models
{
    public class InsuranceInfo : NotifyPropertyChangedBase
    {
        private string title;
        private string description;
        private string totalValue;

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    this.OnPropertyChanged(nameof(this.Title));
                }
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    this.OnPropertyChanged(nameof(this.Description));
                }
            }
        }

        public string TotalValue
        {
            get
            {
                return this.totalValue;
            }
            set
            {
                if (this.totalValue != value)
                {
                    this.totalValue = value;
                    this.OnPropertyChanged(nameof(this.TotalValue));
                }
            }
        }
    }
}