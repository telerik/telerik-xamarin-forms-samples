using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace tagit.Behaviors
{
    public class EventToCommandBehavior : BindableBehavior<View>
    {
        public static readonly BindableProperty EventNameProperty = BindableProperty.Create<EventToCommandBehavior, string>(p => p.EventName, null);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create<EventToCommandBehavior, ICommand>(p => p.Command, null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create<EventToCommandBehavior, object>(p => p.CommandParameter, null);
        public static readonly BindableProperty EventArgsConverterProperty = BindableProperty.Create<EventToCommandBehavior, IValueConverter>(p => p.EventArgsConverter, null);
        public static readonly BindableProperty EventArgsConverterParameterProperty = BindableProperty.Create<EventToCommandBehavior, object>(p => p.EventArgsConverterParameter, null);

        private Delegate _handler;
        private EventInfo _eventInfo;
        
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

        public IValueConverter EventArgsConverter
        {
            get => (IValueConverter)GetValue(EventArgsConverterProperty);
            set => SetValue(EventArgsConverterProperty, value);
        }
        
        public object EventArgsConverterParameter
        {
            get => GetValue(EventArgsConverterParameterProperty);
            set => SetValue(EventArgsConverterParameterProperty, value);
        }
        
        protected override void OnAttachedTo(View visualElement)
        {
            base.OnAttachedTo(visualElement);

            var events = AssociatedObject.GetType().GetRuntimeEvents().ToArray();

            if (events.Any())
            {
                _eventInfo = events.FirstOrDefault(e => e.Name == EventName);

                if (_eventInfo == null)

                    throw new ArgumentException(
                        $"EventToCommand: Can't find any event named '{EventName}' on attached type");
                
                AddEventHandler(_eventInfo, AssociatedObject, OnFired);
            }
        }
        
        protected override void OnDetachingFrom(View view)
        {
            if (_handler != null)
                _eventInfo.RemoveEventHandler(AssociatedObject, _handler);
            
            base.OnDetachingFrom(view);
        }
        
        private void AddEventHandler(EventInfo eventInfo, object item, Action<object, EventArgs> action)
        {
            var eventParameters = eventInfo.EventHandlerType
                .GetRuntimeMethods().First(m => m.Name == "Invoke")
                .GetParameters()
                .Select(p => Expression.Parameter(p.ParameterType))
                .ToArray();

            var actionInvoke = action.GetType()
                .GetRuntimeMethods().First(m => m.Name == "Invoke");

            _handler = Expression.Lambda(
                eventInfo.EventHandlerType,
                Expression.Call(Expression.Constant(action), actionInvoke, eventParameters[0], eventParameters[1]),
                eventParameters
            ).Compile();
            
            eventInfo.AddEventHandler(item, _handler);
        }

        private void OnFired(object sender, EventArgs eventArgs)
        {
            if (Command == null)
                return;
            
            var parameter = CommandParameter;
            
            if (eventArgs != null && eventArgs != EventArgs.Empty)
            {
                parameter = eventArgs;

                if (EventArgsConverter != null)
                {
                    parameter = EventArgsConverter.Convert(eventArgs, typeof(object), EventArgsConverterParameter, CultureInfo.CurrentUICulture);
                }
            }
            
            if (Command.CanExecute(parameter))
            {
                Command.Execute(parameter);
            }

        }

    }
}