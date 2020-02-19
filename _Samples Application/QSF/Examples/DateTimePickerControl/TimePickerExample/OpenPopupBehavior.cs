using System;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Examples.DateTimePickerControl.TimePickerExample
{
    public class OpenPopupBehavior : Behavior<RadDateTimePicker>
    {
        public static readonly BindableProperty IsPickerOpenedProperty =
            BindableProperty.Create(nameof(IsPickerOpened), typeof(bool), typeof(OpenPopupBehavior), false, BindingMode.TwoWay, propertyChanged: OnCommandSet);

        private RadDateTimePicker owner;

        public bool IsPickerOpened
        {
            get
            {
                return (bool)this.GetValue(IsPickerOpenedProperty);
            }
            set
            {
                this.SetValue(IsPickerOpenedProperty, value);
            }
        }

        protected override void OnAttachedTo(RadDateTimePicker bindable)
        {
            base.OnAttachedTo(bindable);
            this.owner = bindable;
            this.owner.BindingContextChanged += Picker_BindingContextChanged;
        }

        protected override void OnDetachingFrom(RadDateTimePicker bindable)
        {
            base.OnDetachingFrom(bindable);
            this.owner.BindingContextChanged -= Picker_BindingContextChanged;
            this.owner = null;
        }

        private static void OnCommandSet(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (OpenPopupBehavior)bindable;

            if (behavior.IsPickerOpened)
            {
                behavior.ToggleOwner();
                behavior.IsPickerOpened = false;
            }
        }

        private void Picker_BindingContextChanged(object sender, EventArgs e)
        {
            this.BindingContext = this.owner.BindingContext;
        }

        private void ToggleOwner()
        {
            this.owner.ToggleCommand.Execute(null);
        }
    }
}
