using System;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;

namespace QSF.Examples.PopupControl.ModalExample
{
    public partial class ModalView : ContentView
    {
        private View popupContent;
        private RadPopup popup;

        public ModalView()
        {
            this.InitializeComponent();

            MessagingCenter.Subscribe<ModalViewModel>(this, "ShowModal", this.ShowModal);
            MessagingCenter.Subscribe<ModalViewModel>(this, "CloseModal", this.CloseModal);
        }

        private void ShowModal(ModalViewModel viewModel)
        {
            Page page = null;
            Element element = this;
            while (page == null && element != null)
            {
                element = element.Parent;
                page = element as Page;
            }

            if (page != null)
            {
                if (this.popupContent == null)
                {
                    this.popupContent = (View)((ControlTemplate)this.Resources["PopupTemplate"]).CreateContent();
                    this.popupContent.BindingContextChanged += this.PopupContent_BindingContextChanged;
                }

                if (this.popup == null)
                {
                    this.popup = new RadPopup();
                    this.popup.Content = this.popupContent;
                    this.popup.IsModal = true;
                    this.popup.OutsideBackgroundColor = Color.FromHex("6F000000");
                    this.popup.Placement = PlacementMode.Center;
                    this.popup.AnimationType = PopupAnimationType.Zoom;
                }

                this.popup.IsOpen = true;
            }
        }

        private void PopupContent_BindingContextChanged(object sender, EventArgs e)
        {
        }

        private void CloseModal(ModalViewModel obj)
        {
            this.popup.IsOpen = false;
        }
    }
}