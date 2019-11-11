using QSF.Examples.DataGridControl.Common;
using System.Collections.Generic;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.DataGridControl.CRUDOperationsExample
{
    public partial class AddItemView : ContentView
    {
        public AddItemView()
        {
            this.InitializeComponent();

            this.dataForm.RegisterEditor(nameof(Order.OrderID), EditorType.IntegerEditor);
            this.dataForm.RegisterEditor(nameof(Order.ShipName), EditorType.TextEditor);
            this.dataForm.RegisterEditor(nameof(Order.OrderDate), EditorType.DateEditor);
            this.dataForm.RegisterEditor(nameof(Order.ShippedDate), EditorType.DateEditor);
            this.dataForm.RegisterEditor(nameof(Order.ShipVia), EditorType.IntegerEditor);
            this.dataForm.RegisterEditor(nameof(Order.CustomerID), EditorType.TextEditor);
            this.dataForm.RegisterEditor(nameof(Order.Freight), EditorType.DecimalEditor);

            this.dataForm.MetadataProvider = new OrderMetadataProvider(new HashSet<string>
            {
                nameof(Order.OrderID),
                nameof(Order.ShipName),
                nameof(Order.ShippedDate),
                nameof(Order.ShippedDate),
                nameof(Order.ShipVia),
                nameof(Order.CustomerID),
                nameof(Order.Freight),
            });
        }
    }
}
