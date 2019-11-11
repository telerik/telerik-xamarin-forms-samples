using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ErpApp.Pages.Customers
{
    public partial class CustomerDetailView : ContentView, IPopupHost
    {
        public CustomerDetailView()
        {
            InitializeComponent();

            if (Device.Idiom == TargetIdiom.Phone)
            {
                this.title.IsVisible = false;
                grid.ColumnDefinitions.RemoveAt(2);
            }
            else
            {
                Telerik.XamarinForms.Primitives.RadPopup.SetPopup(this, null);
                this.popup = null;
            }
        }

        public void OpenPopup()
        {
            if (Device.Idiom != TargetIdiom.Phone)
                return;

            this.popup.IsOpen = true;
        }

        public void ClosePopup()
        {
            if (Device.Idiom != TargetIdiom.Phone)
                return;

            this.popup.IsOpen = false;
        }
    }
}
