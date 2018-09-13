using QSF.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.XamarinForms.DataControls.TreeView.Commands;
using Xamarin.Forms;

namespace QSF.Examples.TreeViewControl.LoadOnDemandExample
{
    public class ExampleViewModel : BindableBase
    {
        readonly string dataSourceFileName = "LoadOnDemandDataSource.xml";
        List<FileViewModel> source;
        public List<FileViewModel> Source
        {
            get
            {
                return this.source;
            }
            set
            {
                if (this.source != value)
                {
                    this.source = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand LoadOnDemandCommand
        {
            get;set;
        }

        public ExampleViewModel()
        {
            this.LoadOnDemandCommand = new Command(async (p) => await this.LoadOnDemandExecute(p), (p) => this.IsLoadOnDemandEnabled(p));

            this.GetSource().ContinueWith((task) =>
            {
                this.Source = task.Result;
            });
        }

        async private Task<List<FileViewModel>> GetSource()
        {
            return await ExampleDataProvider.GetRootItems(this.dataSourceFileName);
        }

        private bool IsLoadOnDemandEnabled(object p)
        {
            var context = (TreeViewLoadOnDemandCommandContext)p;
            var folder = context.Item as FolderViewModel;
            if (folder != null)
            {
                return folder.HasUnrealizedChildren;
            }
            return false;
        }

        async private Task LoadOnDemandExecute(object p)
        {
            var context = (TreeViewLoadOnDemandCommandContext)p;
            var folder = context.Item as FolderViewModel;
            if (folder != null)
            {
                List<FileViewModel> children = await Task.Run(() => this.LoadChildren(folder));
                folder.Children = children;
                context.Finish();
            }
        }

        private List<FileViewModel> LoadChildren(FolderViewModel folder)
        {
            Task.Delay(1000).Wait();
            List<FileViewModel> result = ExampleDataProvider.GetChildren(this.dataSourceFileName, folder.Name, folder.Path);
            return result;
        }
    }
}