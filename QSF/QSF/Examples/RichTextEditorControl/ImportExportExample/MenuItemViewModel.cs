using System.Windows.Input;
using QSF.ViewModels;

namespace QSF.Examples.RichTextEditorControl.ImportExportExample
{
    public class MenuItemViewModel : ViewModelBase
    {
        private string text;
        private ICommand command;

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                if (this.text != value)
                {
                    this.text = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand Command
        {
            get
            {
                return this.command;
            }
            set
            {
                if (this.command != value)
                {
                    this.command = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
