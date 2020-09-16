using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using QSF.Services;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.RichTextEditorControl.ImportExportExample
{
    public class FileOpenViewModel : ViewModelBase
    {
        private IFileSharingContext fileSharingContext;
        private FileEntryViewModel fileEntry;
        private bool isBusy;

        public FileOpenViewModel()
        {
            this.FileEntries = new ObservableCollection<FileEntryViewModel>();
            this.CancelCommand = new Command(this.ExecuteCancelCommand, this.CanExecuteCancelCommand);
            this.OpenCommand = new Command(this.ExecuteOpenCommand, this.CanExecuteOpenCommand);
        }

        public FileEntryViewModel FileEntry
        {
            get
            {
                return this.fileEntry;
            }
            set
            {
                if (this.fileEntry != value)
                {
                    this.OnSelectionChanging();
                    this.fileEntry = value;
                    this.OnSelectionChanged();
                    this.OnPropertyChanged();
                    this.UpdateOpenCommand();
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
                    this.UpdateOpenCommand();
                }
            }
        }

        public ObservableCollection<FileEntryViewModel> FileEntries { get; }

        public Command CancelCommand { get; }
        public Command OpenCommand { get; }

        internal override Task InitializeAsync(object parameter)
        {
            this.fileSharingContext = (IFileSharingContext)parameter;

            this.UpdateFileEntries();

            return base.InitializeAsync(parameter);
        }

        private void OnSelectionChanging()
        {
            if (this.fileEntry != null)
            {
                this.fileEntry.IsSelected = false;
            }
        }

        private void OnSelectionChanged()
        {
            if (this.fileEntry != null)
            {
                this.fileEntry.IsSelected = true;
            }
        }

        private void UpdateFileEntries()
        {
            this.FileEntries.Clear();

            var fileSystemService = DependencyService.Get<IFileSystemService>();
            var basePath = fileSystemService.GetLocalFolder();
            var rootPath = Path.Combine(basePath, FileSharingConstants.RecentFolderName);

            if (Directory.Exists(rootPath))
            {
                var filePaths = Directory.EnumerateFiles(rootPath);

                foreach (var filePath in filePaths)
                {
                    var fileName = Path.GetFileName(filePath);
                    var fileEntry = new FileEntryViewModel
                    {
                        FileName = fileName,
                        FilePath = filePath
                    };

                    this.FileEntries.Add(fileEntry);
                }
            }
        }

        private void UpdateCancelCommand()
        {
            this.CancelCommand.ChangeCanExecute();
        }

        private void UpdateOpenCommand()
        {
            this.OpenCommand.ChangeCanExecute();
        }

        private bool CanExecuteCancelCommand()
        {
            return !this.IsBusy;
        }

        private async void ExecuteCancelCommand()
        {
            await this.NavigateBackAsync();
        }

        private bool CanExecuteOpenCommand()
        {
            return !this.IsBusy && this.FileEntry != null;
        }

        private async void ExecuteOpenCommand()
        {
            var filePath = this.FileEntry.FilePath;

            if (await this.OpenDocumentAsync(filePath))
            {
                await this.NavigateBackAsync();
            }
        }

        private Task NavigateBackAsync()
        {
            var navigationService = DependencyService.Get<INavigationService>();

            return navigationService.NavigateBackAsync();
        }

        private async Task<bool> OpenDocumentAsync(string filePath)
        {
            var documentService = DependencyService.Get<IDocumentService>();

            this.IsBusy = true;

            try
            {
                var htmlText = await documentService.OpenDocumentAsync(filePath);

                this.fileSharingContext.HtmlText = htmlText;
                this.fileSharingContext.FilePath = filePath;
            }
            catch (Exception exception)
            {
                var errorCaption = "Error Opening File";
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

        private Task ShowMessageAsync(string captionText, string messageText)
        {
            var messageService = DependencyService.Get<IMessageService>();

            return messageService.ShowMessage(captionText, messageText);
        }
    }
}
