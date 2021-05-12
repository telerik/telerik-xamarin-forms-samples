using System.Collections.ObjectModel;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.ListViewControl.ReorderExample
{
    public class ReorderViewModel : ExampleViewModel
    {
        public Command<ListItem> DeleteCommand { get; private set; }
        public Command<ListItem> DoneCommand { get; private set; }
        public ObservableCollection<ListItem> Items { get; private set; }

        public ReorderViewModel()
        {
            this.Items = new ObservableCollection<ListItem>()
            {
                new ListItem("Check weather for London"),
                new ListItem("Call Brian Ingram"),
                new ListItem("Check the children’s documents"),
                new ListItem("Check if Joahn will take care of the dog"),
                new ListItem("Airplane tickets rescheduling"),
                new ListItem("Check the trains schedule"),
                new ListItem("Bills due: Alissa's ballet class"),
                new ListItem("Weekly organic basket")
            };
            this.DeleteCommand = new Command<ListItem>(this.OnDelete);
            this.DoneCommand = new Command<ListItem>(this.OnDone);
        }

        private void OnDelete(ListItem item)
        {
            this.Items.Remove(item);
        }

        private void OnDone(ListItem item)
        {
            item.IsDone = !item.IsDone;
        }
    }
}
