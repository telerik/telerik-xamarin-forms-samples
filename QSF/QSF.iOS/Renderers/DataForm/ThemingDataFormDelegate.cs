using Telerik.XamarinForms.InputRenderer.iOS;
using Telerik.XamarinForms.InputRenderer.iOS.DataForm;
using TelerikUI;

namespace QSF.iOS.Renderers.DataForm
{
    public class ThemingDataFormDelegate : DataFormDelegate
    {
        public ThemingDataFormDelegate(DataFormRenderer renderer) : base(renderer)
        {
        }

        public override void UpdateGroupView(TKDataForm dataForm, TKEntityPropertyGroupView groupView, uint groupIndex)
        {
            base.UpdateGroupView(dataForm, groupView, groupIndex);
            groupView.TitleView.Hidden = true;
        }
    }
}