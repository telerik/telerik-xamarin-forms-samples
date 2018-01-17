using System.Collections.ObjectModel;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.ListViewControl.ReorderExample
{
    public class ReorderViewModel : ViewModelBase
    {
        public Command<ListItem> DeleteCommand { get; private set; }
        public Command<ListItem> DoneCommand { get; private set; }
        public ObservableCollection<ListItem> Items { get; private set; }

        public ReorderViewModel()
        {
            this.Items = new ObservableCollection<ListItem>()
            {
                new ListItem("Check weather for London"),
                new ListItem("Call Brian Ingram for the Hotel reservations"),
                new ListItem("Check the childrens' documents"),
                new ListItem("Check if Johan will take care of the dog"),
                new ListItem("Airplane tickets reschedule for the morning plane"),
                new ListItem("Check the trains schedule London - Paris"),
                new ListItem("Bills due: Alissa's ballet class free tomorrow"),
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
