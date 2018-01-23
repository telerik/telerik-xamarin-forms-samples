using Android.Graphics;
using Android.Graphics.Drawables;
using Com.Telerik.Widget.Dataform.Visualization;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.Input.DataForm;
using Telerik.XamarinForms.InputRenderer.Android;

namespace QSF.Droid.Renderers.DataForm
{
    public class CustomGroupLayoutManager : GroupLayoutManager
    {
        private IDataFormRenderer renderer;

        public CustomGroupLayoutManager(IDataFormRenderer renderer) : base(renderer)
        {
            this.renderer = renderer;
        }

        protected override EditorGroup CreateEditorGroup(string groupName)
        {
            if (groupName != null)
            {
                var layout = this.renderer.GetGroupLayoutDefinition(groupName);
                var group = new EditorGroup(Xamarin.Forms.Forms.Context, groupName, Resource.Layout.Custom_Group_Layout);
                group.LayoutManager = TypeMappings.CreateInstance(layout) as DataFormLayoutManager;
                group.RootLayout().Background = new ColorDrawable(Color.White);

                return group;
            }

            return base.CreateEditorGroup(groupName);
        }
    }
}