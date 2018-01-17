using System;
using System.Reflection;
using Telerik.XamarinForms.Input;
using Telerik.XamarinForms.Input.DataForm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.DataFormControl.ReservationsExample
{
    public partial class DataFormView : ContentPage
    {
        private DataFormViewModel viewModel;

        public DataFormView()
        {
            this.InitializeComponent();

            this.dataForm.PropertyDataSourceProvider = new UserPropertyDataSourceProvider();

            foreach (var property in typeof(Reservation).GetTypeInfo().DeclaredProperties)
            {
                this.dataForm.RegisterEditor(property.Name, EditorType.Custom);
            }

            this.dataForm.GroupHeaderStyle = new DataFormGroupHeaderStyle { IsCollapsible = false };
            this.dataForm.GroupLayoutDefinition = new DataFormGroupGridLayoutDefinition();

            var style = new DataFormEditorStyle
            {
                FeedbackFontSize = 10,
                PositiveFeedbackBackground = Color.Transparent,
            };

            if (Device.RuntimePlatform == Device.Android)
            {
                style.HeaderFontSize = 12;
            }

            this.dataForm.EditorStyle = style;

            var cancelEditGesture = new TapGestureRecognizer();
            cancelEditGesture.Tapped += this.CancelButton_Clicked;
            this.cancelButton.GestureRecognizers.Add(cancelEditGesture);

            var addReservationGesture = new TapGestureRecognizer();
            addReservationGesture.Tapped += this.DoneButton_Clicked;
            this.doneButton.GestureRecognizers.Add(addReservationGesture);

            var cancelReservationGesture = new TapGestureRecognizer();
            cancelReservationGesture.Tapped += CancelReservationClicked;
            this.cancelReservationButton.GestureRecognizers.Add(cancelReservationGesture);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.viewModel = (DataFormViewModel)this.BindingContext;
        }

        private void CancelReservationClicked(object sender, EventArgs e)
        {
            this.viewModel.CancelReservation();
        }

        private void DoneButton_Clicked(object sender, EventArgs e)
        {
            this.dataForm.FormValidationCompleted += DataForm_FormValidationCompleted;
            this.dataForm.ValidateAll();
        }

        private void DataForm_FormValidationCompleted(object sender, FormValidationCompletedEventArgs e)
        {
            var form = sender as RadDataForm;
            if (form != null)
            {
                form.FormValidationCompleted -= this.DataForm_FormValidationCompleted;
                if (e.IsValid)
                {
                    this.dataForm.CommitAll();
                    this.viewModel.CommitReservation();
                }
            }
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            this.viewModel.CancelEditing();
        }
    }
}
