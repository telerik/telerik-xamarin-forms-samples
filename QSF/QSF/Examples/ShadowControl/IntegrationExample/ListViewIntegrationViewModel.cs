using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace QSF.Examples.ShadowControl.IntegrationExample
{
    public class ListViewIntegrationViewModel
    {
        private static bool IsDarkTheme = Application.Current.RequestedTheme == OSAppTheme.Dark;

        public ListViewIntegrationViewModel()
        {
            this.InitCategories();
            this.InitTasks();
        }

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Task> Tasks { get; set; }

        private void InitCategories()
        {

            this.Categories = new ObservableCollection<Category>();
            this.Categories.Add(new Category()
            {
                Icon = char.ConvertFromUtf32(0xe872),
                Color = IsDarkTheme ? "#42A5F5" : "#0E88F2",
            });

            this.Categories.Add(new Category()
            {
                Icon = char.ConvertFromUtf32(0xe861),
                Color = IsDarkTheme ? "#FF6E6E" : "#F85446",
            });

            this.Categories.Add(new Category()
            {
                Icon = char.ConvertFromUtf32(0xe862),
                Color = IsDarkTheme ? "#66BB6A" : "#56AF51",
            });

            this.Categories.Add(new Category()
            {
                Icon = char.ConvertFromUtf32(0xe863),
                Color = IsDarkTheme ? "#FFA726" : "#FFAC3E",
            });
        }

        private void InitTasks()
        {
            this.Tasks = new ObservableCollection<Task>();
            this.Tasks.Add(new Task()
            {
                Title = "Robin Sharma",
                Subtitle = "The Last 6h [The Circle of Legends]",
                Date = "11:04",
                Color = IsDarkTheme ? "#FFA726" : "#FFAC3E",
            });

            this.Tasks.Add(new Task()
            {
                Title = "Charlie Thompson",
                Subtitle = "Get inspired by these top creatives",
                Date = "20 Jun",
                Color = "#FF7043",
            });

            this.Tasks.Add(new Task()
            {
                Title = "Morgan Cook",
                Subtitle = "Optimizing Xamarin Apps & Libraries",
                Date = "20 Jun",
                Color = IsDarkTheme ? "#66BB6A" : "#56AF51",
            });

            this.Tasks.Add(new Task()
            {
                Title = "Hank Baldwin",
                Subtitle = "Request Time Off - Successfully Completed",
                Date = "19 Jun",
                Color = IsDarkTheme ? "#42A5F5" : "#0E88F2",
            });
        }
    }
}
