using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ErpApp.Pages.Orders
{
    public partial class OrderEditView : ContentView
    {
        public OrderEditView()
        {
            InitializeComponent();

            this.tabView.PropertyChanged += TabView_PropertyChanged;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName != IsVisibleProperty.PropertyName)
                return;

            // Remove client tab if we are not creating an order
            if (IsVisible && (this.BindingContext as ViewModels.OrderDetailViewModel).Mode != Models.PresentationMode.Create)
            {
                this.clientTab.BindingContext = null;
                this.tabView.Items.Remove(this.clientTab);
            }
            if (!IsVisible && !this.tabView.Items.Contains(this.clientTab))
            {
                this.tabView.Items.Insert(0, this.clientTab);
            }

            this.overviewTab.BindingContext = null;

            // Ensure the first tab is the selected one
            this.tabView.SelectedItem = this.tabView.Items[0];

            // Update tab index
            int tabIndex = 1;
            foreach (var item in this.tabView.Items)
            {
                item.Header.TabIndex = tabIndex;
                tabIndex++;
            }
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            int tabCount = this.tabView.Items.Count;
            int selectedIndex = this.tabView.Items.IndexOf((Telerik.XamarinForms.Primitives.TabViewItem)this.tabView.SelectedItem);
            int nextIndex = selectedIndex + 1;

            if (nextIndex < tabCount)
            {
                this.tabView.SelectedItem = this.tabView.Items[nextIndex];
            }
        }

        private void TabView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != Telerik.XamarinForms.Primitives.RadTabView.SelectedItemProperty.PropertyName)
                return;

            int tabCount = this.tabView.Items.Count;
            int selectedIndex = this.tabView.Items.IndexOf((Telerik.XamarinForms.Primitives.TabViewItem) this.tabView.SelectedItem);
            if (selectedIndex == tabCount - 1)
            {
                this.nextButton.Text = "Add Order";
                if (this.nextButton.Triggers.Count == 0)
                {
                    DataTrigger trigger = new DataTrigger(typeof(Button));
                    trigger.Binding = new Binding("Mode");
                    trigger.Value = Models.PresentationMode.Edit;
                    trigger.Setters.Add(Button.TextProperty, "Edit Order");
                    this.nextButton.Triggers.Add(trigger);
                }
                this.nextButton.SetBinding(Button.CommandProperty, new Binding("CommitCommand"));
            }
            else
            {
                this.nextButton.Text = "Next";
                this.nextButton.RemoveBinding(Button.CommandProperty);
                this.nextButton.Command = null;
                this.nextButton.Triggers.Clear();
            }

            this.overviewTab.BindingContext = this.tabView.SelectedItem == this.overviewTab ? this.BindingContext : null;
        }
    }
}
