using System;
using Telerik.XamarinForms.InputRenderer.iOS;
using Telerik.XamarinForms.InputRenderer.iOS.DataForm;
using TelerikUI;
using UIKit;

namespace QSF.iOS.Renderers.DataForm
{
    public class DFDelegate : DataFormDelegate
    {
        public DFDelegate(DataFormRenderer renderer) : base(renderer)
        {
        }

        public override nfloat HeightForHeader(TKDataForm dataForm, uint groupIndex)
        {
            return 30;
        }

        public override void UpdateGroupView(TKDataForm dataForm, TKEntityPropertyGroupView groupView, uint groupIndex)
        {
            base.UpdateGroupView(dataForm, groupView, groupIndex);

            groupView.TitleView.TitleLabel.TextColor = new UIColor(0.46f, 0.46f, 0.46f, 1);
            groupView.TitleView.Style.Insets = new UIEdgeInsets(15, 30, 0, 0);
            groupView.TitleView.TitleLabel.Font = UIFont.SystemFontOfSize(12);
        }
    }
}