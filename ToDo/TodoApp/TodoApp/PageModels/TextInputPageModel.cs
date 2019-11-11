using FreshMvvm;
using System.Windows.Input;
using Xamarin.Forms;

namespace TodoApp.PageModels
{
    public class TextInputPageModel : FreshBasePageModel
    {
        public TextInputPageModel()
        {
            _acceptCommand = new Command(OnAccept);
        }

        private Command _acceptCommand;

        public ICommand AcceptCommand => _acceptCommand;

        public string Text { get; set; }

        public override void Init(object initData)
        {
            if (initData is Model model)
            {
                this.Text = model.Text;
            }
        }

        private async void OnAccept()
        {
            await CoreMethods.PopPageModel(new Model(this.Text));
        }

        public class Model
        {
            public Model(string text)
            {
                Text = text;
            }

            public string Text { get; private set; }
        }
    }
}
