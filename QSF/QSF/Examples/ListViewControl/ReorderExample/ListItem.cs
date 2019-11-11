using QSF.ViewModels;

namespace QSF.Examples.ListViewControl.ReorderExample
{
    public class ListItem : BindableBase
    {
        private string header;
        private bool isDone;

        public string Header
        {
            get
            {
                return this.header;
            }
            set
            {
                if (this.header != value)
                {
                    this.header = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsDone
        {
            get
            {
                return this.isDone;
            }
            set
            {
                if (this.isDone != value)
                {
                    this.isDone = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ListItem(string header, bool isDone = false)
        {
            this.header = header;
            this.isDone = isDone;
        }
    }
}
