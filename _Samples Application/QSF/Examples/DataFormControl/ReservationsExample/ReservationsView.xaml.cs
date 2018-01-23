using System;
using Telerik.XamarinForms.DataControls.ListView;
using Xamarin.Forms;

namespace QSF.Examples.DataFormControl.ReservationsExample
{
    public partial class ReservationsView : ContentView
    {
        private ReservationsViewModel viewModel = new ReservationsViewModel();

        public ReservationsView()
        {
            this.InitializeComponent();
            this.BindingContext = viewModel;

            var addReservation = new TapGestureRecognizer();
            addReservation.Tapped += this.AddReservationClicked;

            this.addReservationImage.GestureRecognizers.Add(addReservation);
            this.addReservationLabel.GestureRecognizers.Add(addReservation);

            this.dateHeader.Text = DateTime.Today.ToString("ddd, dd.MM");

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    this.listView.LayoutDefinition.VerticalItemSpacing = 15;
                    this.listView.LayoutDefinition.ItemLength = 42;
                    break;
                case Device.Android:
                case Device.UWP:
                    this.listView.LayoutDefinition.VerticalItemSpacing = 20;
                    this.listView.LayoutDefinition.ItemLength = 56;
                    break;
            }
        }

        private void AddReservationClicked(object sender, EventArgs e)
        {
            this.viewModel.AddReservation();
        }

        private void ReservationTap(object sender, ItemTapEventArgs e)
        {
            this.viewModel.EditReservation((Reservation)e.Item);
        }
    }
}
