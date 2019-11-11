using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ArtGalleryCRM.Forms.Common;
using ArtGalleryCRM.Forms.Interfaces;
using ArtGalleryCRM.Forms.Models;
using ArtGalleryCRM.Forms.Services;
using ArtGalleryCRM.Forms.Views.EmployeePages;
using ArtGalleryCRM.Forms.Views.OrderPages;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels.EmployeeViewModels
{
    public class EmployeeDetailViewModel : PageViewModelBase
    {
        private int _companySalesCount;
        private int _employeeSalesCount;
        private double _companySalesRevenue;
        private double _employeeSalesRevenue;
        private Employee _selectedEmployee;

        public EmployeeDetailViewModel() { }

        public EmployeeDetailViewModel(Employee item = null)
        {
            this.Title = "Employee Detail";
            this.SelectedEmployee = item;
            this.ToolbarCommand = new Command(this.ToolbarItemTapped);
        }

        public Employee SelectedEmployee
        {
            get => this._selectedEmployee;
            set => SetProperty(ref this._selectedEmployee, value);
        }

        public ObservableCollection<ChartDataPoint> CompensationData { get; } = new ObservableCollection<ChartDataPoint>();

        public ObservableCollection<ChartDataPoint> SalesHistory { get; } = new ObservableCollection<ChartDataPoint>();

        public int CompanySalesCount
        {
            get => this._companySalesCount;
            set => SetProperty(ref this._companySalesCount, value);
        }

        public int EmployeeSalesCount
        {
            get => this._employeeSalesCount;
            set => SetProperty(ref this._employeeSalesCount, value);
        }

        public double CompanySalesRevenue
        {
            get => this._companySalesRevenue;
            set => SetProperty(ref this._companySalesRevenue, value);
        }

        public double EmployeeSalesRevenue
        {
            get => this._employeeSalesRevenue;
            set => SetProperty(ref this._employeeSalesRevenue, value);
        }

        public ICommand ToolbarCommand { get; set; }

        public IGaugesView GaugesView { get; set; }

        public override async void OnAppearing()
        {
            if (this.IsBusy || this.SelectedEmployee == null)
            {
                return;
            }

            this.IsBusy = true;

            try
            {
                // *** Compensation Calculations *** //
                this.IsBusyMessage = "calculating compensation...";

                if (this.CompensationData.Count == 0)
                {
                    this.CalculateCompensationData(SelectedEmployee.Salary);
                }

                // *** Sales Calculations ** //
                this.IsBusyMessage = "calculating sales...";

                if (this.SalesHistory.Count == 0)
                {
                    // Gets all company orders
                    var orders = await DependencyService.Get<IDataStore<Order>>().GetItemsAsync();

                    if (orders == null)
                        return;

                    this.CalculateSalesData(orders);
                }

                // With the data loading and calculations complete, trigger any UI updates via interface
                this.GaugesView.ConfigureGauges();
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = $"There was a problem calculating {SelectedEmployee.Name}'s data. Details:\r\n\n{ex.Message}",
                    Cancel = "OK"
                }, "Alert");
            }
            finally
            {
                this.IsBusyMessage = "";
                this.IsBusy = false;
            }
        }

        public void CalculateCompensationData(double salary)
        {
            var rand = new Random();

            var bonusPercentage = (double)rand.Next(10, 20) / 100;
            var benefitsPercentage = (double)rand.Next(5, 15) / 100;
            var baseSalaryPercentage = 1 - bonusPercentage - benefitsPercentage;

            if (this.CompensationData.Any())
            {
                this.CompensationData.Clear();
            }

            this.CompensationData.Add(new ChartDataPoint { Title = "Base Salary", Value = salary * baseSalaryPercentage });
            this.CompensationData.Add(new ChartDataPoint { Title = "Benefits", Value = salary * benefitsPercentage });
            this.CompensationData.Add(new ChartDataPoint { Title = "Bonus", Value = salary * bonusPercentage });
        }

        public void CalculateSalesData(IReadOnlyList<Order> orders)
        {
            // Set company values
            this.CompanySalesCount = orders.Count;
            this.CompanySalesRevenue = Math.Floor(orders.Sum(o => o.TotalPrice));

            // Get the orders associated with the employee
            var employeeSales = orders.Where(o => o.EmployeeId == this.SelectedEmployee.Id).OrderBy(o => o.OrderDate.Date).ToList();

            // Set employee values
            this.EmployeeSalesCount = employeeSales.Count;
            this.EmployeeSalesRevenue = Math.Floor(employeeSales.Sum(o => o.TotalPrice));

            // Create Sales History chart data
            foreach (var order in employeeSales)
            {
                this.SalesHistory.Add(new ChartDataPoint
                {
                    Value = order.TotalPrice,
                    Date = new DateTime(order.OrderDate.Year, order.OrderDate.Month, order.OrderDate.Day)
                });
            }
        }

        private async void ToolbarItemTapped(object obj)
        {
            if (this.SelectedEmployee == null)
            {
                return;
            }

            switch (obj.ToString())
            {
                case "order":
                    await this.NavigateForwardAsync(new OrderEditPage(this.SelectedEmployee));
                    break;
                case "edit":
                    await this.NavigateForwardAsync(new EmployeeEditPage(this.SelectedEmployee));
                    break;
                case "delete":
                    await this.DeleteEmployeeAsync();
                    break;
            }
        }

        private async Task DeleteEmployeeAsync()
        {
            await App.DisplayReadOnlyDemoAlert();
            return;

            // ****** Deletion Logic ****** //

            //if (this.SelectedEmployee == null)
            //{
            //    return;
            //}

            //var result = await DisplayAlert("Delete?", "Do you really want to delete this item?", "Yes", "No");

            //if (!result)
            //{
            //    return;
            //}

            //try
            //{
            //    this.IsBusy = true;

            //    // Do Referential Integrity Check
            //    var orders = await DependencyService.Get<IDataStore<Order>>().GetItemsAsync();
            //    var matchingOrders = orders.Where(o => o.EmployeeId == this.SelectedEmployee.Id).ToList();

            //    if (matchingOrders.Count > 0)
            //    {
            //        var deleteAll = await DisplayAlert("!!! WARNING !!!", $"There are orders in the database for {_vm?.SelectedEmployee.Name}. If you delete this employee, you'll also delete all of the associated orders.\r\n\nDo you wish to delete everything?", "Delete All", "Cancel");

            //        // Back out if user declines to delete associated orders
            //        if (!deleteAll)
            //            return;

            //        // Delete each associated Order
            //        foreach (var order in matchingOrders)
            //        {
            //            await DependencyService.Get<IDataStore<Order>>().DeleteItemAsync(order);
            //        }
            //    }

            //    // Lastly, delete the employee
            //    await DependencyService.Get<IDataStore<Employee>>().DeleteItemAsync(this.SelectedEmployee);
            //}
            //catch (Exception ex)
            //{
            //    // display error to user
            //}
            //finally
            //{
            //    this.IsBusy = false;
            //}

            //await this.NavigateBackAsync();
        }
    }
}