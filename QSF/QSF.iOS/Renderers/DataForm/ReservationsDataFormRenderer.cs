using QSF.Examples.DataFormControl.ReservationsExample;
using QSF.iOS.Renderers.DataForm;
using System;
using Telerik.XamarinForms.Input.DataForm;
using Telerik.XamarinForms.InputRenderer.iOS;
using TelerikUI;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ReservationsDataForm), typeof(ReservationsDataFormRenderer))]
namespace QSF.iOS.Renderers.DataForm
{
    public class ReservationsDataFormRenderer : DataFormRenderer
    {
        protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<Telerik.XamarinForms.Input.RadDataForm> e)
        {
            base.OnElementChanged(e);
            if (this.Element != null)
            {
                this.Element.GroupLayoutDefinition = new DataFormGroupStackLayoutDefinition();
            }
        }

        protected override Type GetCustomEditorType(string propertyName, Type propertyType)
        {
            switch (propertyName)
            {
                case nameof(Reservation.ReservationHolder):
                    return typeof(TKDataFormTextFieldEditor);

                case nameof(Reservation.HolderPhoneNumber):
                    return typeof(TKDataFormPhoneEditor);

                case nameof(Reservation.ReservationDate):
                    return typeof(TKDataFormDatePickerEditor);

                case nameof(Reservation.ReservationTime):
                    return typeof(TKDataFormTimePickerEditor);

                case nameof(Reservation.GuestNumber):
                    return typeof(TKDataFormStepperEditor);

                case nameof(Reservation.TableSection):
                case nameof(Reservation.TableNumber):
                    return typeof(TKDataFormPickerViewEditor);

                case nameof(Reservation.OrderOrigin):
                    return typeof(TKDataFormSegmentedEditor);

                default:
                    return base.GetCustomEditorType(propertyName, propertyType);
            }
        }

        protected override void InitEditor(TKDataFormEditor editor, IEntityProperty property)
        {
            base.InitEditor(editor, property);

            var name = property.PropertyName;

            editor.Style.ImageViewSize = new CoreGraphics.CGSize(15, 15);
            editor.Style.AccessoryArrowSize = new CoreGraphics.CGSize(10, 15);
            editor.Style.AccessoryArrowStroke = new TKStroke(UIColor.FromRGB(199, 51, 57), 2);
            editor.Style.Insets = UIEdgeInsets.Zero;
            editor.Style.SeparatorLeadingSpace = 30;

            if (name != nameof(Reservation.GuestNumber) &&
                name != nameof(Reservation.TableSection) &&
                name != nameof(Reservation.TableNumber))
            {
                editor.Style.TextLabelDisplayMode = TKDataFormEditorTextLabelDisplayMode.Hidden;

                var textLabelDef = editor.GridLayout.DefinitionForView(editor.TextLabel);
                editor.GridLayout.SetWidth(0, textLabelDef.Column.Int32Value);
                editor.Style.EditorOffset = new UIOffset(15, 0);
            }
            else
            {
                editor.Style.TextLabelOffset = new UIOffset(15, 0);
            }

            switch (name)
            {
                case nameof(Reservation.ReservationHolder):
                    editor.Property.Image = new UIImage("DataForm_Guest_Name.png");
                    break;

                case nameof(Reservation.GuestNumber):
                    editor.Property.Image = new UIImage("DataForm_Guest_Number.png");
                    editor.TintColor = UIColor.FromRGB(199, 51, 57);

                    var stepperLabel = (editor as TKDataFormStepperEditor).ValueLabel;
                    var labelDef = editor.GridLayout.DefinitionForView(stepperLabel);
                    labelDef.ContentOffset = new UIOffset(-15, 0);
                    stepperLabel.TextColor = SelectDynamicColor(UIColor.Black, UIColor.White);
                    break;

                case nameof(Reservation.HolderPhoneNumber):
                    editor.Property.Image = new UIImage("DataForm_Phone_Number.png");
                    break;

                case nameof(Reservation.ReservationDate):
                    editor.Property.Image = new UIImage("DataForm_Date.png");
                    (editor as TKDataFormInlineEditor).EditorValueLabel.TextColor = SelectDynamicColor(UIColor.Black, UIColor.White);
                    break;

                case nameof(Reservation.ReservationTime):
                    property.Metadata.Position = 0;
                    editor.Property.Image = new UIImage("DataForm_Time.png");
                    (editor as TKDataFormInlineEditor).EditorValueLabel.TextColor = SelectDynamicColor(UIColor.Black, UIColor.White);
                    break;

                case nameof(Reservation.TableSection):
                    editor.Property.Image = new UIImage("DataForm_Section.png");
                    var sectionPickerEditor = editor as TKDataFormPickerViewEditor;
                    var sectionValueLabel = sectionPickerEditor.EditorValueLabel;
                    sectionValueLabel.TextColor = SelectDynamicColor(UIColor.Black, UIColor.White);
                    sectionValueLabel.TextAlignment = UITextAlignment.Right;
                    sectionValueLabel.TextInsets = new UIEdgeInsets(0, 0, 0, 15);
                    break;

                case nameof(Reservation.TableNumber):
                    editor.Property.Image = new UIImage("DataForm_Table_Number.png");
                    var pickerEditor = editor as TKDataFormPickerViewEditor;
                    var valueLabel = pickerEditor.EditorValueLabel;
                    valueLabel.TextColor = SelectDynamicColor(UIColor.Black, UIColor.White); ;
                    valueLabel.TextAlignment = UITextAlignment.Right;
                    valueLabel.TextInsets = new UIEdgeInsets(0, 0, 0, 15);
                    break;

                case nameof(Reservation.OrderOrigin):
                    var segmentedEditor = editor as TKDataFormSegmentedEditor;
                    segmentedEditor.SegmentedControl.TintColor = UIColor.FromRGB(199, 51, 57);
                    editor.Style.EditorOffset = new UIOffset(0, 0);
                    editor.Style.SeparatorColor = new TKFill();
                    break;
            }
        }

        protected override TKDataFormDelegate GetDataFormDelegate(TKDataForm form)
        {
            return new DFDelegate(this);
        }

        private static UIColor SelectDynamicColor(UIColor color, UIColor darkColor)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                return UIColor.FromDynamicProvider((traitCollection) =>
                {
                    return traitCollection.UserInterfaceStyle == UIUserInterfaceStyle.Dark
                    ? darkColor
                    : color;
                });
            }

            return color;

        }
    }
}