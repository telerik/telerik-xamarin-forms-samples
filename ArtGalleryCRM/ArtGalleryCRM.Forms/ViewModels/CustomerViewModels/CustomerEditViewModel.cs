using System.Threading.Tasks;
using System.Windows.Input;
using ArtGalleryCRM.Forms.Interfaces;
using ArtGalleryCRM.Forms.Models;
using Telerik.XamarinForms.Input.DataForm;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels.CustomerViewModels
{
    public class CustomerEditViewModel : PageViewModelBase
    {
        private bool _isReadyToSave;
        private Customer _selectedCustomer;

        public CustomerEditViewModel()
        {
            this.Title = "Edit Customer";
            this.ToolbarCommand = new Command(this.ToolbarItemTapped);
            this.FormValidationCompleteCommand = new Command(this.FormValidationCompleted);
        }

        public bool IsNewCustomer { get; set; }
        
        public Customer SelectedCustomer
        {
            get => this._selectedCustomer;
            set => SetProperty(ref this._selectedCustomer, value);
        }

        public ICommand ToolbarCommand { get; set; }

        public ICommand FormValidationCompleteCommand { get; set; }

        public IDataFormView DataFormView { get; set; }

        private async void ToolbarItemTapped(object obj)
        {
            switch (obj.ToString())
            {
                case "save":
                    // Commit the Editor's values for validation
                    this.DataFormView.CommitChanges();

                    // If values pass validation
                    if (this._isReadyToSave)
                    {
                        // Save the record
                        if (await this.UpdateDatabaseAsync())
                        {
                            await this.NavigateBackAsync();
                        }
                    }
                    break;
            }
        }

        private void FormValidationCompleted(object obj)
        {
            if (obj is FormValidationCompletedEventArgs e)
            {
                this._isReadyToSave = e.IsValid;
            }
        }

        public async Task<bool> UpdateDatabaseAsync()
        {
            await App.DisplayReadOnlyDemoAlert();
            return false;

            // ****** Update Record Logic ****** //

            //try
            //{
            //    this.IsBusy = true;

            //    if (this.IsNewCustomer)
            //    {
            //        await DependencyService.Get<IDataStore<Customer>>().AddItemAsync(this.SelectedCustomer);
            //    }
            //    else
            //    {
            //        await DependencyService.Get<IDataStore<Customer>>().UpdateItemAsync(this.SelectedCustomer);
            //    }
            //
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    MessagingCenter.Send(new MessagingCenterAlert
            //    {
            //        Title = "Error",
            //        Message = $"There was a problem updating the database. Details:\r\n\n{ex.Message}",
            //        Cancel = "OK"
            //    }, "Alert");
            //
            //    return false;
            //}
            //finally
            //{
            //    this.IsBusy = false;
            //}
        }
    }
}