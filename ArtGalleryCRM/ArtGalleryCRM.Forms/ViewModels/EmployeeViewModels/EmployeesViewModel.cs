using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ArtGalleryCRM.Forms.Common;
using ArtGalleryCRM.Forms.Models;
using ArtGalleryCRM.Forms.Services;
using ArtGalleryCRM.Forms.Views.EmployeePages;
using Telerik.XamarinForms.DataControls.ListView.Commands;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels.EmployeeViewModels
{
    public class EmployeesViewModel : PageViewModelBase
    {
        private IReadOnlyList<Employee> _allEmployees;
        private string _searchText = string.Empty;

        public EmployeesViewModel()
        {
            this.Title = "Employees";
            this.ItemTapCommand = new Command<ItemTapCommandContext>(this.ItemTapped);
            this.ToolbarCommand = new Command(this.ToolbarItemTapped);
        }

        public ObservableCollection<Employee> Employees { get; } = new ObservableCollection<Employee>();

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                this.ApplyFilter(value);
            }
        }

        public ICommand ItemTapCommand { get; set; }

        public ICommand ToolbarCommand { get; set; }

        public override async void OnAppearing()
        {
            await this.LoadEmployeesAsync();
        }

        private void ApplyFilter(string textToSearch)
        {
            if (this._allEmployees == null)
            {
                return;
            }

            var filteredEmployees = string.IsNullOrEmpty(textToSearch)
                ? this._allEmployees
                : this._allEmployees.Where(p => p.Name.ToLower().Contains(textToSearch.ToLower()));

            this.Employees.Clear();

            foreach (var employee in filteredEmployees)
            {
                this.Employees.Add(employee);
            }
            
        }

        private async Task LoadEmployeesAsync()
        {
            if (this.IsBusy)
                return;
            
            try
            {
                if (this.Employees.Count == 0)
                {
                    this.IsBusy = true;
                    this.IsBusyMessage = "loading employees...";

                    this._allEmployees = await DependencyService.Get<IDataStore<Employee>>().GetItemsAsync();
                    
                    foreach (var employee in this._allEmployees)
                    {
                        this.Employees.Add(employee);
                    }
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = $"There was a problem loading employees, check your network connection and try again. Details: \r\n\n{ex.Message}",
                    Cancel = "OK"
                }, "Alert");
            }
            finally
            {
                this.IsBusyMessage = "";
                this.IsBusy = false;
            }
        }

        private async void ItemTapped(ItemTapCommandContext context)
        {
            if (context.Item is Employee employee)
            {
                await this.NavigateForwardAsync(new EmployeeDetailPage(new EmployeeDetailViewModel(employee)));
            }
        }

        private async void ToolbarItemTapped(object obj)
        {
            switch (obj.ToString())
            {
                case "sync":
                    this.Employees.Clear();
                    await this.LoadEmployeesAsync();
                    break;
                case "add":
                    await this.NavigateForwardAsync(new EmployeeEditPage());
                    break;
            }
        }
    }
}