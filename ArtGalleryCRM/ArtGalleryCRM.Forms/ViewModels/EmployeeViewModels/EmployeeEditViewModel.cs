using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ArtGalleryCRM.Forms.Interfaces;
using ArtGalleryCRM.Forms.Models;
using ArtGalleryCRM.Forms.Views;
using Telerik.XamarinForms.Input.DataForm;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels.EmployeeViewModels
{
    public class EmployeeEditViewModel : PageViewModelBase
    {
        private bool _isReadyToSave;
        private readonly Random _rand = new Random();
        private Employee _selectedEmployee;

        public EmployeeEditViewModel()
        {
            this.Title = "Edit Employee";
            this.SetPhotoCommand = new Command(async () => await this.ShowModalAsync(new ImageEditorPage(this.SelectedEmployee)));
            this.ToolbarCommand = new Command(this.ToolbarItemTapped);
            this.FormValidationCompleteCommand = new Command(this.FormValidationCompleted);
        }

        public bool IsNewEmployee { get; set; }
        
        public Employee SelectedEmployee
        {
            get => this._selectedEmployee;
            set => SetProperty(ref this._selectedEmployee, value);
        }

        public ICommand SetPhotoCommand { get; set; }

        public ICommand ToolbarCommand { get; set; }

        public ICommand FormValidationCompleteCommand { get; set; }

        public IDataFormView DataFormView { get; set; }

        public override void OnAppearing()
        {
            base.OnAppearing();

            if (string.IsNullOrEmpty(this.SelectedEmployee.PhotoUri))
            {
                this.SelectedEmployee.PhotoUri = "profile_photo.png";
            }
        }

        private void FormValidationCompleted(object obj)
        {
            if (obj is FormValidationCompletedEventArgs e)
            {
                this._isReadyToSave = e.IsValid;
            }
        }

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

        public async Task<bool> UpdateDatabaseAsync()
        {
            await App.DisplayReadOnlyDemoAlert();
            return false;

            // ****** Update Record Logic ****** //

            //try
            //{
            //    this.IsBusy = true;

            //    if (string.IsNullOrEmpty(this.SelectedEmployee.PhotoUri) || this.SelectedEmployee.PhotoUri == "profile_photo.png")
            //    {
            //        SetPhoto();
            //    }

            //    if (this.IsNewEmployee)
            //    {
            //        await DependencyService.Get<IDataStore<Employee>>().AddItemAsync(this.SelectedEmployee);
            //    }
            //    else
            //    {
            //        await DependencyService.Get<IDataStore<Employee>>().UpdateItemAsync(this.SelectedEmployee);
            //    }

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

            //    return false;
            //}
            //finally
            //{
            //    this.IsBusy = false;
            //}
        }
    }
}