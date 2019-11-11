using System;
using System.Threading.Tasks;
using Telerik.XamarinForms.DataControls.ListView;
using Xamarin.Forms;

namespace QSF.Examples.DataFormControl.ReservationsExample
{
    public partial class ReservationsView : ContentView
    {
        public ReservationsView()
        {
            this.InitializeComponent();

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
            ((ReservationsViewModel)this.BindingContext).AddReservation();
        }

        private void ReservationTap(object sender, ItemTapEventArgs e)
        {
            ((ReservationsViewModel)this.BindingContext).EditReservation((Reservation)e.Item);
        }
    }
}
