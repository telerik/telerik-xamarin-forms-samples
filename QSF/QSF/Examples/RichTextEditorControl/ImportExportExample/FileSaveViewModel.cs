using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using QSF.Services;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.RichTextEditorControl.ImportExportExample
{
    public class FileSaveViewModel : ViewModelBase
    {
        private IFileSharingContext fileSharingContext;
        private string fileName;
        private FileTypeViewModel fileType;
        private bool isBusy;

        public FileSaveViewModel()
        {
            this.FileTypes = new ObservableCollection<FileTypeViewModel>
            {
                new FileTypeViewModel
                {
                    DisplayName = "html",
                    FileExtension = ".html",
                    DocumentType = DocumentType.Html
                },
                new FileTypeViewModel
                {
                    DisplayName = "docx",
                    FileExtension = ".docx",
                    DocumentType = DocumentType.Docx
                },
                new FileTypeViewModel
                {
                    DisplayName = "rtf",
                    FileExtension = ".rtf",
                    DocumentType = DocumentType.Rtf
                },
                new FileTypeViewModel
                {
                    DisplayName = "txt",
                    FileExtension = ".txt",
                    DocumentType = DocumentType.Txt
                }
            };

            this.CancelCommand = new Command(this.ExecuteCancelCommand, this.CanExecuteCancelCommand);
            this.SaveCommand = new Command(this.ExecuteSaveCommand, this.CanExecuteSaveCommand);
        }

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
                    this.UpdateSaveCommand();
                }
            }
        }

        public FileTypeViewModel FileType
        {
            get
            {
                return this.fileType;
            }
            set
            {
                if (this.fileType != value)
                {
                    this.fileType = value;
                    this.OnPropertyChanged();
                    this.UpdateSaveCommand();
                }
            }
        }

        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
            private set
            {
                if (this.isBusy != value)
                {
                    this.isBusy = value;
                    this.OnPropertyChanged();
                    this.UpdateCancelCommand();
                    this.UpdateSaveCommand();
                }
            }
        }

        public ObservableCollection<FileTypeViewModel> FileTypes { get; }

        public Command CancelCommand { get; }
        public Command SaveCommand { get; }

        internal override Task InitializeAsync(object parameter)
        {
            this.fileSharingContext = (IFileSharingContext)parameter;

            this.UpdateDefaultValues();

            return base.InitializeAsync(parameter);
        }

        private void UpdateDefaultValues()
        {
            var filePath = this.fileSharingContext.FilePath;

            if (string.IsNullOrEmpty(filePath))
            {
                this.FileName = "RichTextEditor Overview";
                this.FileType = this.FileTypes.FirstOrDefault(viewModel =>
                viewModel.FileExtension.Equals(".html",
                    StringComparison.OrdinalIgnoreCase));
                return;
            }

            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var fileExtension = Path.GetExtension(filePath);
            var fileType = this.FileTypes.FirstOrDefault(viewModel =>
                viewModel.FileExtension.Equals(fileExtension,
                    StringComparison.OrdinalIgnoreCase));

            this.FileName = fileName;
            this.FileType = fileType;
        }

        private void UpdateCancelCommand()
        {
            this.CancelCommand.ChangeCanExecute();
        }

        private void UpdateSaveCommand()
        {
            this.SaveCommand.ChangeCanExecute();
        }

        private bool CanExecuteCancelCommand()
        {
            return !this.IsBusy;
        }

        private async void ExecuteCancelCommand()
        {
            await this.NavigateBackAsync();
        }

        private bool CanExecuteSaveCommand()
        {
            return !this.IsBusy && this.FileType != null && !string.IsNullOrEmpty(this.FileName);
        }

        private async void ExecuteSaveCommand()
        {
            var filePath = this.GetDocumentFilePath();

            if (await this.SaveDocumentAsync(filePath))
            {
                await this.NavigateBackAsync();
            }
        }

        private Task NavigateBackAsync()
        {
            var navigationService = DependencyService.Get<INavigationService>();

            return navigationService.NavigateBackAsync();
        }

        private string GetDocumentFilePath()
        {
            var fileName = this.FileName;
            var fileType = this.FileType;
            var fileExtension = fileType.FileExtension;

            if (!fileName.EndsWith(fileExtension, StringComparison.OrdinalIgnoreCase))
            {
                fileName += fileExtension;
            }

            var fileSystemService = DependencyService.Get<IFileSystemService>();
            var basePath = fileSystemService.GetLocalFolder();
            var rootPath = Path.Combine(basePath, FileSharingConstants.RecentFolderName);

            return Path.Combine(rootPath, fileName);
        }

        private async Task<bool> SaveDocumentAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                var replaceConfirmed = await this.ConfirmReplaceAsync(filePath);

                if (!replaceConfirmed)
                {
                    return false;
                }
            }

            var documentService = DependencyService.Get<IDocumentService>();
            var htmlText = this.fileSharingContext.HtmlText;

            this.IsBusy = true;

            try
            {
                await documentService.SaveDocumentAsync(htmlText, filePath);

                this.fileSharingContext.FilePath = filePath;
            }
            catch (Exception exception)
            {
                var errorCaption = "Error Saving File";
                var errorMessage = exception.Message;

                await this.ShowMessageAsync(errorCaption, errorMessage);

                return false;
            }
            finally
            {
                this.IsBusy = false;
            }

            return true;
        }

        private Task<bool> ConfirmReplaceAsync(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var replaceCaption = "Confirm File Replace";
            var replaceMessage = $"The file \"{fileName}\" already exists. Do you want to replace it? " +
                                 $"Replacing the file will overwrite its current contents.";

            return this.ShowQuestionAsync(replaceCaption, replaceMessage, "Replace", "Cancel");
        }

        private Task ShowMessageAsync(string captionText, string messageText)
        {
            var messageService = DependencyService.Get<IMessageService>();

            return messageService.ShowMessage(captionText, messageText);
        }

        private Task<bool> ShowQuestionAsync(string captionText, string messageText, string acceptText, string cancelText)
        {
            var messageService = DependencyService.Get<IMessageService>();

            return messageService.ShowQuestion(captionText, messageText, acceptText, cancelText);
        }
    }
}
