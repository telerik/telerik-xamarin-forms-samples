using Android.Content;
using Android.Runtime;
using AndroidX.AppCompat.Widget;
using Com.Telerik.Widget.Dataform.Visualization.Core;
using Com.Telerik.Widget.Dataform.Visualization.Editors;
using Telerik.XamarinForms.InputRenderer.Android;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Telerik.XamarinForms.Input.RadDataForm), typeof(ArtGalleryCRM.Droid.Renderers.CustomDataFormRenderer))]
namespace ArtGalleryCRM.Droid.Renderers
{
    public class CustomDataFormRenderer : DataFormRenderer
    {
        public CustomDataFormRenderer(Context context) : base(context)
        {
        }

        protected override void UpdateEditor(EntityPropertyEditor editor, Telerik.XamarinForms.Input.DataForm.IEntityProperty property)
        {
            base.UpdateEditor(editor, property);

            if (editor is DataFormTextEditor || editor is DataFormDecimalEditor || editor is DataFormIntegerEditor)
            {
                var editText = editor.EditorView.JavaCast<AppCompatEditText>();
                editText.ImeOptions = global::Android.Views.InputMethods.ImeAction.Done;
            }
        }
    }
}