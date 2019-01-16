using QSF.Examples.DataFormControl.GlobalizationExample.Resources;
using Telerik.XamarinForms.Input.DataForm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.DataFormControl.GlobalizationExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GlobalizationView : ContentView
    {
        public GlobalizationView()
        {
            DataFormLocalizationManager.Manager = DataFormResources.ResourceManager;
            InitializeComponent();
            dataForm.RegisterEditor("Married", Telerik.XamarinForms.Input.EditorType.CheckBoxEditor);
        }
    }
}