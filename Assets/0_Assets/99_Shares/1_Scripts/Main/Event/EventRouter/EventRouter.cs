using System;

namespace Attention.Main.EventModule
{
    public abstract class EventRouter<T> where T : IEvent
    {
        private IEventDispatcher<T> _eventDispatcher;
        private IEventHandlerContainer<T> _handlers;

        public EventRouter(IEventDispatcher<T> eventDispatcher, IEventHandlerContainer<T> eventHandlers)
        {
            _eventDispatcher = eventDispatcher;
            _handlers = eventHandlers;;
        }

        public void Update()
        {
            while (_eventDispatcher.TryDispatch(out T eventData))
            {
                HandleEvent(eventData);
            }
        }

        private void HandleEvent(T eventData)
        {
            Type type = eventData.GetType();
            if (_handlers.TryGetEvents(type, out var handlerList))
            {
                foreach (var handler in handlerList)
                {
                    handler.Invoke(eventData);
                }
            }
        }
    }
}