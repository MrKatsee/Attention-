using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;

namespace Attention.Main.EventModule
{
    public interface IEventHandlerContainer<T> where T : IEvent
    {
        bool TryGetEvents(Type eventType, out List<Action<T>> events);
    }

    public class EventHandlerContainer<T> : IEventHandlerContainer<T> where T : IEvent
    {
        private Dictionary<Type, IEventHandler<T>> _eventHandlers;
        private Dictionary<Type, List<Action<T>>> _registeredEvents;

        public void Init()
        {
            InitializeHandlers();
            RegisterEvents();
        }

        private void InitializeHandlers()
        {
            _eventHandlers = new Dictionary<Type, IEventHandler<T>>();

            var handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(IEventHandler<T>).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var handlerType in handlerTypes)
            {
                var handler = Activator.CreateInstance(handlerType);
                _eventHandlers.Add(handlerType, (IEventHandler<T>)handler);
            }

            OnInitializeHandlers(_eventHandlers.Values);
        }
        protected virtual void OnInitializeHandlers(IEnumerable<IEventHandler<T>> eventHandlers) { }

        private void RegisterEvents()
        {
            _registeredEvents = new Dictionary<Type, List<Action<T>>>();

            var handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(IEventHandler<T>).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var handlerType in _eventHandlers.Keys)
            {
                var methods = handlerType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(m =>
                        m.GetParameters().Length == 1 &&
                        typeof(T).IsAssignableFrom(m.GetParameters()[0].ParameterType));

                foreach (var method in methods)
                {
                    var paramType = method.GetParameters()[0].ParameterType;

                    Action<T> action = (evt) =>
                    {
                        method.Invoke(_eventHandlers[handlerType], new object[] { evt });
                    };

                    if (!_registeredEvents.TryGetValue(paramType, out var list))
                    {
                        list = new List<Action<T>>();
                        _registeredEvents[paramType] = list;
                    }

                    list.Add(action);
                }
            }
        }

        public bool TryGetEvents(Type eventType, out List<Action<T>> events)
        {
            if (_registeredEvents.TryGetValue(eventType, out events))
            {
                return true;
            }
            return false;
        }
    }
}