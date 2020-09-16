using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using QSF.Services;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.RichTextEditorControl.ImportExportExample
{
    public class ImportExportViewModel : ExampleViewModel, IFileSharingContext
    {
        private string htmlText;
        private string filePath;
        private bool isBusy;
        private const string defaultRecentFilesName = "RichTextEditor_Overview";
        private readonly IResourceService resourceService;

        public ImportExportViewModel()
        {
            this.resourceService = DependencyService.Get<IResourceService>();
            this.HtmlText = this.GetDefaultContent();
            this.SaveDefaultRecentFiles();
            this.OpenCommand = new Command<IRichTextContext>(this.ExecuteOpenCommand);
            this.OpenRecentCommand = new Command<IRichTextContext>(this.ExecuteOpenRecentCommand);
            this.SaveCommand = new Command<IRichTextContext>(this.ExecuteSaveCommand);
            this.SaveAsCommand = new Command<IRichTextContext>(this.ExecuteSaveAsCommand);
            this.ShareCommand = new Command<IRichTextContext>(this.ExecuteShareCommand);

            this.OpenItems = new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel
                {
                    Text = "Open",
                    Command = this.OpenCommand
                },
                new MenuItemViewModel
                {
                    Text = "Open Recent",
                    Command = this.OpenRecentCommand
                }
            };

            this.SaveItems = new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel
                {
                    Text = "Save",
                    Command = this.SaveCommand
                },
                new MenuItemViewModel
                {
                    Text = "Save As",
                    Command = this.SaveAsCommand
                }
            };
        }

        public string HtmlText
        {
            get
            {
                return this.htmlText;
            }
            set
            {
                if (this.htmlText != value)
                {
                    this.htmlText = value;
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
                }
            }
        }

        public ObservableCollection<MenuItemViewModel> OpenItems { get; }
        public ObservableCollection<MenuItemViewModel> SaveItems { get; }

        public Command OpenCommand { get; }
        public Command OpenRecentCommand { get; }
        public Command SaveCommand { get; }
        public Command SaveAsCommand { get; }
        public Command ShareCommand { get; }

        private string GetDefaultContent()
        {
            var resourceService = DependencyService.Get<IResourceService>();

            using (var resourceStream = resourceService.GetResourceStream(FileSharingConstants.DefaultResourceName))
            using (var streamReader = new StreamReader(resourceStream))
            {
                return streamReader.ReadToEnd();
            }
        }

        private async void ExecuteOpenCommand(IRichTextContext richTextContext)
        {
            await this.OpenDocumentAsync();
        }

        private async void ExecuteOpenRecentCommand(IRichTextContext richTextContext)
        {
            await this.NavigateToOpenAsync();
        }

        private async void ExecuteSaveCommand(IRichTextContext richTextContext)
        {
            this.HtmlText = await richTextContext.GetHtmlAsync();

            if (string.IsNullOrEmpty(this.FilePath))
            {
                await this.NavigateToSaveAsync();
            }
            else
            {
                await this.SaveDocumentAsync(this.HtmlText, this.FilePath);
            }
        }

        private async void ExecuteSaveAsCommand(IRichTextContext richTextContext)
        {
            this.HtmlText = await richTextContext.GetHtmlAsync();

            await this.NavigateToSaveAsync();
        }

        private async void ExecuteShareCommand(IRichTextContext richTextContext)
        {
            this.HtmlText = await richTextContext.GetHtmlAsync();

            await this.ShareDocumentAsync(this.HtmlText, this.FilePath);
        }

        private Task NavigateToOpenAsync()
        {
            var navigationService = DependencyService.Get<INavigationService>();

            return navigationService.NavigateToAsync<FileOpenViewModel>(this);
        }

        private Task NavigateToSaveAsync()
        {
            var navigationService = DependencyService.Get<INavigationService>();

            return navigationService.NavigateToAsync<FileSaveViewModel>(this);
        }

        private async Task<bool> OpenDocumentAsync()
        {
            var filePickerService = DependencyService.Get<IFilePickerService>();

            using (var filePickerEntry = await filePickerService.PickFileAsync())
            {
                if (filePickerEntry == null)
                {
                    return false;
                }

                var documentService = DependencyService.Get<IDocumentService>();

                this.IsBusy = true;

                try
                {
                    var fileName = filePickerEntry.FileName;
                    var documentType = documentService.GetDocumentType(fileName);

                    using (var fileStream = filePickerEntry.OpenFile())
                    {
                        this.HtmlText = await documentService.OpenDocumentAsync(fileStream, documentType);
                    }

                    this.FilePath = null;
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
        }

        private async Task<bool> SaveDocumentAsync(string htmlText, string filePath)
        {
            var documentService = DependencyService.Get<IDocumentService>();

            this.IsBusy = true;

            try
            {
                await documentService.SaveDocumentAsync(htmlText, filePath);
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

        private async Task ShareDocumentAsync(string htmlText, string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = this.GetDefaultFilePath();
            }

            if (await this.SaveDocumentAsync(htmlText, filePath))
            {
                var fileShareService = DependencyService.Get<IFileShareService>();

                await fileShareService.ShareFileAsync(filePath);
            }
        }

        private string GetDefaultFilePath()
        {
            var fileSystemService = DependencyService.Get<IFileSystemService>();
            var basePath = fileSystemService.GetLocalFolder();
            var rootPath = Path.Combine(basePath, FileSharingConstants.SharedFolderName);

            return Path.Combine(rootPath, FileSharingConstants.SharedDocumentName);
        }

        private Task ShowMessageAsync(string captionText, string messageText)
        {
            var messageService = DependencyService.Get<IMessageService>();

            return messageService.ShowMessage(captionText, messageText);
        }

        private void SaveDefaultRecentFiles()
        {
            var fileSystemService = DependencyService.Get<IFileSystemService>();
            var basePath = fileSystemService.GetLocalFolder();
            var rootPath = Path.Combine(basePath, FileSharingConstants.RecentFolderName);

            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            var fileNames = new List<string>()
                {
                    defaultRecentFilesName + ".html",
                    defaultRecentFilesName + ".rtf",
                    defaultRecentFilesName + ".docx",
                    defaultRecentFilesName + ".txt"
                };

            foreach (var fileName in fileNames)
            {
                var documentService = DependencyService.Get<IDocumentService>();
                var filePath = Path.Combine(rootPath, fileName);
                if (!File.Exists(filePath))
                {
                    using (var contentStream = this.resourceService.GetResourceStream(fileName))
                    {
                        using (var outputStream = File.OpenWrite(filePath))
                        {
                            contentStream.CopyTo(outputStream);
                        }
                    }
                }
            }
        }

    }
}
