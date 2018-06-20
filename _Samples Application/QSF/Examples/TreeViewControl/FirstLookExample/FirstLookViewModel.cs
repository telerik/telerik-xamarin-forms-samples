using System;
using QSF.Examples.TreeViewControl.Common;
using QSF.Services;
using QSF.ViewModels;
using Telerik.XamarinForms.DataControls.TreeView.Commands;
using Xamarin.Forms;

namespace QSF.Examples.TreeViewControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private const string OpenFolderIcon = "\uE829";
        private const string ClosedFolderIcon = "\uE82A";

        private AccountViewModel account;
        private FolderViewModel folder;

        public AccountViewModel Account
        {
            get
            {
                return this.account;
            }
            private set
            {
                if (this.account != value)
                {
                    this.account = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public FolderViewModel Folder
        {
            get
            {
                return this.folder;
            }
            private set
            {
                if (this.folder != value)
                {
                    this.folder = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Command<TreeViewItemCommandContext> NavigateCommand { get; private set; }
        public Command<TreeViewItemCommandContext> ActivateCommand { get; private set; }
        public Command<TreeViewItemCommandContext> ExpandCommand { get; private set; }
        public Command<TreeViewItemCommandContext> CollapseCommand { get; private set; }
        public Command<FolderViewModel> RemoveCommand { get; private set; }

        public FirstLookViewModel()
        {
            this.Account = DataProvider.GetData<AccountViewModel>(ResourcePaths.AccountPath);
            this.NavigateCommand = new Command<TreeViewItemCommandContext>(this.OnNavigateCommand);
            this.ActivateCommand = new Command<TreeViewItemCommandContext>(this.OnActivateCommand);
            this.ExpandCommand = new Command<TreeViewItemCommandContext>(this.OnExpandCommand);
            this.CollapseCommand = new Command<TreeViewItemCommandContext>(this.OnCollapseCommand);
            this.RemoveCommand = new Command<FolderViewModel>(this.OnRemoveCommand);
        }

        private void OnNavigateCommand(TreeViewItemCommandContext context)
        {
            var folder = (FolderViewModel)context.Item;

            if (folder.Emails.Count > 0)
            {
                var navigationService = DependencyService.Get<INavigationService>();

                navigationService.NavigateToAsync<DetailsViewModel>(folder);
            }
        }

        private void OnActivateCommand(TreeViewItemCommandContext context)
        {
            if (this.Folder != null)
            {
                this.Folder.IsActive = false;
            }

            var folder = (FolderViewModel)context.Item;

            if (this.Folder != folder && folder.Parent != null)
            {
                this.Folder = folder;
                this.Folder.IsActive = true;
            }
            else
            {
                this.Folder = null;
            }
        }

        private void OnExpandCommand(TreeViewItemCommandContext context)
        {
            var folder = (FolderViewModel)context.Item;

            folder.Icon = OpenFolderIcon;
        }

        private void OnCollapseCommand(TreeViewItemCommandContext context)
        {
            var folder = (FolderViewModel)context.Item;

            folder.Icon = ClosedFolderIcon;
        }

        private void OnRemoveCommand(FolderViewModel folder)
        {
            var parent = folder.Parent;

            if (parent != null)
            {
                parent.Folders.Remove(folder);
            }
        }
    }
}
