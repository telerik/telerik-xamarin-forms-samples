using QSF.ViewModels;

namespace QSF.Examples.ListViewControl.SelectionExample
{
    public class ListItem : BindableBase
    {
        private string date;
        private string title;
        private string content;
        private bool isFavorite;

        public string Date
        {
            get
            {
                return this.date;
            }
            set
            {
                if (this.date != value)
                {
                    this.date = value;
                    this.OnPropertyChanged();
                }
            }
        }

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
                    this.OnPropertyChanged();
                }
            }
        }

        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                if (this.content != value)
                {
                    this.content = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsFavorite
        {
            get
            {
                return this.isFavorite;
            }
            set
            {
                if (this.isFavorite != value)
                {
                    this.isFavorite = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
