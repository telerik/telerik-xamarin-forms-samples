using System.Collections;
using System.Collections.Generic;
using Telerik.XamarinForms.Input;
using Telerik.XamarinForms.Input.DataForm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.DataFormControl.ThemingExample
{
    public partial class ThemingView : ContentView
    {
        public ThemingView()
        {
            this.InitializeComponent();

            var viewModel = new ThemingViewModel();
            this.BindingContext = viewModel;

            this.RegisterEditors();
            dataForm.PropertyDataSourceProvider = new DF020PropertyDataSourceProvider();
            dataForm.GroupLayoutDefinition = new DataFormGroupGridLayoutDefinition();
            dataForm.GroupHeaderStyle = new DataFormGroupHeaderStyle
            {
                Foreground = Color.Transparent,
                Height = 0,
                Background = Color.Transparent
            };
        }

        public class DF020PropertyDataSourceProvider : PropertyDataSourceProvider
        {
            public override IList GetSourceForKey(object key)
            {
                if (key.Equals("ListPick"))
                {
                    return new List<int> { 33, 55, 77, 88 };
                }

                if (key.Equals("IntSegment"))
                {
                    return new List<int> { 1, 2, 3 };
                }

                return base.GetSourceForKey(key);
            }
        }

        private void RegisterEditors()
        {
            dataForm.RegisterEditor("EnumPick", EditorType.PickerEditor);
            dataForm.RegisterEditor("ListPick", EditorType.PickerEditor);
            dataForm.RegisterEditor("StringValue", EditorType.TextEditor);
            dataForm.RegisterEditor("EnumSegment", EditorType.SegmentedEditor);
            dataForm.RegisterEditor("IntSegment", EditorType.SegmentedEditor);
            dataForm.RegisterEditor("NumberValue", EditorType.NumberPickerEditor);
            dataForm.RegisterEditor("DateValue", EditorType.DateEditor);
            dataForm.RegisterEditor("TimeValue", EditorType.TimeEditor);
            dataForm.RegisterEditor("CheckValue1", EditorType.CheckBoxEditor);
            dataForm.RegisterEditor("CheckValue2", EditorType.CheckBoxEditor);
            dataForm.RegisterEditor("ToggleValue", EditorType.ToggleButtonEditor);
            dataForm.RegisterEditor("ToggleValue2", EditorType.ToggleButtonEditor);
            dataForm.RegisterEditor("BaseValue", EditorType.SliderEditor);
        }
   }
}