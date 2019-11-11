using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ArtGalleryCRM.Forms.Common;
using ArtGalleryCRM.Forms.Interfaces;
using ArtGalleryCRM.Forms.Models;
using ArtGalleryCRM.Forms.Services;
using ArtGalleryCRM.Forms.Views.OrderPages;
using CommonHelpers.Extensions;
using Telerik.Documents.SpreadsheetStreaming;
using Telerik.XamarinForms.Common.Data;
using Telerik.XamarinForms.DataGrid;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.ViewModels.OrderViewModels
{
    public class OrdersViewModel : PageViewModelBase
    {
        private bool _isPopupOpen;

        public OrdersViewModel()
        {
            this.Title = "Orders";
            this.ToolbarCommand = new Command(this.ToolbarItemTapped);
            this.SelectionChangedCommand = new Command(this.SelectionChanged);
            this.TogglePopupCommand = new Command(() => IsPopupOpen = !IsPopupOpen);
            this.SpreadsheetExportCommand = new Command(this.Export);
        }

        public ObservableCollection<Order> Orders { get; } = new ObservableCollection<Order>();

        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        public ICommand ToolbarCommand { get; set; }

        public ICommand SelectionChangedCommand { get; set; }

        public ICommand TogglePopupCommand { get; set; }

        public ICommand SpreadsheetExportCommand { get; set; }

        public IDataGridView DataGridView { get; set; }

        public override async void OnAppearing()
        {
            await this.LoadOrdersAsync();
        }

        private async Task LoadOrdersAsync()
        {
            if (this.IsBusy)
            {
                return;
            }

            try
            {
                if (this.Orders.Count == 0)
                {
                    this.IsBusy = true;
                    this.IsBusyMessage = "loading orders...";

                    var orders = await DependencyService.Get<IDataStore<Order>>().GetItemsAsync();

                    foreach (var order in orders)
                    {
                        this.Orders.Add(order);
                    }
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = $"There was a problem loading orders, check network connection and try again. Details: \r\n\n{ex.Message}",
                    Cancel = "OK"
                }, "Alert");
            }
            finally
            {
                this.DataGridView.ClearGroupDescriptors();
                this.DataGridView.SetGroupDescriptor(new PropertyGroupDescriptor { PropertyName = "DeliveryService" });

                this.IsBusyMessage = "";
                this.IsBusy = false;
            }
        }

        private async void ToolbarItemTapped(object obj)
        {
            switch (obj.ToString())
            {
                case "sync":
                    this.Orders.Clear();
                    await this.LoadOrdersAsync();
                    break;
                case "add":
                    await this.NavigateForwardAsync(new OrderEditPage());
                    break;
            }
        }

        private async void SelectionChanged(object obj)
        {
            if (obj is DataGridSelectionChangedEventArgs e)
            {
                if (e.AddedItems != null && e.AddedItems.FirstOrDefault() is Order order)
                {
                    await this.NavigateForwardAsync(new OrderDetailPage(new OrderDetailViewModel(order)));
                }
            }
        }

        private async void Export(object obj)
        {
            if (obj is SpreadDocumentFormat format)
            {
                this.IsBusy = true;
                this.IsBusyMessage = $"exporting {format}...";

                if (format == SpreadDocumentFormat.Xlsx)
                {
                    await GenerateDocumentAsync("Orders.xlsx", format);
                }

                if (format == SpreadDocumentFormat.Csv)
                {
                    await GenerateDocumentAsync("Orders.csv", format);
                }

                this.IsBusyMessage = string.Empty;
                this.IsBusy = false;
            }
        }

        private async Task GenerateDocumentAsync(string fileName, SpreadDocumentFormat exportFormat)
        {
            using (var stream = new MemoryStream())
            {
                using (var workbook = SpreadExporter.CreateWorkbookExporter(exportFormat, stream))
                {
                    using (var worksheet = workbook.CreateWorksheetExporter("First sheet"))
                    {
                        // ***** Create Columns **** //

                        using (var column = worksheet.CreateColumnExporter())
                        {
                            column.SetWidthInPixels(100);
                            column.SetWidthInCharacters(" Customer Id ".Length);
                        }

                        using (var column = worksheet.CreateColumnExporter())
                        {
                            column.SetWidthInPixels(100);
                            column.SetWidthInCharacters(" EmployeeId Id ".Length);
                        }

                        using (var column = worksheet.CreateColumnExporter())
                        {
                            column.SetWidthInPixels(100);
                            column.SetWidthInCharacters(" TotalPrice ".Length);
                        }

                        using (var column = worksheet.CreateColumnExporter())
                        {
                            column.SetWidthInPixels(100);
                            column.SetWidthInCharacters(" Quantity ".Length);
                        }

                        using (var column = worksheet.CreateColumnExporter())
                        {
                            column.SetWidthInPixels(100);
                            column.SetWidthInCharacters(" OrderDate ".Length);
                        }

                        using (var column = worksheet.CreateColumnExporter())
                        {
                            column.SetWidthInPixels(100);
                            column.SetWidthInCharacters(" DeliveryService ".Length);
                        }

                        using (var column = worksheet.CreateColumnExporter())
                        {
                            column.SetWidthInPixels(100);
                            column.SetWidthInCharacters(" Street ".Length);
                        }

                        using (var column = worksheet.CreateColumnExporter())
                        {
                            column.SetWidthInPixels(100);
                            column.SetWidthInCharacters(" City ".Length);
                        }

                        using (var column = worksheet.CreateColumnExporter())
                        {
                            column.SetWidthInPixels(100);
                            column.SetWidthInCharacters(" State ".Length);
                        }

                        using (var column = worksheet.CreateColumnExporter())
                        {
                            column.SetWidthInPixels(100);
                            column.SetWidthInCharacters(" Country ".Length);
                        }

                        using (var column = worksheet.CreateColumnExporter())
                        {
                            column.SetWidthInPixels(100);
                            column.SetWidthInCharacters(" Zip Code ".Length);
                        }

                        using (var column = worksheet.CreateColumnExporter())
                        {
                            column.SetWidthInPixels(100);
                            column.SetWidthInCharacters(" Notes ".Length);
                        }

                        using (var column = worksheet.CreateColumnExporter())
                        {
                            column.SetWidthInPixels(100);
                            column.SetWidthInCharacters(" Order ID ".Length);
                        }

                        using (var row = worksheet.CreateRowExporter())
                        {
                            row.SetHeightInPixels(40);

                            using (var cell = row.CreateCellExporter())
                            {
                                cell.SetValue("CustomerId");
                            }

                            using (var cell = row.CreateCellExporter())
                            {
                                cell.SetValue("EmployeeId");
                            }

                            using (var cell = row.CreateCellExporter())
                            {
                                cell.SetValue("ProductId");
                            }

                            using (var cell = row.CreateCellExporter())
                            {
                                cell.SetValue("TotalPrice");
                            }

                            using (var cell = row.CreateCellExporter())
                            {
                                cell.SetValue("Quantity");
                            }

                            using (var cell = row.CreateCellExporter())
                            {
                                cell.SetValue("OrderDate");
                            }

                            using (var cell = row.CreateCellExporter())
                            {
                                cell.SetValue("DeliveryService");
                            }

                            using (var cell = row.CreateCellExporter())
                            {
                                cell.SetValue("Street");
                            }

                            using (var cell = row.CreateCellExporter())
                            {
                                cell.SetValue("City");
                            }

                            using (var cell = row.CreateCellExporter())
                            {
                                cell.SetValue("State");
                            }

                            using (var cell = row.CreateCellExporter())
                            {
                                cell.SetValue("Country");
                            }

                            using (var cell = row.CreateCellExporter())
                            {
                                cell.SetValue("ZipCode");
                            }

                            using (var cell = row.CreateCellExporter())
                            {
                                cell.SetValue("Notes");
                            }

                            using (var cell = row.CreateCellExporter())
                            {
                                cell.SetValue("Order Id (KEY)");
                            }
                        }

                        // ***** Create Rows **** //

                        foreach (var order in Orders)
                        {
                            using (var row = worksheet.CreateRowExporter())
                            {
                                row.SetHeightInPixels(40);

                                using (var cell = row.CreateCellExporter())
                                {
                                    cell.SetValue(order.CustomerId);
                                }

                                using (var cell = row.CreateCellExporter())
                                {
                                    cell.SetValue(order.EmployeeId);
                                }

                                using (var cell = row.CreateCellExporter())
                                {
                                    cell.SetValue(order.ProductId);
                                }

                                using (var cell = row.CreateCellExporter())
                                {
                                    cell.SetValue(order.TotalPrice);
                                }

                                using (var cell = row.CreateCellExporter())
                                {
                                    cell.SetValue(order.Quantity);
                                }

                                using (var cell = row.CreateCellExporter())
                                {
                                    cell.SetValue(order.OrderDate);
                                }

                                using (var cell = row.CreateCellExporter())
                                {
                                    cell.SetValue(order.DeliveryService);
                                }

                                using (var cell = row.CreateCellExporter())
                                {
                                    cell.SetValue(order.Street);
                                }

                                using (var cell = row.CreateCellExporter())
                                {
                                    cell.SetValue(order.City);
                                }

                                using (var cell = row.CreateCellExporter())
                                {
                                    cell.SetValue(order.State);
                                }

                                using (var cell = row.CreateCellExporter())
                                {
                                    cell.SetValue(order.Country);
                                }

                                using (var cell = row.CreateCellExporter())
                                {
                                    cell.SetValue(order.ZipCode);
                                }

                                using (var cell = row.CreateCellExporter())
                                {
                                    cell.SetValue(order.Notes);
                                }

                                using (var cell = row.CreateCellExporter())
                                {
                                    cell.SetValue(order.Id);
                                }
                            }
                        }
                    }
                }

                var filePath = await stream.SaveToLocalFolderAsync(fileName);

                // This is required for Xamarin Essentials to use the Share feature
                ExperimentalFeatures.Enable(ExperimentalFeatures.EmailAttachments);
                ExperimentalFeatures.Enable(ExperimentalFeatures.ShareFileRequest);

                await Share.RequestAsync(new ShareFileRequest(fileName, new ShareFile(filePath)));
            }
        }
    }
}