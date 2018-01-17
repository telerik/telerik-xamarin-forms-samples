using Telerik.XamarinForms.DataGrid;
using Xamarin.Forms;

namespace QSF.Examples.DataGridControl.CustomizationExample
{
    public class GridStyleSelector : DataGridStyleSelector
    {
        public DataGridStyle OuterGroupStyle { get; set; }

        public DataGridStyle InnerGroupStyle { get; set; }

        public override DataGridStyle SelectStyle(object item, BindableObject container)
        {
            if ((item as GroupHeaderContext).Level == 0)
            {
                return this.OuterGroupStyle;
            }
            else
            {
                return this.InnerGroupStyle;
            }
        }

    }
}
