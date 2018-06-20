using Android.Widget;
using Com.Telerik.Widget.Dataform.Visualization;
using Com.Telerik.Widget.Dataform.Visualization.Core;
using Com.Telerik.Widget.Dataform.Visualization.Editors;
using Com.Telerik.Widget.Numberpicker;
using QSF.Droid.Renderers;
using QSF.Droid.Renderers.DataForm;
using QSF.Examples.DataFormControl.ReservationsExample;
using Telerik.XamarinForms.InputRenderer.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Telerik.XamarinForms.Input.RadDataForm), typeof(CustomDataFormRenderer))]
namespace QSF.Droid.Renderers
{
    public class CustomDataFormRenderer : DataFormRenderer
    {
        public CustomDataFormRenderer(Android.Content.Context context) : base(context)
        { }

        protected override DataFormLayoutManager GetLayoutManager()
        {
            return new CustomGroupLayoutManager(this,this.Context);
        }

        protected override EntityPropertyEditor GetCustomEditorForProperty(RadDataForm form, Com.Telerik.Widget.Dataform.Engine.IEntityProperty nativeProperty, Telerik.XamarinForms.Input.DataForm.IEntityProperty property)
        {
            var name = property.PropertyName;

            switch (name)
            {
                case nameof(Reservation.ReservationHolder):
                    nativeProperty.ImageResource = Resource.Drawable.DataForm_Guest_Name;

                    var editor = new DataFormTextEditor(form,
                                 Resource.Layout.Editor_Main_Layout_1,
                                 form.EditorsHeaderLayout,
                                 Resource.Id.data_form_text_viewer_header,
                                 Resource.Layout.data_form_text_editor,
                                 Resource.Id.data_form_text_editor,
                                 form.EditorsValidationLayout,
                                 nativeProperty);

                    return editor;

                case nameof(Reservation.HolderPhoneNumber):
                    nativeProperty.ImageResource = Resource.Drawable.DataForm_Phone_Number;

                    var textEditor = new DataFormTextEditor(form,
                                 Resource.Layout.Editor_Main_Layout_1,
                                 form.EditorsHeaderLayout,
                                 Resource.Id.data_form_text_viewer_header,
                                 Resource.Layout.data_form_text_editor,
                                 Resource.Id.data_form_text_editor,
                                 form.EditorsValidationLayout,
                                 nativeProperty);
                    textEditor.HeaderView.Visibility = Android.Views.ViewStates.Invisible;

                    return textEditor;

                case nameof(Reservation.GuestNumber):
                    nativeProperty.ImageResource = Resource.Drawable.DataForm_Guest_Number;

                    var numberEditor = new DataFormNumberPickerEditor(form,
                                     Resource.Layout.Editor_Main_Layout_1,
                                     form.EditorsHeaderLayout,
                                     Resource.Id.data_form_text_viewer,
                                     Resource.Layout.data_form_number_picker,
                                     Resource.Id.data_form_number_picker_editor,
                                     form.EditorsValidationLayout,
                                     nativeProperty);

                    var label = ((numberEditor.EditorView as RadNumberPicker).LabelView() as TextView);
                    numberEditor.EditorView.RootView.LayoutParameters.Height = (int)this.Context.ToPixels(44);
                    numberEditor.EditorView.LayoutParameters.Height = (int)this.Context.ToPixels(44);
                    label.SetTextColor(Android.Graphics.Color.Black);
                    return numberEditor;

                case nameof(Reservation.ReservationDate):
                    nativeProperty.ImageResource = Resource.Drawable.DataForm_Date;

                    var dateEditor = new DataFormDateEditor(form,
                                 Resource.Layout.Editor_Main_Layout_2,
                                 form.EditorsHeaderLayout,
                                 Resource.Id.data_form_text_viewer,
                                 Resource.Layout.data_form_date_editor,
                                 Resource.Id.data_form_date_editor,
                                 form.EditorsValidationLayout,
                                 nativeProperty);

                    return dateEditor;

                case nameof(Reservation.ReservationTime):
                    var timeEditor = new DataFormTimeEditor(form,
                                     Resource.Layout.Editor_Main_Layout_2,
                                     form.EditorsHeaderLayout,
                                     Resource.Id.data_form_text_viewer,
                                     Resource.Layout.data_form_time_editor,
                                     Resource.Id.data_form_date_editor,
                                     form.EditorsValidationLayout,
                                     nativeProperty);

                    var editorView = timeEditor.EditorView as EditText;
                    (editorView.LayoutParameters as FrameLayout.LayoutParams).LeftMargin = (int)this.Context.ToPixels(12);

                    (timeEditor.HeaderView.LayoutParameters as FrameLayout.LayoutParams).LeftMargin = (int)this.Context.ToPixels(12);

                    return timeEditor;

                case nameof(Reservation.OrderOrigin):
                    var orderEditor = new DataFormSpinnerEditor(form,
                                      Resource.Layout.Editor_Main_Layout_2,
                                      form.EditorsHeaderLayout,
                                      Resource.Id.data_form_text_viewer_header,
                                      Resource.Layout.data_form_spinner_editor,
                                      Resource.Id.data_form_spinner_editor,
                                      form.EditorsValidationLayout,
                                      nativeProperty, null);

                    var leftMargin = (int)this.Context.ToPixels(32);

                    (orderEditor.EditorView.LayoutParameters as FrameLayout.LayoutParams).LeftMargin = leftMargin;
                    (orderEditor.HeaderView.LayoutParameters as FrameLayout.LayoutParams).LeftMargin = leftMargin;

                    return orderEditor;

                case nameof(Reservation.TableSection):
                    nativeProperty.ImageResource = Resource.Drawable.DataForm_Table_Number;

                    var sectionEditor = new DataFormSpinnerEditor(form,
                                     Resource.Layout.Editor_Main_Layout_2,
                                     form.EditorsHeaderLayout,
                                     Resource.Id.data_form_text_viewer_header,
                                     Resource.Layout.data_form_spinner_editor,
                                     Resource.Id.data_form_spinner_editor,
                                     form.EditorsValidationLayout,
                                     nativeProperty, null);

                    var sectionEditorView = sectionEditor.EditorView as Spinner;
                    (sectionEditorView.LayoutParameters as FrameLayout.LayoutParams).RightMargin = (int)this.Context.ToPixels(12);
                    return sectionEditor;

                case nameof(Reservation.TableNumber):
                    var tableNumberEditor = new DataFormSpinnerEditor(form,
                                            Resource.Layout.Editor_Main_Layout_2,
                                            form.EditorsHeaderLayout,
                                            Resource.Id.data_form_text_viewer_header,
                                            Resource.Layout.data_form_spinner_editor,
                                            Resource.Id.data_form_spinner_editor,
                                            form.EditorsValidationLayout,
                                            nativeProperty, null);

                    (tableNumberEditor.EditorView.LayoutParameters as FrameLayout.LayoutParams).LeftMargin = (int)this.Context.ToPixels(12);
                    (tableNumberEditor.HeaderView.LayoutParameters as FrameLayout.LayoutParams).LeftMargin = (int)this.Context.ToPixels(12);

                    return tableNumberEditor;

                default:
                    return base.GetCustomEditorForProperty(form, nativeProperty, property);
            }
        }
    }
}