using QSF.Examples.DataFormControl.ThemingExample;
using QSF.iOS.Renderers.DataForm;
using Telerik.XamarinForms.InputRenderer.iOS;
using TelerikUI;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ThemingDataForm), typeof(ThemingDataFormRenderer))]
namespace QSF.iOS.Renderers.DataForm
{
    public class ThemingDataFormRenderer : DataFormRenderer
    {
        protected override TKDataFormDelegate GetDataFormDelegate(TKDataForm form)
        {
            return new ThemingDataFormDelegate(this);
        }
    }
}