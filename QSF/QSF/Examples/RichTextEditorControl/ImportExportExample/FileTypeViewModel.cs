using QSF.Services;
using QSF.ViewModels;

namespace QSF.Examples.RichTextEditorControl.ImportExportExample
{
    public class FileTypeViewModel : ViewModelBase
    {
        private string displayName;
        private string fileExtension;
        private DocumentType documentType;

        public string DisplayName
        {
            get
            {
                return this.displayName;
            }
            set
            {
                if (this.displayName != value)
                {
                    this.displayName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string FileExtension
        {
            get
            {
                return this.fileExtension;
            }
            set
            {
                if (this.fileExtension != value)
                {
                    this.fileExtension = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public DocumentType DocumentType
        {
            get
            {
                return this.documentType;
            }
            set
            {
                if (this.documentType != value)
                {
                    this.documentType = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
