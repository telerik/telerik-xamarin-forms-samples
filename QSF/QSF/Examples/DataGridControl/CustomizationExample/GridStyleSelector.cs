using Telerik.XamarinForms.DataGrid;
using Xamarin.Forms;

namespace QSF.Examples.DataGridControl.CustomizationExample
{
    public class GridStyleSelector : DataGridStyleSelector
    {
        public DataGridStyle OuterGroupStyleLight { get; set; }

        public DataGridStyle OuterGroupStyleDark { get; set; }

        public DataGridStyle InnerGroupStyleLight { get; set; }

        public DataGridStyle InnerGroupStyleDark { get; set; }

        public override DataGridStyle SelectStyle(object item, BindableObject container)
        {
            var isThemeLight = Application.Current.UserAppTheme != OSAppTheme.Dark;
            if ((item as GroupHeaderContext).Level == 0)
            {
                return isThemeLight ? this.OuterGroupStyleLight : this.OuterGroupStyleDark;
            }

            return isThemeLight ? this.InnerGroupStyleLight : this.InnerGroupStyleDark;
        }

    }
}
