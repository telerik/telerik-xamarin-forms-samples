using System;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.Behaviors
{
    public class EventToCommandBehavior : BehaviorBase<View>
    {
        private Delegate _eventHandler;

        public static readonly BindableProperty EventNameProperty = BindableProperty.Create("EventName", typeof(string), typeof(EventToCommandBehavior), null, propertyChanged: OnEventNameChanged);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(EventToCommandBehavior));
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(EventToCommandBehavior));
        public static readonly BindableProperty InputConverterProperty = BindableProperty.Create("Converter", typeof(IValueConverter), typeof(EventToCommandBehavior));

        public string EventName
        {
            get => (string)GetValue(EventNameProperty);
            set => SetValue(EventNameProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public IValueConverter Converter
        {
            get => (IValueConverter)GetValue(InputConverterProperty);
            set => SetValue(InputConverterProperty, value);
        }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            this.RegisterEvent(this.EventName);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);
            this.DeregisterEvent(this.EventName);
        }

        private void RegisterEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var eventInfo = this.AssociatedObject.GetType().GetRuntimeEvent(name);

            if (eventInfo == null)
            {
                throw new ArgumentException($"EventToCommandBehavior: Can't register the '{this.EventName}' event.");
            }

            var methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnEvent");

            this._eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);

            eventInfo.AddEventHandler(this.AssociatedObject, this._eventHandler);
        }

        private void DeregisterEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || this._eventHandler == null)
            {
                return;
            }

            var eventInfo = this.AssociatedObject.GetType().GetRuntimeEvent(name);

            if (eventInfo == null)
            {
                throw new ArgumentException($"EventToCommandBehavior: Can't de-register the '{EventName}' event.");
            }

            eventInfo.RemoveEventHandler(this.AssociatedObject, this._eventHandler);

            this._eventHandler = null;
        }

        private void OnEvent(object sender, object eventArgs)
        {
            if (this.Command == null)
            {
                return;
            }

            object resolvedParameter;

            if (this.CommandParameter != null)
            {
                resolvedParameter = this.CommandParameter;
            }
            else if (this.Converter != null)
            {
                resolvedParameter = this.Converter.Convert(eventArgs, typeof(object), null, null);
            }
            else
            {
                resolvedParameter = eventArgs;
            }

            if (this.Command.CanExecute(resolvedParameter))
                this.Command.Execute(resolvedParameter);
        }

        private static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (EventToCommandBehavior)bindable;

            if (behavior.AssociatedObject == null)
                return;

            var oldEventName = oldValue as string;
            var newEventName = newValue as string;

            behavior.DeregisterEvent(oldEventName);
            behavior.RegisterEvent(newEventName);
        }
    }
}