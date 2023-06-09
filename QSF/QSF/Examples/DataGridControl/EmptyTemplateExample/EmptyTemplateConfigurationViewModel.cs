using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using QSF.Examples.DataGridControl.Common;
using QSF.ViewModels;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.DataGrid;

namespace QSF.Examples.DataGridControl.EmptyTemplateExample
{
    public class EmptyTemplateConfigurationViewModel : ConfigurationViewModel
    {
        private ObservableCollection<Order> orders;
        private object itemsSource;
        private IEnumerable<EmptyContentDisplayMode> emptyContentDisplayModes;
        private EmptyContentDisplayMode emptyContentDisplayMode;
        private bool useEmptyCollectionSource;
        private bool useNonEmptyCollectionSource;
        private bool useNullSource;
        private DataGridColumnCollection columns;

        public EmptyTemplateConfigurationViewModel()
        {
            this.orders = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
            this.emptyContentDisplayModes = Enum.GetValues(typeof(EmptyContentDisplayMode))
                                                .Cast<EmptyContentDisplayMode>();
            this.emptyContentDisplayMode = this.emptyContentDisplayModes.Last();
            this.useEmptyCollectionSource = true;
        }


        public ObservableCollection<Order> Orders => this.orders;

        public DataGridColumnCollection Columns
        {
            get => this.columns;
            set
            {
                this.columns = value;

                if (this.useNullSource)
                {
                    this.SetNullItemsSource();
                }
                else if (this.useEmptyCollectionSource)
                {
                    this.SetEmptyCollectionItemsSource();
                }
                else if (this.useNonEmptyCollectionSource)
                {
                    this.SetOrdersItemsSource();
                }
            }
        }

        public object ItemsSource
        {
            get => this.itemsSource;
            set
            {
                if (this.itemsSource != value)
                {
                    this.itemsSource = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public IEnumerable<EmptyContentDisplayMode> EmptyContentDisplayModes => this.emptyContentDisplayModes;

        public EmptyContentDisplayMode EmptyContentDisplayMode
        {
            get => this.emptyContentDisplayMode;
            set
            {
                if (this.emptyContentDisplayMode != value)
                {
                    this.emptyContentDisplayMode = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool UseEmptyCollectionSource
        {
            get => this.useEmptyCollectionSource;
            set
            {
                if (this.useEmptyCollectionSource != value)
                {
                    this.useEmptyCollectionSource = value;
                    this.OnPropertyChanged();

                    if (this.useEmptyCollectionSource)
                    {
                        this.UseNonEmptyCollectionSource = false;
                        this.UseNullSource = false;

                        this.SetEmptyCollectionItemsSource();
                    }
                }
            }
        }

        public bool UseNonEmptyCollectionSource
        {
            get => this.useNonEmptyCollectionSource;
            set
            {
                if (this.useNonEmptyCollectionSource != value)
                {
                    this.useNonEmptyCollectionSource = value;
                    this.OnPropertyChanged();

                    if (this.useNonEmptyCollectionSource)
                    {
                        this.UseEmptyCollectionSource = false;
                        this.UseNullSource = false;

                        this.SetOrdersItemsSource();
                    }
                }
            }
        }

        public bool UseNullSource
        {
            get => this.useNullSource;
            set
            {
                if (this.useNullSource != value)
                {
                    this.useNullSource = value;
                    this.OnPropertyChanged();

                    if (this.useNullSource)
                    {
                        this.UseNonEmptyCollectionSource = false;
                        this.UseEmptyCollectionSource = false;

                        this.SetNullItemsSource();
                    }
                }
            }
        }

        private void InitColumns()
        {
            if (this.columns == null || this.columns.Count > 0)
            {
                return;
            }

            this.columns.Clear();
            this.columns.Add(new DataGridNumericalColumn() { PropertyName = "OrderID", HeaderText = "Order ID" });
            this.columns.Add(new DataGridNumericalColumn() { PropertyName = "OrderDate", HeaderText = "Order Date" });
            this.columns.Add(new DataGridNumericalColumn() { PropertyName = "ShippedDate", HeaderText = "Shipped Date" });
            this.columns.Add(new DataGridNumericalColumn() { PropertyName = "ShipName", HeaderText = "Ship Name" });
            this.columns.Add(new DataGridNumericalColumn() { PropertyName = "ShipCity", HeaderText = "Ship City" });
            this.columns.Add(new DataGridNumericalColumn() { PropertyName = "ShipCountry", HeaderText = "Ship Country" });
            this.columns.Add(new DataGridNumericalColumn() { PropertyName = "ShipPostalCode", HeaderText = "Ship Postal Code" });
        }

        private void SetOrdersItemsSource()
        {
            this.ItemsSource = this.Orders;
            this.InitColumns();
        }

        private void SetNullItemsSource()
        {
            this.ItemsSource = null;
            this.columns?.Clear();
        }

        private void SetEmptyCollectionItemsSource()
        {
            this.ItemsSource = new ObservableCollection<Order>();
            this.InitColumns();
        }
    }
}
