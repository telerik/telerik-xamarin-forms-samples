using System;
using System.Windows.Input;
using TodoApp.Models;
using Xamarin.Forms;

namespace TodoApp.Controls
{
	public partial class TodoItemPresenter : TemplatedView
    {
        private static readonly BindablePropertyKey IsExpandedPropertyKey = BindableProperty.CreateReadOnly(nameof(IsExpanded),
            typeof(bool), typeof(TodoItemPresenter), false);

        public static readonly BindableProperty ItemProperty = BindableProperty.Create(nameof(Item),
            typeof(TodoItem), typeof(TodoItemPresenter));

        public static readonly BindableProperty EditItemCommandProperty = BindableProperty.Create(nameof(EditItemCommand),
            typeof(ICommand), typeof(TodoItemPresenter), null);

        public static readonly BindableProperty IsExpandedProperty = IsExpandedPropertyKey.BindableProperty;

        public TodoItemPresenter()
        {
            InitializeComponent();
        }

        public TodoItem Item
        {
            get { return (TodoItem)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        public bool IsExpanded => (bool)GetValue(IsExpandedProperty);

        public ICommand EditItemCommand
        {
            get { return (ICommand)GetValue(EditItemCommandProperty); }
            set { SetValue(EditItemCommandProperty, value); }
        }

        private void OnLabelTapped(object sender, EventArgs e)
        {
            SetValue(IsExpandedPropertyKey, !this.IsExpanded);
        }
    }
}