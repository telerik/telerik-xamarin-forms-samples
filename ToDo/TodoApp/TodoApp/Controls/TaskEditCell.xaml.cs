using System.Windows.Input;
using Xamarin.Forms;

namespace TodoApp.Controls
{
    public partial class TaskEditCell : TemplatedView
    {
		public TaskEditCell()
		{
			InitializeComponent();
		}

        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon),
            typeof(string), typeof(TaskEditCell), null);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
            typeof(string), typeof(TaskEditCell), null);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command),
            typeof(ICommand), typeof(TaskEditCell), null);

        public static readonly BindableProperty ShowDisclosureIndicatorProperty = BindableProperty.Create(nameof(ShowDisclosureIndicator),
            typeof(bool), typeof(TaskEditCell), false);

        public string Icon
        {
            get => (string)this.GetValue(IconProperty);
            set => this.SetValue(IconProperty, value);
        }

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        public bool ShowDisclosureIndicator
        {
            get => (bool)this.GetValue(ShowDisclosureIndicatorProperty);
            set => this.SetValue(ShowDisclosureIndicatorProperty, value);
        }
    }
}