using Xamarin.Forms;

namespace TodoApp.Models
{
    public class WelcomeModel
    {
        public WelcomeModel(ImageSource image, string title, string description)
        {
            Image = image;
            Title = title;
            Description = description;
        }

        public ImageSource Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
