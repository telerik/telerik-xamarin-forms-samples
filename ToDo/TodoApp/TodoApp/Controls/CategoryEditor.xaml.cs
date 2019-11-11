using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Telerik.XamarinForms.Input.Calendar;
using TodoApp.Models;
using Xamarin.Forms;

namespace TodoApp.Controls
{
    public partial class CategoryEditor : TemplatedView
    {
        private static readonly BindablePropertyKey IsExpandedPropertyKey = BindableProperty.CreateReadOnly(nameof(IsExpanded),
            typeof(bool), typeof(CategoryEditor), false);

        public static readonly BindableProperty ItemProperty = BindableProperty.Create(nameof(Item),
            typeof(Category), typeof(CategoryEditor));

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command),
            typeof(ICommand), typeof(CategoryEditor), null);

        public static readonly BindableProperty IsExpandedProperty = IsExpandedPropertyKey.BindableProperty;

        List<Color> availableColors = new List<Color>
        {
            Color.FromHex("#2AB6F6"),
            Color.FromHex("#EE534F"),
            Color.FromHex("#EC407A"),
            Color.FromHex("#AB47BC"),
            Color.FromHex("#7E57C2"),
            Color.FromHex("#42A5F5"),
            Color.FromHex("#26C5DA"),
            Color.FromHex("#24A79A"),
            Color.FromHex("#66BB6A"),
            Color.FromHex("#9CCC65"),
            Color.FromHex("#D4E157"),
            Color.FromHex("#FFEE58"),
            Color.FromHex("#FFCA29"),
            Color.FromHex("#FFA726"),
            Color.FromHex("#FF7043"),
            Color.FromHex("#8D6E63"),
            Color.FromHex("#BDBDBD"),
            Color.FromHex("#78909C"),
            Color.FromHex("#78009C")
        };

        public CategoryEditor()
		{
			InitializeComponent();

            var child = this.Children.First();
            var picker = child.FindByName<CalendarColorPicker>("colorPicker");
            picker.Colors = this.availableColors;
        }

        public bool IsExpanded => (bool)GetValue(IsExpandedProperty);

        public Category Item
        {
            get { return (Category)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        public ICommand Command
        {
            get => (ICommand)this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        private void OnExpandClicked(object sender, System.EventArgs e)
        {
            this.SetValue(IsExpandedPropertyKey, !IsExpanded);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.Command is Command cmd)
            {
                cmd.ChangeCanExecute();
            }
        }
    }
}