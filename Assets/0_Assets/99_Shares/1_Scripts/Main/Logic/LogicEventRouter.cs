using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Attention.Main.EventModule
{
    // TODO: 일반화해서 사용?
    public class LogicEventRouter
    {
        private IEventDispatcher<ILogicEvent> _eventDispatcher;

        private Dictionary<Type, List<Action<ILogicEvent>>> _handlers;

        public LogicEventRouter(IEventDispatcher<ILogicEvent> eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        public void Update()
        {
            while (_eventDispatcher.TryDispatch(out ILogicEvent eventData))
            {
                HandleEvent(eventData);
            }
        }

        public void RegisterEvents()
        {
            _handlers = new Dictionary<Type, List<Action<ILogicEvent>>>();

            var handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(IEventHandler).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var handlerType in handlerTypes)
            {
                var handlerInstance = Activator.CreateInstance(handlerType);

                var methods = handlerType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(m =>
                        m.GetParameters().Length == 1 &&
                        typeof(ILogicEvent).IsAssignableFrom(m.GetParameters()[0].ParameterType));

                foreach (var method in methods)
                {
                    var paramType = method.GetParameters()[0].ParameterType;

                    Action<ILogicEvent> action = (evt) =>
                    {
                        method.Invoke(handlerInstance, new object[] { evt });
                    };

                    if (!_handlers.TryGetValue(paramType, out var list))
                    {
                        list = new List<Action<ILogicEvent>>();
                        _handlers[paramType] = list;
                    }

                    list.Add(action);
                }
            }
        }

        private void HandleEvent(ILogicEvent eventData)
        {
            Type type = eventData.GetType();
            if (_handlers.TryGetValue(type, out var handlerList))
            {
                foreach (var handler in handlerList)
                {
                    handler.Invoke(eventData);
                }
            }
        }
    }
}