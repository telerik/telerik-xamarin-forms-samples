using FreshMvvm;
using System.Collections.Generic;
using System.Windows.Input;
using TodoApp.Models;
using Xamarin.Forms;

namespace TodoApp.PageModels
{
    public class WelcomePageModel : FreshBasePageModel
    {
        public WelcomePageModel()
        {
            ContinueCommand = new Command(OnContinue);
            Logo = GetImage("TodoApp.Assets.Logo.png");
            Slides = new List<WelcomeModel>()
            {
                new WelcomeModel(GetImage("TodoApp.Assets.Slide1.png"), "Remember", "Let remember the tasks for you!"),
                new WelcomeModel(GetImage("TodoApp.Assets.Slide2.png"), "Make a todo list!", "Start getting tasks into your todo list!"),
                new WelcomeModel(GetImage("TodoApp.Assets.Slide3.png"), "Enjoy!", "View, edit and manage tasks on the go!"),
            };
        }

        private static ImageSource GetImage(string image)
        {
            return ImageSource.FromResource(image, typeof(WelcomePageModel).Assembly);
        }

        private void OnContinue()
        {
            CoreMethods.PopToRoot(false);
        }

        public ICommand ContinueCommand { get; }
        public List<WelcomeModel> Slides { get; }
        public ImageSource Logo { get; }
    }
}
