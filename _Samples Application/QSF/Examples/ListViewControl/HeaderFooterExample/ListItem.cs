using QSF.ViewModels;

namespace QSF.Examples.ListViewControl.HeaderFooterExample
{
    public class ListItem : BindableBase
    {
        private string name;
        private string icon;
        private string company;
        private double amount;
        private bool isSelected;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Icon
        {
            get
            {
                return this.icon;
            }
            set
            {
                if (this.icon != value)
                {
                    this.icon = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Company
        {
            get
            {
                return this.company;
            }
            set
            {
                if (this.company != value)
                {
                    this.company = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double Amount
        {
            get
            {
                return this.amount;
            }
            set
            {
                if (this.amount != value)
                {
                    this.amount = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
