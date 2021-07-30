using System;
using System.Reflection;
using Telerik.XamarinForms.Input;
using Telerik.XamarinForms.Input.DataForm;
using Xamarin.Forms;

namespace QSF.Examples.DataFormControl.ReservationsExample
{
    public partial class DataFormView : ContentPage
    {
        private DataFormViewModel viewModel;

        public DataFormView()
        {
            this.InitializeComponent();

            // The PageRenderer by default sets BackgroundColor for the NavigationPage to White: https://github.com/xamarin/Xamarin.Forms/blob/ec380e4e24b213500a01992b3e10c5e8a1af3b3a/Xamarin.Forms.Platform.iOS/Renderers/PageRenderer.cs#L537
            // Because of that a Background should be set in order to make the StatusBar in iOS be visible when the mode is either dark or light.
            this.SetAppThemeColor(ContentPage.BackgroundColorProperty, (Color)App.Current.Resources["DarkBackgroundColorLight"], (Color)App.Current.Resources["DarkBackgroundColorDark"]);

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
