using System;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace QSF.Behaviors
{
    public class OpenPopupBehavior : Behavior<RadPickerBase>
    {
        public static readonly BindableProperty IsPickerOpenedProperty =
            BindableProperty.Create(nameof(IsPickerOpened), typeof(bool), typeof(OpenPopupBehavior), false, BindingMode.TwoWay, propertyChanged: OnIsPickerOpenedChanged);

        private RadPickerBase owner;

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

        protected override void OnAttachedTo(RadPickerBase bindable)
        {
            base.OnAttachedTo(bindable);
            this.owner = bindable;
            this.owner.BindingContextChanged += Picker_BindingContextChanged;
        }

        protected override void OnDetachingFrom(RadPickerBase bindable)
        {
            base.OnDetachingFrom(bindable);
            this.owner.BindingContextChanged -= Picker_BindingContextChanged;
            this.owner = null;
        }

        private static void OnIsPickerOpenedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (OpenPopupBehavior)bindable;

            if (behavior.IsPickerOpened)
            {
                behavior.owner.ToggleCommand.Execute(null);
                behavior.IsPickerOpened = false;
            }
        }

        private void Picker_BindingContextChanged(object sender, EventArgs e)
        {
            this.BindingContext = this.owner.BindingContext;
        }
    }
}
