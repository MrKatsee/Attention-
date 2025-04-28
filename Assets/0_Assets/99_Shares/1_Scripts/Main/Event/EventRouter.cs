using System;

namespace Attention.Main.EventModule
{
    public abstract class EventRouter<T> where T : IEvent
    {
        private IEventDispatcher<T> _eventDispatcher;
        private EventHandlerContainer<T> _handlers;

        public EventRouter(IEventDispatcher<T> eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
            _handlers = new EventHandlerContainer<T>();
        }

        public void Init()
        {
            _handlers.RegisterEvents();
        }

        public void Update()
        {
            while (_eventDispatcher.TryDispatch(out T eventData))
            {
                OnBeforeHandleEvent(eventData);
                HandleEvent(eventData);
            }
        }

        private void HandleEvent(T eventData)
        {
            Type type = eventData.GetType();
            if (_handlers.TryGetHandlers(type, out var handlerList))
            {
                foreach (var handler in handlerList)
                {
                    handler.Invoke(eventData);
                }
            }
        }

        protected virtual void OnBeforeHandleEvent(T eventData) { }
    }
}