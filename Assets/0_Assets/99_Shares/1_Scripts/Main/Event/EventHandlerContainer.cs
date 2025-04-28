using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;

namespace Attention.Main.EventModule
{
    public class EventHandlerContainer<T> where T : IEvent
    {
        private Dictionary<Type, List<Action<T>>> _handlers;

        public EventHandlerContainer()
        {
            _handlers = new Dictionary<Type, List<Action<T>>>();
        }

        public void RegisterEvents()
        {
            _handlers = new Dictionary<Type, List<Action<T>>>();

            var handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(IEventHandler<T>).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var handlerType in handlerTypes)
            {
                var handlerInstance = Activator.CreateInstance(handlerType);

                var methods = handlerType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(m =>
                        m.GetParameters().Length == 1 &&
                        typeof(T).IsAssignableFrom(m.GetParameters()[0].ParameterType));

                foreach (var method in methods)
                {
                    var paramType = method.GetParameters()[0].ParameterType;

                    Action<T> action = (evt) =>
                    {
                        method.Invoke(handlerInstance, new object[] { evt });
                    };

                    if (!_handlers.TryGetValue(paramType, out var list))
                    {
                        list = new List<Action<T>>();
                        _handlers[paramType] = list;
                    }

                    list.Add(action);
                }
            }
        }

        public bool TryGetHandlers(Type type, out List<Action<T>> handlerList)
        {
            if (_handlers.TryGetValue(type, out handlerList))
            {
                return true;
            }
            return false;
        }
    }
}