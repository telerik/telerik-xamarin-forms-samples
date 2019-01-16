using QSF.ViewModels;

namespace QSF.Examples.PopupControl.ModalExample
{
    public class ContactViewModel : ViewModelBase
    {
        private string image;
        private string name;
        private bool isSelected;

        public ContactViewModel(string name, string imagePath)
        {
            this.Name = name;
            this.Image = imagePath;
        }

        public string Image
        {
            get
            {
                return this.image;
            }
            private set
            {
                if (this.image != value)
                {
                    this.image = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (this.name != value)
                {
                    this.name = value;
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
