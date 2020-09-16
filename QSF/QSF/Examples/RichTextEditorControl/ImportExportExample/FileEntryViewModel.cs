using QSF.ViewModels;

namespace QSF.Examples.RichTextEditorControl.ImportExportExample
{
    public class FileEntryViewModel : ViewModelBase
    {
        private string fileName;
        private string filePath;
        private bool isSelected;

        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                if (this.fileName != value)
                {
                    this.fileName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string FilePath
        {
            get
            {
                return this.filePath;
            }
            set
            {
                if (this.filePath != value)
                {
                    this.filePath = value;
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
